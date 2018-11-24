using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Sample;

public class targetObjectManager : MonoBehaviour {

	public GameObject videoPlayer1;
	public GameObject videoPlayer2;
	public GameObject videoPlayer3;
	public GameObject videoPlayer4;
	public GameObject videoPlayer5;

	public GameObject targetMarker1;
	public GameObject targetMarker2;
	public GameObject targetMarker3;
	public GameObject targetMarker4;
	public GameObject targetMarker5;

	public GameObject model1;
	public GameObject model2;
	public GameObject model3;
	public GameObject model4;
	public GameObject model5;

	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;

	public string modelId1;
	public string modelId2;
	public string modelId3;
	public string modelId4;
	public string modelId5;

	public Image targetObjectThumbnail1;
	public Image targetObjectThumbnail2;
	public Image targetObjectThumbnail3;
	public Image targetObjectThumbnail4;
	public Image targetObjectThumbnail5;

	public Image blankSprite;

	public List<string> attribs = new List<string>();

	public FilesManager fm;
	public ImageTargetManager itm;

	// Use this for initialization
	void Start () {
		
	}
	
	// THIS NEEDS TO BE FIXED, AS IT IS NOW THERE IS NO SOUND
	// WHEN THE UPDATE() FUNCTION IS REMOVED, SOUND COMES BACK ON BUT THE VIDEO PLAYS WITHOUT THE ACTIVE TARGET UPON SELECTION
	//decides whether to display a targets video player depending on if a video was assigned or not
	//video player will also appear as placeholder for target on a "created" target 
	void Update () {

		
		GameObject videoPlayer = videoPlayer1;
		GameObject targetMarker = targetMarker1;
		GameObject image = image1;
		for (int i = 0; i < 5; i++)
		{
			switch(i)
			{
				case 0:
					videoPlayer = videoPlayer1;
					targetMarker = targetMarker1;
					image = image1;
					break;
				case 1:
					videoPlayer = videoPlayer2;
					targetMarker = targetMarker2;
					image = image2;
					break;
				case 2:
					videoPlayer = videoPlayer3;
					targetMarker = targetMarker3;
					image = image3;
					break;
				case 3:
					videoPlayer = videoPlayer4;
					targetMarker = targetMarker4;
					image = image4;
					break;
				case 4:
					videoPlayer = videoPlayer5;
					targetMarker = targetMarker5;
					image = image5;
					break;
			}
			//manages objects for each target
			if (fm.targetStatus[i] == "none")
			{
				//videoPlayer.SetActive(false);
				targetMarker.SetActive(false);
				image.SetActive(false);
				switch(i)
				{
					case 0:
						resetTargetModel(1);
						videoPlayer1.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 1:
						resetTargetModel(2);
						videoPlayer2.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 2:
						resetTargetModel(3);
						videoPlayer3.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 3:
						resetTargetModel(4);
						videoPlayer4.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 4:
						resetTargetModel(5);
						videoPlayer5.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
				}
			}
			else if (fm.targetStatus[i] == "image")
			{
				switch(i)
				{
					case 0:
						resetTargetModel(1);
						break;
					case 1:
						resetTargetModel(2);
						break;
					case 2:
						resetTargetModel(3);
						break;
					case 3:
						resetTargetModel(4);
						break;
					case 4:
						resetTargetModel(5);
						break;
				}
				videoPlayer.SetActive(false);
				targetMarker.SetActive(false);
				image.SetActive(true);
			}
			else if (fm.targetStatus[i] == "created")
			{
				//videoPlayer.SetActive(false);
				targetMarker.SetActive(true);
				image.SetActive(false);
				switch(i)
				{
					case 0:
						resetTargetModel(1);
						videoPlayer1.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 1:
						resetTargetModel(2);
						videoPlayer2.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 2:
						resetTargetModel(3);
						videoPlayer3.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 3:
						resetTargetModel(4);
						videoPlayer4.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
					case 4:
						resetTargetModel(5);
						videoPlayer5.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						break;
				}
			}
			else if (fm.targetStatus[i] == "model")
			{
				videoPlayer.SetActive(false);
				targetMarker.SetActive(false);
				image.SetActive(false);
			}
			
			else if (fm.targetStatus[i] == "video")
			{
				switch(i)
				{
					case 0:	
						resetTargetModel(1);
						if (itm.activeTarget1 == false && itm.target1.activeSelf == true)
						{
							videoPlayer1.GetComponent<SimplePlayback>().unityVideoPlayer.Pause();
							videoPlayer1.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						}
						break;
					case 1:
						resetTargetModel(2);
						if (itm.activeTarget2 == false && itm.target2.activeSelf == true)
						{
							videoPlayer2.GetComponent<SimplePlayback>().unityVideoPlayer.Pause();
							videoPlayer2.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						}
						break;
					case 2:
						resetTargetModel(3);
						if (itm.activeTarget3 == false && itm.target3.activeSelf == true)
						{
							videoPlayer3.GetComponent<SimplePlayback>().unityVideoPlayer.Pause();
							videoPlayer3.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						}
						break;
					case 3:
						resetTargetModel(4);
						if (itm.activeTarget4 == false && itm.target4.activeSelf == true)
						{
							videoPlayer4.GetComponent<SimplePlayback>().unityVideoPlayer.Pause();
							videoPlayer4.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						}
						break;
					case 4:
						resetTargetModel(5);
						if (itm.activeTarget5 == false && itm.target5.activeSelf == true)
						{
							videoPlayer5.GetComponent<SimplePlayback>().unityVideoPlayer.Pause();
							videoPlayer5.gameObject.transform.position = new Vector3(90f, 0f, 0f);
						}
						break;
				}
				videoPlayer.SetActive(true);
				targetMarker.SetActive(false);
				image.SetActive(false);
				
			}
		}
	}

