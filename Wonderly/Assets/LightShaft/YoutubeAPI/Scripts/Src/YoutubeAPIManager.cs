﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System;
using Sample;

public class YoutubeAPIManager : MonoBehaviour {

    public FilesManager fm;
    private YoutubeData data;
    private YoutubeData[] searchResults;
    private YoutubeComments[] comments;
    private YoutubePlaylistItems[] playslistItems;
    private YoutubeChannel[] channels;
    public List<string> YoutubeTitles = new List<string>();
    public GameObject chosenDisplay1;
    public GameObject chosenDisplay2;
    public GameObject chosenDisplay3;
    public GameObject chosenDisplay4;
    public GameObject chosenDisplay5;

    //REMEMBER TO CHANGE HERE IF YOU NEED TO POINT TO YOUR GOOGLE APP. 
    /* 
     * TO CREATE YOUR GOOGLE APP AND USE YOUR API GO TO:
     * https://code.google.com/apis/console
     * -Create a project.
     * -Go to APIs -> YouTube APIs -> YouTube Data API and enable that.
     * -then go to credentials create a new key for Public API access
     * - now you have the API key just copy and change the variable APIKey with your new API Key to monitor the use of youtube api calls.
     * Any question? mail me: kelvinparkour@gmail.com
     * 
     * */

    private const string APIKey = "AIzaSyCfDvywYv5hI109dr7YF8OPHXc5fOIa0UM";

    public void SetVideoInfo(int whichVideo)
    {
        GameObject chosenDisplay = chosenDisplay1;
        switch(fm.currentTarget)
        {
            case 0:
                return;
            case 1:
                chosenDisplay = chosenDisplay1;
                break;
            case 2:
                chosenDisplay = chosenDisplay2;
                break;
            case 3:
                chosenDisplay = chosenDisplay3;
                break;
            case 4:
                chosenDisplay = chosenDisplay4;
                break;
            case 5:
                chosenDisplay = chosenDisplay5;
                break;

        }

        //set the chosen video title
        switch(whichVideo)
        {
            case 1:
                //set chosen display video text to correct youtube title
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[0];
                break;
            case 2:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[1];
                break;
            case 3:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[2];
                break;
            case 4:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[3];
                break;
            case 5:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[4];
                break;
            case 6:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[5];
                break;
            case 7:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[6];
                break;
            case 8:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[7];
                break;
            case 9:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[8];
                break;
            case 10:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[9];
                break;
            case 11:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[10];
                break;
            case 12:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[11];
                break;
            case 13:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[12];
                break;
            case 14:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[13];
                break;
            case 15:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[14];
                break;
            case 16:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[15];
                break;
            case 17:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[16];
                break;
            case 18:
                chosenDisplay.transform.GetChild(1).gameObject.GetComponent<Text>().text = YoutubeTitles[17];
                break;

        }
    }

    public void GetVideoData(string videoId, Action<YoutubeData> callback)
    {
        StartCoroutine(LoadSingleVideo(videoId, callback));
    }

    private void Start()
    {
        Debug.LogWarning("REMEMBER TO CHANGE THE API KEY TO YOUR OWN KEY - REMOVE THIS IF YOU CHANGED");
    }


    public void GetChannelVideos(string channelId, int maxResults, Action<YoutubeData[]> callback)
    {
        StartCoroutine(GetVideosFromChannel(channelId, maxResults,callback));
    }

    public void Search(string keyword, int maxResult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        StartCoroutine(YoutubeSearch(keyword, maxResult, order, safeSearch, callback));
    }

    public void SearchForChannels(string keyword, int maxResult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeChannel[]> callback)
    {
        StartCoroutine(YoutubeSearchChannel(keyword, maxResult, order, safeSearch, callback));
    }

    public void SearchByCategory(string keyword, string category, int maxResult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        StartCoroutine(YoutubeSearchUsingCategory(keyword, category, maxResult, order, safeSearch, callback));
    }

    public void SearchByLocation(string keyword, int maxResult,int locationRadius, float latitude, float longitude, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        StartCoroutine(YoutubeSearchByLocation(keyword, maxResult, locationRadius, latitude, longitude, order, safeSearch, callback));
    }

    public void GetComments(string videoId, Action<YoutubeComments[]> callback)
    {
        StartCoroutine(YoutubeCallComments(videoId, callback));
    }

    public void GetPlaylistItems(string playlistId,int maxResults, Action<YoutubePlaylistItems[]> callback)
    {
        StartCoroutine(YoutubeCallPlaylist(playlistId, maxResults, callback));
    }

