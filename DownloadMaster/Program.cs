using System;
using System.Threading;

class MyProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the File Downloader!");
        Console.Write("Enter the file URL: ");
        string fileUrl = Console.ReadLine();
        Console.Write("Enter the destination folder: ");
        string destinationFolder = Console.ReadLine();

        FileDownloader fileDownloader = new FileDownloader(fileUrl, destinationFolder);

        // הרשמה לאירוע DownloadProgressChanged
        fileDownloader.DownloadProgressChanged += (progress) =>
        {
            Console.WriteLine($"Download progress: {progress}%");

            // הוסף שלטון משתמש - בדוגמה זו, אפשרות להפסיק הורדה באמצע
            Console.Write("Enter 'stop' to pause download or press Enter to continue: ");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "stop")
            {
                // השהה את ההורדה כאן (עלול להפסיק את השרשור ל-Thread)
                // הפעל כאן פקודות או פונקציות שתשהיה את ההורדה
            }
        };

        // הפעל את ההורדה
        fileDownloader.DownloadFile();
    }
}
