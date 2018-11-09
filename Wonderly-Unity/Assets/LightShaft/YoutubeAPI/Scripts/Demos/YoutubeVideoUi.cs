using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Sample;
//using Vuforia;

public class YoutubeVideoUi : MonoBehaviour {

    public Text videoName;
    public string videoId,thumbUrl;
    public UnityEngine.UI.Image videoThumb;
    private GameObject mainUI;
    public FilesManager fm;
    public targetObjectManager tom;
    public ImageTargetManager itm;

    public void PlayYoutubeVideo()
    {
            string videoPlayerString = "";
            switch(fm.currentTarget)
            {
                case 0:
                    return;
                case 1:
                    //if the target is not created yet, do not play video
                    if (fm.targetStatus[0] == "none")
                    {
                        return;
                    }
                    fm.targetStatus[0] = "video";
                    itm.target1.SetActive(true);
                    tom.videoPlayer1.SetActive(true);
                    Debug.Log(videoId);
                    tom.videoPlayer1.GetComponent<SimplePlayback>().PlayYoutubeVideo(videoId);
                    tom.videoPlayer1.GetComponent<SimplePlayback>().unityVideoPlayer.loopPointReached += VideoFinished;
                    break;
                case 2:
                    if (fm.targetStatus[1] == "none")
                    {
                        return;
                    }
                    fm.targetStatus[1] = "video";
                    itm.target2.SetActive(true);
                    tom.videoPlayer2.SetActive(true);
                    Debug.Log(videoId);
                    tom.videoPlayer2.GetComponent<SimplePlayback>().PlayYoutubeVideo(videoId);
                    tom.videoPlayer2.GetComponent<SimplePlayback>().unityVideoPlayer.loopPointReached += VideoFinished;
                    break;
                case 3:
                    if (fm.targetStatus[2] == "none")
                    {
                        return;
                    }
                    fm.targetStatus[2] = "video";
                    itm.target3.SetActive(true);
                    tom.videoPlayer3.SetActive(true);
                    Debug.Log(videoId);
                    tom.videoPlayer3.GetComponent<SimplePlayback>().PlayYoutubeVideo(videoId);
                    tom.videoPlayer3.GetComponent<SimplePlayback>().unityVideoPlayer.loopPointReached += VideoFinished;
                    break;
                case 4:
                    if (fm.targetStatus[3] == "none")
                    {
                        return;
                    }
                    fm.targetStatus[3] = "video";
                    itm.target4.SetActive(true);
                    tom.videoPlayer4.SetActive(true);
                    Debug.Log(videoId);
                    tom.videoPlayer4.GetComponent<SimplePlayback>().PlayYoutubeVideo(videoId);
                    tom.videoPlayer4.GetComponent<SimplePlayback>().unityVideoPlayer.loopPointReached += VideoFinished;
                    break;
                case 5:
                    if (fm.targetStatus[4] == "none")
                    {
                        return;
                    }
                    fm.targetStatus[4] = "video";
                    itm.target5.SetActive(true);
                    tom.videoPlayer5.SetActive(true);
                    Debug.Log(videoId);
                    tom.videoPlayer5.GetComponent<SimplePlayback>().PlayYoutubeVideo(videoId);
                    tom.videoPlayer5.GetComponent<SimplePlayback>().unityVideoPlayer.loopPointReached += VideoFinished;
                    break;


            }
            //GameObject.FindObjectOfType<VideoSearchDemo2>().vidReference1.GetComponent<HighQualityPlayback>().PlayYoutubeVideo(videoId);
            //GameObject.FindObjectOfType<VideoSearchDemo2>().vidReference1.GetComponent<HighQualityPlayback>().unityVideoPlayer.loopPointReached += VideoFinished;

    }

    private void VideoFinished(VideoPlayer vPlayer)
    {
        if (GameObject.FindObjectOfType<SimplePlayback>() != null)
        {
            GameObject.FindObjectOfType<SimplePlayback>().unityVideoPlayer.loopPointReached -= VideoFinished;
        }
        else if (GameObject.FindObjectOfType<HighQualityPlayback>() != null)
        {
            GameObject.FindObjectOfType<HighQualityPlayback>().unityVideoPlayer.loopPointReached -= VideoFinished;
        }
        
        Debug.Log("Video Finished");
        mainUI.SetActive(true);
    }

    public IEnumerator PlayVideo(string url)
    {
#if UNITY_ANDROID || UNITY_IOS
        yield return Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill);
#else
        yield return true;
#endif
        Debug.Log("below this line will run when the video is finished");
        VideoFinished();
    }

    public void LoadThumbnail()
    {
        StartCoroutine(DownloadThumb());
    }

    IEnumerator DownloadThumb()
    {
        WWW www = new WWW(thumbUrl);
        yield return www;
        Texture2D thumb = new Texture2D(100, 100);
        www.LoadImageIntoTexture(thumb);
        videoThumb.sprite = Sprite.Create(thumb, new Rect(0, 0, thumb.width, thumb.height), new Vector2(0.5f, 0.5f), 100);
    }

    public void VideoFinished()
    {
        Debug.Log("Finished!");
    }
}