    IEnumerator GetVideosFromChannel(string channelId, int maxResults, Action<YoutubeData[]> callback)
    {
        WWW call = new WWW("https://www.googleapis.com/youtube/v3/search?order=date&type=video&part=snippet&channelId="+channelId+"&maxResults="+maxResults+"&key="+APIKey);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        searchResults = new YoutubeData[result["items"].Count];
        for (int itemsCounter = 0; itemsCounter < searchResults.Length; itemsCounter++)
        {
            searchResults[itemsCounter] = new YoutubeData();
            searchResults[itemsCounter].id = result["items"][itemsCounter]["id"]["videoId"];
            SetSnippet(result["items"][itemsCounter]["snippet"], out searchResults[itemsCounter].snippet);
        }
        callback.Invoke(searchResults);
    }

    IEnumerator YoutubeCallPlaylist(string playlistId,int maxResults, Action<YoutubePlaylistItems[]> callback)
    {
        WWW call = new WWW("https://www.googleapis.com/youtube/v3/playlistItems/?playlistId="+ playlistId + "&maxResults="+maxResults+"&part=snippet%2CcontentDetails&key="+APIKey);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        playslistItems = new YoutubePlaylistItems[result["items"].Count];
        for (int itemsCounter = 0; itemsCounter < playslistItems.Length; itemsCounter++)
        {
            playslistItems[itemsCounter] = new YoutubePlaylistItems();
            playslistItems[itemsCounter].videoId = result["items"][itemsCounter]["snippet"]["resourceId"]["videoId"];
            SetSnippet(result["items"][itemsCounter]["snippet"], out playslistItems[itemsCounter].snippet);
        }
        callback.Invoke(playslistItems);
    }

    IEnumerator YoutubeCallComments(string videoId, Action<YoutubeComments[]> callback)
    {
        WWW call = new WWW("https://www.googleapis.com/youtube/v3/commentThreads/?videoId="+videoId+"&part=snippet%2Creplies&key="+APIKey);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        comments = new YoutubeComments[result["items"].Count];
        for (int itemsCounter = 0; itemsCounter < comments.Length; itemsCounter++)
        {
            comments[itemsCounter] = new YoutubeComments();
            SetComment(result["items"][itemsCounter]["snippet"], out comments[itemsCounter]);
        }
        callback.Invoke(comments);
    }

    IEnumerator YoutubeSearchUsingCategory(string keyword, string category, int maxresult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        keyword = keyword.Replace(" ", "%20");
        category = category.Replace(" ", "%20");

        string orderFilter, safeSearchFilter;
        orderFilter = "";
        if (order != YoutubeSearchOrderFilter.none)
        {
            orderFilter = "&order=" + order.ToString();
        }
        safeSearchFilter = "&safeSearch=" + safeSearch.ToString();


        WWW call = new WWW("https://www.googleapis.com/youtube/v3/search/?q=" + keyword + "&category=" + category + "&maxResults=" + maxresult + "&type=video&part=snippet,id&key=" + APIKey + "" + orderFilter + "" + safeSearchFilter);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        searchResults = new YoutubeData[result["items"].Count];
        Debug.Log(searchResults.Length);
        for (int itemsCounter = 0; itemsCounter < searchResults.Length; itemsCounter++)
        {
            searchResults[itemsCounter] = new YoutubeData();
            searchResults[itemsCounter].id = result["items"][itemsCounter]["id"]["videoId"];
            SetSnippet(result["items"][itemsCounter]["snippet"], out searchResults[itemsCounter].snippet);
        }
        callback.Invoke(searchResults);
    }

    IEnumerator YoutubeSearchChannel(string keyword, int maxresult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeChannel[]> callback)
    {
        keyword = keyword.Replace(" ", "%20");

        string orderFilter, safeSearchFilter;
        orderFilter = "";
        if (order != YoutubeSearchOrderFilter.none)
        {
            orderFilter = "&order=" + order.ToString();
        }
        safeSearchFilter = "&safeSearch=" + safeSearch.ToString();


        WWW call = new WWW("https://www.googleapis.com/youtube/v3/search/?q=" + keyword + "&type=channel&maxResults=" + maxresult + "&part=snippet,id&key=" + APIKey + "" + orderFilter + "" + safeSearchFilter);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        channels = new YoutubeChannel[result["items"].Count];
        for (int itemsCounter = 0; itemsCounter < channels.Length; itemsCounter++)
        {
            channels[itemsCounter] = new YoutubeChannel();
            channels[itemsCounter].id = result["items"][itemsCounter]["id"]["channelId"];
            channels[itemsCounter].title = result["items"][itemsCounter]["snippet"]["title"];
            channels[itemsCounter].description = result["items"][itemsCounter]["snippet"]["description"];
            channels[itemsCounter].thumbnail = result["items"][itemsCounter]["snippet"]["thumbnails"]["high"]["url"];
        }
        callback.Invoke(channels);
    }

