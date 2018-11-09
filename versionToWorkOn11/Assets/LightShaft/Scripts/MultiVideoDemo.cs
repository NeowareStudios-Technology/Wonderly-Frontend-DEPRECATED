using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using YoutubeLight;
using SimpleJSON;

public class MultiVideoDemo : MonoBehaviour {
	
	public string videoId = "bc0sJvtKrRM";
	private string videoUrl;
	private bool videoAreReadyToPlay = false;
	//use unity player(all platforms) or old method to play in mobile only if you want, or if your mobile dont support the new player.
	public bool useNewUnityPlayer;
	public VideoPlayer unityVideoPlayer;
	public GameObject[] objectsToPlayTheSameVIdeo;
	//start playing the video
	public bool playOnStart = false;
    public bool getFromWebServer = false;
    RequestResolver resolver;

	public void Start(){
        resolver = gameObject.AddComponent<RequestResolver>();
		if (playOnStart) {
			PlayYoutubeVideo (videoId);
		}
	}

	public void PlayYoutubeVideo(string _videoId)
	{
		videoId = _videoId;
        if (!getFromWebServer)
            StartCoroutine(resolver.GetDownloadUrls(FinishLoadingUrls, videoId, false));
        else
            StartCoroutine(NewRequest(videoId));
        
    }

	//this will run only in another thread.
	void FinishLoadingUrls()
	{
		List<VideoInfo> videoInfos = resolver.videoInfos;
		foreach (VideoInfo info in videoInfos)
		{
			if (info.VideoType == VideoType.Mp4 && info.Resolution == 360) {
				if (info.RequiresDecryption) {
					//The string is the video url
					StartCoroutine(resolver.DecryptDownloadUrl (DecryptDone,info));
				} else {
					videoUrl = info.DownloadUrl;
                    videoAreReadyToPlay = true;
                }
				break;
			}
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

    public void DecryptDone(string url)
    {
        videoUrl = url;
        videoAreReadyToPlay = true;
    }

	bool checkIfVideoArePrepared = false;
	void FixedUpdate(){
		//used this to play in main thread.
		if (videoAreReadyToPlay) {
			videoAreReadyToPlay = false;
			//play using the old method
			if (!useNewUnityPlayer)
				StartCoroutine (StartVideo ());
			else {
				Debug.Log ("Play!!" + videoUrl);
				unityVideoPlayer.source = VideoSource.Url;
				unityVideoPlayer.url = videoUrl;
				unityVideoPlayer.Prepare ();
				checkIfVideoArePrepared = true;
			}
		}

		if (checkIfVideoArePrepared) {
			if (unityVideoPlayer.isPrepared) {
				//can play
				//unityVideoPlayer.Play();
				checkIfVideoArePrepared = false;
				StartCoroutine (PreparingAudio());
			}
		}
	}

	IEnumerator PreparingAudio(){
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
		//set the other materials to with the video texture.
		foreach (GameObject obj in objectsToPlayTheSameVIdeo) {
			obj.GetComponent<Renderer> ().material.mainTexture = unityVideoPlayer.texture;
		}

		//Play Sound
		unityVideoPlayer.GetComponent<AudioSource>().Play();

		while (unityVideoPlayer.isPlaying)
		{
			yield return null;
		}
		OnVideoFinished ();
	}

	IEnumerator StartVideo(){
		#if UNITY_IPHONE || UNITY_ANDROID
		Handheld.PlayFullScreenMovie (videoUrl);
		#endif
		yield return new WaitForSeconds (1.4f);
		OnVideoFinished ();
	}

	public void OnVideoFinished(){
		Debug.Log ("Video finished");
	}


}