	public void resetTargetModel(int whichModel)
	{
		switch(whichModel)
		{
			case 1:
				Destroy(model1);
				modelId1 = "";
				break;
			case 2:
				Destroy(model2);
				modelId2 = "";
				break;
			case 3:
				Destroy(model3);
				modelId3 = "";
				break;
			case 4:
				Destroy(model4);
				modelId4 = "";
				break;
			case 5:
				Destroy(model5);
				modelId5 = "";
				break;
		}
			
	}

	public void removeTargetObject(int whichTarget)
	{
		//do nothing if there is no target object 
		if(fm.targetStatus[whichTarget-1] == "none" || fm.targetStatus[whichTarget-1] == "created")
			return;

		switch(whichTarget)
		{
			case 1:
				//set to created, since we know that the image was not "none" or "created" before, but set to a model, vid, or pic
				fm.targetStatus[0] = "created";
				if (model1 != null)
				{
					Destroy(model1);
					modelId1 = null;
					model1 = null;
				}
				videoPlayer1.SetActive(false);
				targetObjectThumbnail1.sprite = blankSprite.sprite;
				break;
			case 2:
				fm.targetStatus[1] = "created";
				if (model2 != null)
				{
					Destroy(model2);
					modelId2 = null;
					model2 = null;
				}
				videoPlayer2.SetActive(false);
				targetObjectThumbnail2.sprite = blankSprite.sprite;
				break;
			case 3:
				fm.targetStatus[2] = "created";
				if (model3 != null)
				{
					Destroy(model3);
					modelId3 = null;
					model3 = null;
				}
				videoPlayer3.SetActive(false);
				targetObjectThumbnail3.sprite = blankSprite.sprite;
				break;
			case 4:
				fm.targetStatus[3] = "created";
				if (model4 != null)
				{
					Destroy(model4);
					modelId4 = null;
					model4 = null;
				}
				videoPlayer4.SetActive(false);
				targetObjectThumbnail4.sprite = blankSprite.sprite;
				break;
			case 5:
				fm.targetStatus[4] = "created";
				if (model5 != null)
				{
					Destroy(model5);
					modelId5 = null;
					model5 = null;
				}
				videoPlayer1.SetActive(false);
				targetObjectThumbnail5.sprite = blankSprite.sprite;
				break;
		}
	}