    IEnumerator YoutubeSearch(string keyword,int maxresult, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        keyword = keyword.Replace(" ", "%20");

        string orderFilter,safeSearchFilter;
        orderFilter = "";
        if (order != YoutubeSearchOrderFilter.none)
        {
            orderFilter = "&order="+order.ToString();
        }
        safeSearchFilter = "&safeSearch=" + safeSearch.ToString();

        string newurl = WWW.EscapeURL("https://www.googleapis.com/youtube/v3/search/?q=" + keyword + "&type=video&maxResults=" + maxresult + "&part=snippet,id&key=" + APIKey + "" + orderFilter + "" + safeSearchFilter);
        Debug.Log(newurl);
        WWW call = new WWW(WWW.UnEscapeURL(newurl));
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        searchResults = new YoutubeData[result["items"].Count];
        Debug.Log(searchResults.Length);
        for (int itemsCounter = 0; itemsCounter < searchResults.Length; itemsCounter++)
        {
            searchResults[itemsCounter] = new YoutubeData();
            searchResults[itemsCounter].id = result["items"][itemsCounter]["id"]["videoId"];
            SetSnippet(result["items"][itemsCounter]["snippet"], out searchResults[itemsCounter].snippet);
        }
        callback.Invoke(searchResults);
    }

    IEnumerator YoutubeSearchByLocation(string keyword, int maxResult, int locationRadius, float latitude, float longitude, YoutubeSearchOrderFilter order, YoutubeSafeSearchFilter safeSearch, Action<YoutubeData[]> callback)
    {
        keyword = keyword.Replace(" ", "%20");
        string orderFilter, safeSearchFilter;
        orderFilter = "";
        if (order != YoutubeSearchOrderFilter.none)
        {
            orderFilter = "&order=" + order.ToString();
        }
        safeSearchFilter = "&safeSearch=" + safeSearch.ToString();
        WWW call = new WWW("https://www.googleapis.com/youtube/v3/search/?type=video&q="+keyword+ "&type=video&locationRadius=" + locationRadius+"mi&location="+latitude+"%2C"+longitude+ "&part=snippet,id&maxResults=" + maxResult+"&key="+APIKey+""+orderFilter+""+safeSearchFilter);
        yield return call;
        Debug.Log(call.url);
        JSONNode result = JSON.Parse(call.text);
        searchResults = new YoutubeData[result["items"].Count];
        Debug.Log(searchResults.Length);
        for(int itemsCounter = 0; itemsCounter < searchResults.Length; itemsCounter++)
        {
            searchResults[itemsCounter] = new YoutubeData();
            searchResults[itemsCounter].id = result["items"][itemsCounter]["id"]["videoId"];
            SetSnippet(result["items"][itemsCounter]["snippet"], out searchResults[itemsCounter].snippet);
        }
        callback.Invoke(searchResults);
    }


    IEnumerator LoadSingleVideo(string videoId, Action<YoutubeData> callback)
    {
        WWW call = new WWW("https://www.googleapis.com/youtube/v3/videos?id=" + videoId + "&part=snippet,id,contentDetails,statistics&key=" + APIKey);
        yield return call;
        data = new YoutubeData();
        JSONNode result = JSON.Parse(call.text);
        result = result["items"][0];   //using items
        data.id = result["id"];
        //Populate snippet data
        SetSnippet(result["snippet"], out data.snippet);
        SetStatistics(result["statistics"], out data.statistics);
        SetContentDetails(result["contentDetails"], out data.contentDetails);
        callback.Invoke(data);
    }

