using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubeLight;
using SimpleJSON;
using System.Text;
using System;

public class HighQualityPlayback : MonoBehaviour
{

    [Header("Max time to wait for the video")]
    public int maxSecondsToWait = 5;
    float currentWaitTime;
    int maxRetryUntilToGetFromWebServer = 1;
    int currentRetry = 0;
    bool startedToGetUrl = false;

    [Header("Enable to use new experimental version")]
    public bool getFromWebserver = false;
    public string videoId = "bc0sJvtKrRM";
    public VideoQuality videoQuality;
    public string videoUrl;
    public string audioVideoUrl;
    private bool videoAreReadyToPlay = false;

    //If you will use high quality playback we need one video player that only will run the audio.
    public VideoPlayer unityVideoPlayer;
    //start playing the video
    public bool playOnStart = false;

    [HideInInspector]
    public bool noHD = false;

    RequestResolver resolver;

    public void Start()
    {
        if (videoQuality == VideoQuality.Hd720)
            noHD = true;
#if UNITY_WEBGL
        getFromWebserver = true;
#endif

        unityVideoPlayer.started += VideoStarted;
        unityVideoPlayer.errorReceived += VideoErrorReceived;
        unityVideoPlayer.frameDropped += VideoFrameDropped;

        resolver = gameObject.AddComponent<RequestResolver>();
        if (videoQuality == VideoQuality.Hd720)
            audioVplayer.audioOutputMode = VideoAudioOutputMode.None;
        //if (Application.isMobilePlatform)
        //{
        //    if (GetMaxQualitySupportedByDevice() <= 720)
        //    {
        //        //low end device..
        //        if(videoQuality != VideoQuality.mediumQuality)
        //            videoQuality = VideoQuality.Hd720;
        //        noHD = true;
        //    }
        //}

        if (playOnStart)
        {
            PlayYoutubeVideo(videoId);
        }
    }

    float lastPlayTime;
    public void PlayYoutubeVideo(string _videoId)
    {
        resolver = gameObject.AddComponent<RequestResolver>();
        if (this.GetComponent<VideoController>() != null)
        {
            this.GetComponent<VideoController>().ShowLoading("Loading...");
        }
        videoId = _videoId;
        isRetry = false;
        lastTryQuality = videoQuality;
        lastTryVideoId = _videoId;
        lastPlayTime = Time.time;
        lastVideoReadyToPlay = 0;

#if UNITY_WEBGL
        StartCoroutine(NewRequest(videoId));
#else
        if (!getFromWebserver)
        {
            currentWaitTime = 0;
            startedToGetUrl = true;
            StartCoroutine(resolver.GetDownloadUrls(FinishLoadingUrls, videoId, false));
        }
        else
            StartCoroutine(NewRequest(videoId));
#endif

    }

    private bool audioDecryptDone = false;
    private bool videoDecryptDone = false;


