using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubeLight;
using SimpleJSON;

public class SimplePlayback : MonoBehaviour
{
    public string videoId = "bc0sJvtKrRM";
    private string videoUrl;
    private bool videoAreReadyToPlay = false;
    //use unity player(all platforms) or old method to play in mobile only if you want, or if your mobile dont support the new player.
    public bool useNewUnityPlayer;
    public bool getFromWebServer = false;
    public VideoPlayer unityVideoPlayer;
    //start playing the video
    public bool playOnStart = false;
    public YoutubeLogo youtubeLogo;
    public RequestResolver resolver;
    int maxRetryUntilToGetFromWebServer = 2;
    int currentRetry = 0;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        //resolver = gameObject.AddComponent<RequestResolver>();
        unityVideoPlayer.errorReceived += PlayerError;
        yield return new WaitForSeconds(1);
        if (playOnStart)
        {
            PlayYoutubeVideo(videoId);
        }
    }

    void PlayerError(VideoPlayer p, string error)
    {
        RetryPlayback();
    }

    public void PlayYoutubeVideo(string _videoId)
    {
        if (youtubeLogo != null)
        {
            youtubeLogo.youtubeurl = "https://www.youtube.com/watch?v=" + _videoId;
        }
        videoId = _videoId;
        if (!getFromWebServer)
            StartCoroutine(resolver.GetDownloadUrls(FinishLoadingUrls, videoId, false));
        else
            StartCoroutine(NewRequest(videoId));

    }

    void FinishLoadingUrls()
    {
        List<VideoInfo> videoInfos = resolver.videoInfos;
        foreach (VideoInfo info in videoInfos)
        {
            if (info.VideoType == VideoType.Mp4 && info.Resolution == (360))
            {
                if (info.RequiresDecryption)
                {
                    //The string is the video url
                    videoAreReadyToPlay = false;
                    Debug.Log("Decript");
                    StartCoroutine(resolver.DecryptDownloadUrl(DecryptionFinished, info));
                    break;
                }
                else
                {
                    videoUrl = info.DownloadUrl;
                    videoAreReadyToPlay = true;
                }
                break;
            }
        }
    }

    public void DecryptionFinished(string url)
    {
        videoUrl = url;
        videoAreReadyToPlay = true;
    }

    bool checkIfVideoArePrepared = false;
    void FixedUpdate()
    {
        //used this to play in main thread.
        if (videoAreReadyToPlay)
        {
            videoAreReadyToPlay = false;
            //play using the old method
            if (!useNewUnityPlayer)
                StartHandheldVideo();
            else
            {
                Debug.Log("Play!!" + videoUrl);
                unityVideoPlayer.source = VideoSource.Url;
                unityVideoPlayer.url = videoUrl;
                checkIfVideoArePrepared = true;
                unityVideoPlayer.Prepare();
            }
        }

        if (checkIfVideoArePrepared)
        {
            checkIfVideoArePrepared = false;
            StartCoroutine(PreparingAudio());
        }
    }


    IEnumerator PreparingAudio()
    {
        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!unityVideoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            //Prepare/Wait for 5 sceonds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }

        Debug.Log("Done Preparing Video");

        //Play Video
        unityVideoPlayer.Play();

        //Play Sound
        unityVideoPlayer.Play();

        Debug.Log("Playing Video");
        while (unityVideoPlayer.isPlaying)
        {
            yield return null;
        }
        OnVideoFinished();
    }

    public void Play()
    {
        unityVideoPlayer.Play();
    }



    void StartHandheldVideo()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        HandheldPlayback deviceplayer = gameObject.AddComponent<HandheldPlayback>();
        deviceplayer.PlayVideo(videoUrl, OnVideoFinished);
#endif
    }

    public void OnVideoFinished()
    {
        Debug.Log("Video finished");
    }


    public void Play_Pause()
    {
        if (unityVideoPlayer.isPlaying)
            unityVideoPlayer.Pause();
        else
            unityVideoPlayer.Play();
    }

    public void PlayerPause()
    {
        unityVideoPlayer.Pause();
    }

    public void PlayerPlay()
    {
        unityVideoPlayer.Play();
    }

    public void RetryPlayback()
    {
        currentRetry++;
        if (currentRetry < maxRetryUntilToGetFromWebServer)
        {
            PlayYoutubeVideo(videoId);
        }
        else
        {
            Debug.Log("Trying to get from webserver");
            getFromWebServer = true;
            PlayYoutubeVideo(videoId);
        }
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
        videoAreReadyToPlay = true;
    }

}