    private void SetSnippet(JSONNode resultSnippet, out YoutubeSnippet data)
    {
        data = new YoutubeSnippet();
        data.publishedAt = resultSnippet["publishedAt"];
        data.channelId = resultSnippet["channelId"];
        data.title = resultSnippet["title"];
        data.description = resultSnippet["description"];

        //save all titles to a list to display if chosen
        YoutubeTitles.Add(data.title);

        //Thumbnails
        data.thumbnails = new YoutubeTumbnails();
        data.thumbnails.defaultThumbnail = new YoutubeThumbnailData();
        data.thumbnails.defaultThumbnail.url = resultSnippet["thumbnails"]["default"]["url"];
        data.thumbnails.defaultThumbnail.width = resultSnippet["thumbnails"]["default"]["width"];
        data.thumbnails.defaultThumbnail.height = resultSnippet["thumbnails"]["default"]["height"];
        data.thumbnails.mediumThumbnail = new YoutubeThumbnailData();
        data.thumbnails.mediumThumbnail.url = resultSnippet["thumbnails"]["medium"]["url"];
        data.thumbnails.mediumThumbnail.width = resultSnippet["thumbnails"]["medium"]["width"];
        data.thumbnails.mediumThumbnail.height = resultSnippet["thumbnails"]["medium"]["height"];
        data.thumbnails.highThumbnail = new YoutubeThumbnailData();
        data.thumbnails.highThumbnail.url = resultSnippet["thumbnails"]["high"]["url"];
        data.thumbnails.highThumbnail.width = resultSnippet["thumbnails"]["high"]["width"];
        data.thumbnails.highThumbnail.height = resultSnippet["thumbnails"]["high"]["height"];
        data.thumbnails.standardThumbnail = new YoutubeThumbnailData();
        data.thumbnails.standardThumbnail.url = resultSnippet["thumbnails"]["standard"]["url"];
        data.thumbnails.standardThumbnail.width = resultSnippet["thumbnails"]["standard"]["width"];
        data.thumbnails.standardThumbnail.height = resultSnippet["thumbnails"]["standard"]["height"];
        data.channelTitle = resultSnippet["channelTitle"];
        //TAGS
        data.tags = new string[resultSnippet["tags"].Count];
        for (int index = 0; index < data.tags.Length; index++)
        {
            data.tags[index] = resultSnippet["tags"][index];
        }
        data.categoryId = resultSnippet["categoryId"];
    }
    private void SetStatistics(JSONNode resultStatistics, out YoutubeStatistics data)
    {
        data = new YoutubeStatistics();
        data.viewCount = resultStatistics["viewCount"];
        data.likeCount = resultStatistics["likeCount"];
        data.dislikeCount = resultStatistics["dislikeCount"];
        data.favoriteCount = resultStatistics["favoriteCount"];
        data.commentCount = resultStatistics["commentCount"];
    }
    private void SetContentDetails(JSONNode resultContentDetails, out YoutubeContentDetails data)
    {
        data = new YoutubeContentDetails();
        data.duration = resultContentDetails["duration"];
        data.dimension = resultContentDetails["dimension"];
        data.definition = resultContentDetails["definition"];
        data.caption = resultContentDetails["caption"];
        data.licensedContent = resultContentDetails["licensedContent"];
        data.projection = resultContentDetails["projection"];

        if(resultContentDetails["contentRating"] != null)
        {
            Debug.Log("Age restrict found!");
            if (resultContentDetails["contentRating"]["ytRating"] == "ytAgeRestricted")
                data.ageRestrict = true;
            else
                data.ageRestrict = false;
        }
        else
            data.ageRestrict = false;

    }

    private void SetComment(JSONNode commentsData, out YoutubeComments data)
    {
        data = new YoutubeComments();
        data.videoId = commentsData["videoId"];
        JSONNode commentDetail = commentsData["topLevelComment"]["snippet"];
        data.authorDisplayName = commentDetail["authorDisplayName"];
        data.authorProfileImageUrl = commentDetail["authorProfileImageUrl"];
        data.authorChannelUrl = commentDetail["authorChannelUrl"];
        data.authorChannelId = commentDetail["authorChannelId"]["value"];
        data.textDisplay = commentDetail["textDisplay"];
        data.textOriginal = commentDetail["textOriginal"];
        data.canRate = commentDetail["canRate"].AsBool;
        data.viewerRating = commentDetail["viewerRating"];
        data.likeCount = commentDetail["likeCount"].AsInt;
        data.publishedAt = commentDetail["publishedAt"];
        data.updatedAt = commentDetail["updatedAt"];
    }

    public enum YoutubeSearchOrderFilter
    {
        none,
        date,
        rating,
        relevance,
        title,
        videoCount,
        viewCount
    }

    public enum YoutubeSafeSearchFilter
    {
        none,
        moderate,
        strict
    }
}
public class YoutubeData
{
    public YoutubeSnippet snippet;
    public YoutubeStatistics statistics;
    public YoutubeContentDetails contentDetails;
    public string id;
}

public class YoutubeComments{
    public string authorDisplayName;
    public string authorProfileImageUrl;
    public string authorChannelUrl;
    public string authorChannelId;
    public string videoId;
    public string textDisplay;
    public string textOriginal;
    public bool canRate;
    public string viewerRating;
    public int likeCount;
    public string publishedAt;
    public string updatedAt;
}

public class YoutubePlaylistItems
{
    public string videoId;
    public YoutubeSnippet snippet;
}

public class YoutubeChannel
{
    public string id;
    public string title;
    public string description;
    public string thumbnail;
}