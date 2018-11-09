using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoutubeLight;
using System;
using SimpleJSON;

public class HandheldPlayback : MonoBehaviour {

    RequestResolver resolver;
    private string videoUrl;
    public bool getFromWebServer = false;

    public static HandheldPlayback instance;

    Action videoFinishCallback;

    private void Start()
    {
        instance = this;
        resolver = gameObject.AddComponent<RequestResolver>();
    }

    public void PlayVideo(string url, Action OnVideoFinished)
    {
        videoFinishCallback = OnVideoFinished;
        if (!getFromWebServer)
            StartCoroutine(resolver.GetDownloadUrls(FinishLoadingUrls, url, false));
        else
            StartCoroutine(NewRequest(url));
        
    }

    void FinishLoadingUrls()
    {
        List<VideoInfo> videoInfos = resolver.videoInfos;
        foreach (VideoInfo info in videoInfos)
        {
            if (info.VideoType == VideoType.Mp4 && info.Resolution == (720))
            {
                if (info.RequiresDecryption)
                {
                    //The string is the video url
                    StartCoroutine(resolver.DecryptDownloadUrl(DecryptionFinished, info));
                    break;
                }
                else
                {
                    StartCoroutine(Play(info.DownloadUrl));
                }
                break;
            }
        }
    }

    public void DecryptionFinished(string url)
    {
        StartCoroutine(Play(url));
    }

    IEnumerator Play(string url)
    {
        Debug.Log("Play!");
#if UNITY_IPHONE || UNITY_ANDROID
        Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill);
#else
        Debug.Log("This only runs in mobile");
#endif
        yield return new WaitForSeconds(1);
        videoFinishCallback.Invoke();
    }

    private const string serverURI = "https://unity-dev-youtube.herokuapp.com/api/info?url=https://www.youtube.com/watch?v=";
    private const string formatURI = "&format=best&flatten=true";
    public YoutubeResultIds newRequestResults;

    IEnumerator NewRequest(string videoID)
    {
        WWW request = new WWW(serverURI + "" + videoID + "" + formatURI);
        yield return request;
        var requestData = JSON.Parse(request.text);
        var videos = requestData["videos"][0]["formats"];
        newRequestResults.bestFormatWithAudioIncluded = requestData["videos"][0]["url"];

        videoUrl = newRequestResults.bestFormatWithAudioIncluded;
#if UNITY_WEBGL
        videoUrl = ConvertToWebglUrl(videoUrl);
        audioVideoUrl = ConvertToWebglUrl(audioVideoUrl);

#endif
        StartCoroutine(Play(videoUrl));
    }

}
