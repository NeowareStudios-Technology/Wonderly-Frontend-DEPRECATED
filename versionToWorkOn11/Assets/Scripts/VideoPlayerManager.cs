using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sample;

public class VideoPlayerManager : MonoBehaviour {

	public GameObject videoPlayer1;
	public GameObject videoPlayer2;
	public GameObject videoPlayer3;
	public GameObject videoPlayer4;
	public GameObject videoPlayer5;

	public FilesManager fm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//decides whether to display a targets video player depending on if a video was assigned or not
	//video player will also appear as placeholder for target on a "created" target
	void Update () {
	
		if (fm.targetStatus[0] == "model" || fm.targetStatus[0] == "none" || fm.targetStatus[0] == "created")
			videoPlayer1.SetActive(false);
		else
			videoPlayer1.SetActive(true);
		
		if (fm.targetStatus[1] == "model" || fm.targetStatus[1] == "none" || fm.targetStatus[1] == "created")
			videoPlayer2.SetActive(false);
		else
			videoPlayer2.SetActive(true);
		
		if (fm.targetStatus[2] == "model" || fm.targetStatus[2] == "none" || fm.targetStatus[2] == "created")
			videoPlayer3.SetActive(false);
		else
			videoPlayer3.SetActive(true);

		if (fm.targetStatus[3] == "model" || fm.targetStatus[3] == "none" || fm.targetStatus[3] == "created")
			videoPlayer4.SetActive(false);
		else
			videoPlayer4.SetActive(true);
		
		if (fm.targetStatus[4] == "model" || fm.targetStatus[4] == "none" || fm.targetStatus[4] == "created")
			videoPlayer5.SetActive(false);
		else
			videoPlayer5.SetActive(true);
	}
}
