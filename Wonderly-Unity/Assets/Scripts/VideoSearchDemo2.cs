using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sample;

public class VideoSearchDemo2 : MonoBehaviour {
    YoutubeAPIManager youtubeapi;

    public Text searchField;
    public YoutubeVideoUi[] videoListUI;
    //public GameObject videoUIResult;
    //public GameObject mainUI;
    public GameObject vidReference1;
    public GameObject vidReference2;
    public GameObject vidReference3;
    public GameObject vidReference4;
    public GameObject vidReference5;
    public GameObject targetReference1;
    public GameObject targetReference2;
    public GameObject targetReference3;
    public GameObject targetReference4;
    public GameObject targetReference5;
    public FilesManager fm;
    public List<string> thumbUrls = new List<string>();


    void Start () {
        //Get the api component
        youtubeapi = GameObject.FindObjectOfType<YoutubeAPIManager>();
        if (youtubeapi == null)
        {
            youtubeapi = gameObject.AddComponent<YoutubeAPIManager>();
        }
	}
	
	public void Search()
    {
        thumbUrls.Clear();
        //turn on target's video player
        switch(fm.currentTarget)
        {
            case 0:
                return;
            case 1:
                vidReference1.SetActive(true);
                break;
            case 2:
                vidReference2.SetActive(true);
                break;
            case 3:
                vidReference3.SetActive(true);
                break;
            case 4:
                vidReference4.SetActive(true);
                break;
            case 5:
                vidReference5.SetActive(true);
                break;
        }
        //do nothing if no targets created yet or if indexed target not created yet
        if (fm.targetStatus[fm.currentTarget-1] == "none")
            return;

        YoutubeAPIManager.YoutubeSearchOrderFilter mainFilter = YoutubeAPIManager.YoutubeSearchOrderFilter.none;
       
        youtubeapi.Search(searchField.text, 18, mainFilter, YoutubeAPIManager.YoutubeSafeSearchFilter.none, OnSearchDone);
    }

    public void SearchByLocation(string location)
    {
        YoutubeAPIManager.YoutubeSearchOrderFilter mainFilter = YoutubeAPIManager.YoutubeSearchOrderFilter.none;
    
        string[] splited = location.Split(',');
        float latitude = float.Parse(splited[0]);
        float longitude = float.Parse(splited[1]);
        int locationRadius = 10;
        youtubeapi.SearchByLocation(searchField.text, 10, locationRadius, latitude, longitude, mainFilter, YoutubeAPIManager.YoutubeSafeSearchFilter.none, OnSearchDone);
    }

    void OnSearchDone(YoutubeData[] results)
    {
        //videoUIResult.SetActive(true);
        LoadVideosOnUI(results);
    }

    void LoadVideosOnUI(YoutubeData[] videoList)
    {
        for (int x = 0; x < videoList.Length; x++)
        {
            //videoListUI[x].GetComponent<YoutubeVideoUi>().videoName.text = videoList[x].snippet.title;
            videoListUI[x].GetComponent<YoutubeVideoUi>().videoId = videoList[x].id;
            videoListUI[x].GetComponent<YoutubeVideoUi>().thumbUrl = videoList[x].snippet.thumbnails.defaultThumbnail.url;
            thumbUrls.Add(videoList[x].snippet.thumbnails.defaultThumbnail.url);
            videoListUI[x].GetComponent<YoutubeVideoUi>().LoadThumbnail();
        }
    }

}
