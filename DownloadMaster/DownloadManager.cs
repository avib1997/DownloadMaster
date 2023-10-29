using System;
using System.Collections.Generic;
using System.Threading;

class DownloadManager
{
    private List<FileDownloader> downloaders = new List<FileDownloader>();

    public FileDownloader AddDownload(string url, string destination)
    {
        FileDownloader downloader = new FileDownloader(url, destination);
        downloaders.Add(downloader);
        return downloader;
    }

    public void StartAllDownloads()
    {
        foreach (var downloader in downloaders)
        {
            downloader.DownloadFile();
        }
    }

    public void PauseAllDownloads()
    {
        foreach (var downloader in downloaders)
        {
            // השהה את ההורדה כאן
            downloader.PauseDownload();
        }
    }

    public void ResumeAllDownloads()
    {
        foreach (var downloader in downloaders)
        {
            // המשך את ההורדה כאן
            downloader.ResumeDownload();
        }
    }

    public void RemoveDownload(FileDownloader downloader)
    {
        downloaders.Remove(downloader);
    }

    public void ListDownloads()
    {
        Console.WriteLine("Downloads in progress:");
        foreach (var downloader in downloaders)
        {
            Console.WriteLine($"- {downloader.Url} to {downloader.Destination}");
        }
    }
}

class Program
{
    static void myMain(string[] args)
    {
        DownloadManager downloadManager = new DownloadManager();

        Console.WriteLine("Welcome to the Download Manager!");

        // הוספת הורדות חדשות
        FileDownloader download1 = downloadManager.AddDownload("URL1", "Destination1");
        FileDownloader download2 = downloadManager.AddDownload("URL2", "Destination2");

        // הפעלת הורדות
        downloadManager.StartAllDownloads();

        // השהייה של הורדות
        downloadManager.PauseAllDownloads();

        // המשך של הורדות
        downloadManager.ResumeAllDownloads();

        // הסרת הורדה
        downloadManager.RemoveDownload(download1);

        // רשימת הורדות פעילות
        downloadManager.ListDownloads();
    }
}