    void FinishLoadingUrls()
    {
        startedToGetUrl = false;
        List<VideoInfo> videoInfos = resolver.videoInfos;
        videoDecryptDone = false;
        audioDecryptDone = false;
        //Get the video with audio first
        foreach (VideoInfo info in videoInfos)
        {
            if (info.VideoType == VideoType.Mp4 && info.Resolution == (360))
            {
                
                if (info.RequiresDecryption)
                {
                    //The string is the video url with audio
                    StartCoroutine(resolver.DecryptDownloadUrl(DecryptAudioDone, info));
                }
                else
                {
                    audioVideoUrl = info.DownloadUrl;
                }
                break;
            }
        }

        int quality = 360;
        switch (videoQuality)
        {
            case VideoQuality.SD360:
                quality = 360;
                break;
            case VideoQuality.SD480:
                quality = 480;
                break;
            case VideoQuality.Hd720:
                quality = 720;
                break;
            case VideoQuality.Hd1080:
                quality = 1080;
                break;
            case VideoQuality.Hd1440:
                quality = 1440;
                break;
            case VideoQuality.Hd2160:
                quality = 2160;
                break;
        }

        bool foundVideo = false;
        //Get the high quality video
        foreach (VideoInfo info in videoInfos)
        {
            if (info.VideoType == VideoType.Mp4 && info.Resolution == (quality))
            {
                if (info.RequiresDecryption)
                {
                    Debug.Log("REQUIRE DECRYPTION!");
                    //The string is the video url
                    StartCoroutine(resolver.DecryptDownloadUrl(DecryptVideoDone, info));
                }
                else
                {
                    videoUrl = info.DownloadUrl;
                    videoAreReadyToPlay = true;
                }
                foundVideo = true;
                //videoAreReadyToPlay = true;
                break;
            }
        }

        if (!foundVideo && quality == 2160)
        {
            foreach (VideoInfo info in videoInfos)
            {
                if (info.FormatCode == 313)
                {
                    Debug.Log("Found but with unknow format in results, check to see if the video works normal.");
                    if (info.RequiresDecryption)
                    {
                        //The string is the video url
                        StartCoroutine(resolver.DecryptDownloadUrl(DecryptVideoDone, info));
                    }
                    else
                    {
                        videoUrl = info.DownloadUrl;
                        videoAreReadyToPlay = true;
                    }
                    foundVideo = true;
                    //videoAreReadyToPlay = true;
                    break;
                }
            }
        }

        //if desired quality not found try another lower quality.
        if (!foundVideo)
        {
            Debug.Log("Desired quality not found, playing with low quality, check if the video id: " + videoId + " support that quality!");
            foreach (VideoInfo info in videoInfos)
            {
                if (info.VideoType == VideoType.Mp4 && info.Resolution == (360))
                {
                    if (info.RequiresDecryption)
                    {
                        //The string is the video url
                        StartCoroutine(resolver.DecryptDownloadUrl(DecryptVideoDone, info));
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
    }

    public void DecryptAudioDone(string url)
    {
        audioVideoUrl = url;
        audioDecryptDone = true;

        if (videoDecryptDone)
            videoAreReadyToPlay = true;
    }

    public void DecryptVideoDone(string url)
    {
        videoUrl = url;
        videoDecryptDone = true;

        if (audioDecryptDone)
            videoAreReadyToPlay = true;
    }

    public VideoPlayer audioVplayer;
    bool checkIfVideoArePrepared = false;

    float lastVideoReadyToPlay = 0;

    void FixedUpdate()
    {
        if (startedToGetUrl)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= maxSecondsToWait)
            {
                startedToGetUrl = false;
                Debug.Log("<color=blue>Max time reached, trying again!</color>");
                Debug.Break();
            }
        }
        //used this to play in main thread.
        if (videoAreReadyToPlay)
        {
            videoAreReadyToPlay = false;
            lastVideoReadyToPlay = Time.time;
            //play using the old method

            Debug.Log("Play!!" + videoUrl);
            lastVideoReadyToPlay = Time.time;
            unityVideoPlayer.source = VideoSource.Url;
            unityVideoPlayer.url = videoUrl;
            checkIfVideoArePrepared = true;
            unityVideoPlayer.Prepare();
            if (!noHD)
            {
                audioVplayer.source = VideoSource.Url;
                audioVplayer.url = audioVideoUrl;
                audioVplayer.Prepare();
            }
        }

        if (checkIfVideoArePrepared)
        {
            checkIfVideoArePrepared = false;
            videoPrepared = false;
            unityVideoPlayer.prepareCompleted += VideoPrepared;
            if (!noHD)
            {
                audioPrepared = false;
                audioVplayer.prepareCompleted += AudioPrepared;
            }


        }

        CheckIfIsDesync();
    }

    private bool videoPrepared;
    private bool audioPrepared;

    void AudioPrepared(VideoPlayer vPlayer)
    {
        audioVplayer.prepareCompleted -= AudioPrepared;
        audioPrepared = true;
        if (audioPrepared && videoPrepared)
            Play();
    }

    void VideoPrepared(VideoPlayer vPlayer)
    {
        unityVideoPlayer.prepareCompleted -= VideoPrepared;
        videoPrepared = true;
        if (noHD)
        {
            Play();
        }
        else
        {
            if (audioPrepared && videoPrepared)
                Play();
        }

    }

    public void Play()
    {
        unityVideoPlayer.loopPointReached += PlaybackDone;
        StartCoroutine(WaitAndPlay());
    }

    private void PlaybackDone(VideoPlayer vPlayer)
    {
        OnVideoFinished();
    }

    IEnumerator WaitAndPlay()
    {

        if (!noHD)
        {
            audioVplayer.Play();
            if (syncIssue)
                yield return new WaitForSeconds(0.35f);
            else
                yield return new WaitForSeconds(0);
        }
        else
        {
            if (syncIssue)
                yield return new WaitForSeconds(1f);//if is no hd wait some more
            else
                yield return new WaitForSeconds(0);
        }

        unityVideoPlayer.Play();
        if (this.GetComponent<VideoController>() != null)
        {
            this.GetComponent<VideoController>().HideLoading();
        }
    }

    void StartHandheldVideo()
    {
        Debug.Log("P");
#if UNITY_IPHONE || UNITY_ANDROID
        HandheldPlayback deviceplayer = gameObject.AddComponent<HandheldPlayback>();
        deviceplayer.PlayVideo(videoUrl, OnVideoFinished);
#endif
    }

    public void OnVideoFinished()
    {
        if (unityVideoPlayer.isPrepared)
        {
            Debug.Log("Finished");
            if (unityVideoPlayer.isLooping)
            {
                unityVideoPlayer.time = 0;
                unityVideoPlayer.frame = 0;
                audioVplayer.time = 0;
                audioVplayer.frame = 0;
                unityVideoPlayer.Play();
                audioVplayer.Play();
            }
        }
    }

    public enum VideoQuality
    {
        SD360,
        SD480,
        Hd720,
        Hd1080,
        Hd1440,
        Hd2160
    }

    [HideInInspector]
    public bool isSyncing = false;
    [Header("If you think audio is out of sync enable this bool below")]
    [Header("This happens in some unity versions, the most stable is the 5.6.1p1")]
    public bool syncIssue;

    //Experimental
    private void CheckIfIsDesync()
    {
        if (!getFromWebserver)
        {
            if (!isRetry && lastStartedTime < lastErrorTime && lastErrorTime > lastPlayTime)
            {
                Debug.Log("Error detected!, retry with low quality!");
                lastTryQuality = VideoQuality.SD360;
                RetryPlayYoutubeVideo();
            }
        }
        //if (!noHD)
        //{
        //    //Debug.Log(unityVideoPlayer.time+" "+ audioVplayer.time);
        //    double t = unityVideoPlayer.time - audioVplayer.time;
        //    if (!isSyncing)
        //    {
        //        if (t != 0)
        //        {
        //            Sync();
        //        }
        //        else if (unityVideoPlayer.frame != audioVplayer.frame)
        //        {
        //            Sync();
        //        }
        //    }
        //}
        //else
        //{
        //    //unityVideoPlayer.frame -= 1;
        //}

    }

    private void Sync()
    {
        VideoController controller = GameObject.FindObjectOfType<VideoController>();
        if (controller != null)
        {
            //isSyncing = true;
            //audioVplayer.time = unityVideoPlayer.time;
            //audioVplayer.frame = unityVideoPlayer.frame;
            //controller.Seek();
        }
        else
        {
            Debug.LogWarning("Please add a video controller to your scene to make the sync work! Will be improved in the future.");
        }
    }

    public int GetMaxQualitySupportedByDevice()
    {
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            //use the height
            return Screen.currentResolution.height;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            //use the width
            return Screen.currentResolution.width;
        }
        else
        {
            return Screen.currentResolution.height;
        }
    }

    [SerializeField]
    public YoutubeResultIds newRequestResults;
    /*PRIVATE INFO DO NOT CHANGE THESE URLS OR VALUES*/
    private const string serverURI = "https://unity-dev-youtube.herokuapp.com/api/info?url=https://www.youtube.com/watch?v=";
    private const string formatURI = "&format=best&flatten=true";
    private const string VIDEOURIFORWEBGLPLAYER = "https://youtubewebgl.herokuapp.com/download.php?mime=video/mp4&title=generatedvideo&token=";
    /*END OF PRIVATE INFO*/
    IEnumerator NewRequest(string videoID)
    {
        WWW request = new WWW(serverURI + "" + videoID + "" + formatURI);
        yield return request;
        var requestData = JSON.Parse(request.text);
        var videos = requestData["videos"][0]["formats"];
        newRequestResults.bestFormatWithAudioIncluded = requestData["videos"][0]["url"];
        for (int counter = 0; counter < videos.Count; counter++)
        {
            if (videos[counter]["format_id"] == "160")
            {
                newRequestResults.lowQuality = videos[counter]["url"];
            }
            else if (videos[counter]["format_id"] == "133")
            {
                newRequestResults.lowQuality = videos[counter]["url"];   //if have 240p quality overwrite the 144 quality as low quality.
            }
            else if (videos[counter]["format_id"] == "134")
            {
                newRequestResults.standardQuality = videos[counter]["url"];  //360p
            }
            else if (videos[counter]["format_id"] == "135")
            {
                newRequestResults.mediumQuality = videos[counter]["url"];  //480p
            }
            else if (videos[counter]["format_id"] == "136")
            {
                newRequestResults.hdQuality = newRequestResults.bestFormatWithAudioIncluded;  //720p
            }
            else if (videos[counter]["format_id"] == "137")
            {
                newRequestResults.fullHdQuality = videos[counter]["url"];  //1080p
            }
            else if (videos[counter]["format_id"] == "266")
            {
                newRequestResults.ultraHdQuality = videos[counter]["url"];  //@2160p 4k
            }
            else if (videos[counter]["format_id"] == "139")
            {
                newRequestResults.audioUrl = videos[counter]["url"];  //AUDIO
            }
        }

        audioVideoUrl = newRequestResults.bestFormatWithAudioIncluded;
        int quality = 360;
        videoUrl = newRequestResults.lowQuality;
        switch (videoQuality)
        {
            case VideoQuality.SD360:
                videoUrl = newRequestResults.mediumQuality;
                break;
            case VideoQuality.Hd720:
                videoUrl = newRequestResults.hdQuality;
                break;
            case VideoQuality.Hd1080:
                videoUrl = newRequestResults.fullHdQuality;
                break;
        }
#if UNITY_WEBGL
        videoUrl = ConvertToWebglUrl(videoUrl);
        audioVideoUrl = ConvertToWebglUrl(audioVideoUrl);

#endif
        videoAreReadyToPlay = true;
    }


    private string ConvertToWebglUrl(string url)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(url);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        Debug.Log(url);
        string newUrl = VIDEOURIFORWEBGLPLAYER + "" + encodedText;
        return newUrl;
    }