	public void manualRemoveTargetObject(int whichTarget)
	{
		//do nothing if there is no target object 
		if(fm.targetStatus[whichTarget-1] == "none" || fm.targetStatus[whichTarget-1] == "created")
			return;

		switch(whichTarget)
		{
			case 1:
				fm.targetStatus[0] = "created";
				if (model1 != null)
				{
					Destroy(model1);
					modelId1 = null;
					model1 = null;
				}
				videoPlayer1.SetActive(false);
				
				break;
			case 2:
				fm.targetStatus[1] = "created";
				if (model2 != null)
				{
					Destroy(model2);
					modelId2 = null;
					model2 = null;
				}
				videoPlayer2.SetActive(false);
				break;
			case 3:
				fm.targetStatus[2] = "created";
				if (model3 != null)
				{
					Destroy(model3);
					modelId3 = null;
					model3 = null;
				}
				videoPlayer3.SetActive(false);
				break;
			case 4:
				fm.targetStatus[3] = "created";
				if (model4 != null)
				{
					Destroy(model4);
					modelId4 = null;
					model4 = null;
				}
				videoPlayer4.SetActive(false);
				break;
			case 5:
				fm.targetStatus[4] = "created";
				if (model5 != null)
				{
					Destroy(model5);
					modelId5 = null;
					model5 = null;
				}
				videoPlayer1.SetActive(false);
				break;
		}
	}

	public void rotateTargetObject()
	{
		//do nothing if there is no target object 
		if(fm.targetStatus[fm.currentTarget-1] == "none" || fm.targetStatus[fm.currentTarget-1] == "created")
			return;

		switch(fm.currentTarget)
		{
			case 1:
				if (fm.targetStatus[fm.currentTarget-1] == "video")
				{
					videoPlayer1.transform.Rotate(0,0,30);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "model")
				{
					model1.transform.Rotate(30,0,0);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "image")
				{
					image1.transform.Rotate(0,0,30);
				}
				break;
			case 2:
				if (fm.targetStatus[fm.currentTarget-1] == "video")
				{
					videoPlayer2.transform.Rotate(0,0,30);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "model")
				{
					model2.transform.Rotate(30,0,0);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "image")
				{
					image2.transform.Rotate(0,0,30);
				}
				break;
			case 3:
				if (fm.targetStatus[fm.currentTarget-1] == "video")
				{
					videoPlayer3.transform.Rotate(0,0,30);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "model")
				{
					model3.transform.Rotate(30,0,0);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "image")
				{
					image3.transform.Rotate(0,0,30);
				}
				break;
			case 4:
				if (fm.targetStatus[fm.currentTarget-1] == "video")
				{
					videoPlayer4.transform.Rotate(0,0,30);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "model")
				{
					model4.transform.Rotate(30,0,0);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "image")
				{
					image4.transform.Rotate(0,0,30);
				}
				break;
			case 5:
				if (fm.targetStatus[fm.currentTarget-1] == "video")
				{
					videoPlayer5.transform.Rotate(0,0,30);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "model")
				{
					model5.transform.Rotate(30,0,0);
				}
				if (fm.targetStatus[fm.currentTarget-1] == "image")
				{
					image5.transform.Rotate(0,0,30);
				}
				break;
		}
	}

	public void clearScene() {
		if(itm.target1.transform.childCount == 4)
      Destroy(itm.target1.transform.GetChild(3).gameObject);
		if(itm.target2.transform.childCount == 4)
      Destroy(itm.target2.transform.GetChild(3).gameObject);
		if(itm.target3.transform.childCount == 4)
      Destroy(itm.target3.transform.GetChild(3).gameObject);
		if(itm.target4.transform.childCount == 4)
      Destroy(itm.target4.transform.GetChild(3).gameObject);
		if(itm.target5.transform.childCount == 4)
      Destroy(itm.target5.transform.GetChild(3).gameObject);

		for (int i = 0; i < 5; i++)
		{
			fm.targetStatus[i] = "none";
		}
    
		fm.targetCount = 0;
		fm.currentTarget = 0;

		model1 = null;
		model2 = null;
		model3 = null;
		model4 = null;
		model5 = null;

		modelId1 = "";
		modelId2 = "";
		modelId3 = "";
		modelId4 = "";
		modelId5 = "";

		targetObjectThumbnail1.sprite = blankSprite.sprite;
		targetObjectThumbnail2.sprite = blankSprite.sprite;
		targetObjectThumbnail3.sprite = blankSprite.sprite;
		targetObjectThumbnail4.sprite = blankSprite.sprite;
		targetObjectThumbnail5.sprite = blankSprite.sprite;

		removeTargetObject(1);
		removeTargetObject(2);
		removeTargetObject(3);
		removeTargetObject(4);
		removeTargetObject(5);
      
	}
}
