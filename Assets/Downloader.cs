using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YoutubePlayer;

public class Downloader : MonoBehaviour
{
    static readonly string[] k_DownloadFields = { "title", "_filename", "ext", "url" };
    private string destinationFolder;
    public string videoUrl;
    public Environment.SpecialFolder destination;
    public static string filePath = "notyet";

    
    

    // Start is called before the first frame update
    async void Start()
    {
        destinationFolder = Environment.GetFolderPath(destination);
        System.Threading.CancellationToken cancellationToken = default;
        var video = await YoutubeDl.GetVideoMetaDataAsync<YoutubeVideoMetaData>(videoUrl, k_DownloadFields, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        var fileName = GetVideoFileName(video);
        var filePath2 = fileName;
        if (!string.IsNullOrEmpty(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
            filePath2 = Path.Combine(destinationFolder, fileName);
            Debug.Log(filePath2);
        }
        await YoutubeDownloader.DownloadAsync(video, filePath2, cancellationToken);
        filePath = filePath2;
    }



    static string GetVideoFileName(YoutubeVideoMetaData video)
    {
        if (!string.IsNullOrWhiteSpace(video.FileName))
        {
            return video.FileName;
        }

        var fileName = $"{video.Title}.{video.Extension}";

        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var invalidChar in invalidChars)
        {
            fileName = fileName.Replace(invalidChar.ToString(), "_");
        }

        return fileName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