    //Error detection implementation 

    bool isRetry = false;
    VideoQuality lastTryQuality = VideoQuality.Hd2160;
    string lastTryVideoId;
    public void RetryPlayYoutubeVideo()
    {
        currentRetry++;
        if(currentRetry < maxRetryUntilToGetFromWebServer)
        {
            if (!getFromWebserver)
            {
                StopIfPlaying();
                Debug.Log("Youtube Retrying...:" + lastTryVideoId);
                isRetry = true;
                if (this.GetComponent<VideoController>() != null)
                {
                    this.GetComponent<VideoController>().ShowLoading("Loading...");
                }
                videoId = lastTryVideoId;
                PlayYoutubeVideo(videoId);
            }
        }
        else
        {
            currentRetry = 0;
            Debug.Log("Playing From WEbServer. There's a error in the local player.");
            //Get from webserver becuase there's something wrong with the offline system.
            StopIfPlaying();
            if (this.GetComponent<VideoController>() != null)
            {
                this.GetComponent<VideoController>().ShowLoading("Loading...");
            }
            getFromWebserver = true;
            videoId = lastTryVideoId;
            PlayYoutubeVideo(videoId);
        }
        
    }

    private void StopIfPlaying()
    {
        Debug.Log("Stopping video");
        if (unityVideoPlayer.isPlaying) { unityVideoPlayer.Stop(); }
        if (audioVplayer.isPlaying) { audioVplayer.Stop(); }
    }

    private void VideoFrameDropped(VideoPlayer source)
    {
        Debug.Log("Youtube VideoFrameDropped!"); //[NOT IMPLEMENTED UNITY 2017.2]
    }

    float lastStartedTime;
    private void VideoStarted(VideoPlayer source)
    {
        lastStartedTime = Time.time;
        lastErrorTime = lastStartedTime;
        Debug.Log("Youtube Video Started");
    }

    float lastErrorTime;
    private void VideoErrorReceived(VideoPlayer source, string message)
    {
        lastErrorTime = Time.time;
        Debug.Log("Youtube VideoErrorReceived!:" + message);
    }



}
