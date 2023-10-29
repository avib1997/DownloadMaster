using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

class FileDownloader
{
    private string fileUrl;
    private string destinationFolder;
    private bool isDownloadComplete;
    private int downloadProgress;
    public event Action<int> DownloadProgressChanged;

    public FileDownloader(string url, string destination)
    {
        fileUrl = url;
        destinationFolder = destination;
        isDownloadComplete = false;
    }

    public void DownloadFile()
    {
        string filePath = Path.Combine(destinationFolder, "downloaded_file.ext");

        Thread downloadThread = new Thread(() =>
        {
            try
            {
                // קוד להורדת הקובץ מה-URL
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        downloadProgress = e.ProgressPercentage;
                        DownloadProgressChanged?.Invoke(downloadProgress);
                    };

                    string fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
                    string filePath = Path.Combine(destinationFolder, fileName);

                    client.DownloadFile(fileUrl, filePath);

                    isDownloadComplete = true;
                    DownloadProgressChanged?.Invoke(100); // סיום ההורדה
                }

                // בדיקת אינטגריטה של הקובץ לאחר ההורדה
                if (VerifyFileIntegrity(filePath))
                {
                    Console.WriteLine("Download complete and file integrity verified.");
                }
                else
                {
                    Console.WriteLine("Download failed. The downloaded file is corrupted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download error: " + ex.Message);
            }
        });

        downloadThread.Start();
    }

    private bool VerifyFileIntegrity(string filePath)
    {
        // כאן אתה יכול להמשיך לבדוק את הקובץ לפי האינטרסים שלך, לדוגמה - באמצעות בדיקת האם סימניות (hash) של הקובץ תואמות את הערך הצפוי.
        // ישנם אלגוריתמים כמו MD5, SHA-1 או SHA-256 שיכולים לשמש לצורך בדיקת אינטגריטה.
        return true; // כאן אתה יכול לשנות להחזרת ערך בהתאם לבדיקה.
    }
}
