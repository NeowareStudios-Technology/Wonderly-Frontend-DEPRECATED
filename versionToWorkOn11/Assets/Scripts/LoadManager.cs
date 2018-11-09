﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Sample;
using PolyToolkit;
using EasyAR;

public class LoadManager : MonoBehaviour {
	public FilesManager fm;
	public SaveClassDeclaration scd;
	public targetObjectManager tom; 
	public ImageTargetManager itm;
	public pixabayManager pm;
	public UnityEngine.UI.Image preview1;
	public UnityEngine.UI.Image preview2;
	public UnityEngine.UI.Image preview3;
	public UnityEngine.UI.Image preview4;
	public UnityEngine.UI.Image preview5;

	public GameObject filledIn1;
	public GameObject filledIn2;
	public GameObject filledIn3;
	public GameObject filledIn4;
	public GameObject filledIn5;

	public GameObject unfilled1;
	public GameObject unfilled2;
	public GameObject unfilled3;
	public GameObject unfilled4;
	public GameObject unfilled5;

	public UnityEngine.UI.Image thumb1;
	public UnityEngine.UI.Image thumb2;
	public UnityEngine.UI.Image thumb3;
	public UnityEngine.UI.Image thumb4;
	public UnityEngine.UI.Image thumb5;

	public Text titleDisplay;
	public Text descriptionDisplay;

	public GameObject targetSetter;

	public int previewIndex;

	public List<PolyAsset> allAssets = new List<PolyAsset>();


	//keeps track of first available index for a model
	public int[] modelIndices = new int[5];

	//keeps track of first available index for a model
	public int[] videoIndices = new int[5];



	public void LoadFile() {

		previewIndex = 0;
		targetSetter.SetActive(false);

		Debug.Log("in load file");
		//clear the scene
		tom.clearScene();

		//make sure save directory to load from exists, if it does import the save info from json save file to scd class
		string savePath = Path.Combine(fm.SaveDirectory, "aosave.json");
		if (File.Exists(savePath))
		{
			string jsonString = File.ReadAllText(savePath);
			Debug.Log(jsonString);
			scd = SaveClassDeclaration.CreateFromJSON(jsonString);
		}

		Debug.Log(scd.title);
		//set the experience title and description
		if (scd.title == "")
			titleDisplay.text= "  ";
		else
			titleDisplay.text = scd.title;

		if (scd.description == "")
			descriptionDisplay.text = "  ";
		else
			descriptionDisplay.text = scd.description;

		for (int i =0; i <5; i++)
		{
			fm.targetStatus[i] = scd.targetStatus[i];
		}

		//set up paths for all target photos in save directory and working directory
		string workingPath1 = Path.Combine(fm.MarksDirectory, "targetPhoto1.jpg");
		string workingPath2 = Path.Combine(fm.MarksDirectory, "targetPhoto2.jpg");
		string workingPath3 = Path.Combine(fm.MarksDirectory, "targetPhoto3.jpg");
		string workingPath4 = Path.Combine(fm.MarksDirectory, "targetPhoto4.jpg");
		string workingPath5 = Path.Combine(fm.MarksDirectory, "targetPhoto5.jpg");
		string savePath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string savePath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string savePath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string savePath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string savePath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");

		//copy the target images from the save directory to the working directory
		if (File.Exists(savePath1))
			System.IO.File.Copy(savePath1, workingPath1, true);
		if (File.Exists(savePath2))
			System.IO.File.Copy(savePath2, workingPath2, true);
		if (File.Exists(savePath3))
			System.IO.File.Copy(savePath3, workingPath3, true);
		if (File.Exists(savePath4))
			System.IO.File.Copy(savePath4, workingPath4, true);
		if (File.Exists(savePath5))
			System.IO.File.Copy(savePath5, workingPath5, true);


					if(File.Exists(workingPath1))
					{
						//creates new target game object
						//GameObject imageTarget1 = new GameObject(obj.Key);
						//target1 = imageTarget1;
						GameObject imageTarget1 = itm.target1;
						imageTarget1.SetActive(true);
						imageTarget1.tag = "target1";
						var behaviour1 = imageTarget1.AddComponent<DynamicImageTagetBehaviour>();
						behaviour1.whichTargetAmI = 1;
						behaviour1.Name = "target1";
						behaviour1.Path = workingPath1;
						behaviour1.Storage = StorageType.Absolute;
						//binds tracking behaviour to target behavior script (required)
						behaviour1.Bind(itm.tracker1);
						//keeps track of name of target and behavior
						//imageTargetDic.Add(obj.Key, behaviour1);
						//set the target status array to reflect that this target has been created

					}
					if(File.Exists(workingPath2))
					{
						//creates new target game object
						//GameObject imageTarget1 = new GameObject(obj.Key);
						//target1 = imageTarget1;
						GameObject imageTarget2 = itm.target2;
						imageTarget2.SetActive(true);
						imageTarget2.tag = "target2";
						var behaviour2 = imageTarget2.AddComponent<DynamicImageTagetBehaviour>();
						behaviour2.whichTargetAmI = 2;
						behaviour2.Name = "target1";
						behaviour2.Path = workingPath2;
						behaviour2.Storage = StorageType.Absolute;
						//binds tracking behaviour to target behavior script (required)
						behaviour2.Bind(itm.tracker2);
						//keeps track of name of target and behavior
						//imageTargetDic.Add(obj.Key, behaviour1);
						//set the target status array to reflect that this target has been created

					}
					if(File.Exists(workingPath3))
					{
						//creates new target game object
						//GameObject imageTarget3 = new GameObject(obj.Key);
						//target3 = imageTarget3;
						GameObject imageTarget3 = itm.target3;
						imageTarget3.SetActive(true);
						imageTarget3.tag = "target3";
						var behaviour3 = imageTarget3.AddComponent<DynamicImageTagetBehaviour>();
						behaviour3.whichTargetAmI = 3;
						behaviour3.Name = "target3";
						behaviour3.Path = workingPath3;
						behaviour3.Storage = StorageType.Absolute;
						//binds tracking behaviour to target behavior script (required)
						behaviour3.Bind(itm.tracker3);
						//keeps track of name of target and behavior
						//imageTargetDic.Add(obj.Key, behaviour3);
						//set the target status array to reflect that this target has been created
					}
					if(File.Exists(workingPath4))
					{
						//creates new target game object
						//GameObject imageTarget4 = new GameObject(obj.Key);
						//target4 = imageTarget4;
						GameObject imageTarget4 = itm.target4;
						imageTarget4.SetActive(true);
						imageTarget4.tag = "target4";
						var behaviour4 = imageTarget4.AddComponent<DynamicImageTagetBehaviour>();
						behaviour4.whichTargetAmI = 4;
						behaviour4.Name = "target4";
						behaviour4.Path = workingPath4;
						behaviour4.Storage = StorageType.Absolute;
						//binds tracking behaviour to target behavior script (required)
						behaviour4.Bind(itm.tracker4);
						//keeps track of name of target and behavior
						//imageTargetDic.Add(obj.Key, behaviour4);
						//set the target status array to reflect that this target has been created
					}
					if(File.Exists(workingPath5))
					{
						//creates new target game object
						//GameObject imageTarget5 = new GameObject(obj.Key);
						//target5 = imageTarget5;
						GameObject imageTarget5 = itm.target5;
						imageTarget5.SetActive(true);
						imageTarget5.tag = "target5";
						var behaviour5 = imageTarget5.AddComponent<DynamicImageTagetBehaviour>();
						behaviour5.whichTargetAmI = 5;
						behaviour5.Name = "target5";
						behaviour5.Path = workingPath5;
						behaviour5.Storage = StorageType.Absolute;
						//binds tracking behaviour to target behavior script (required)
						behaviour5.Bind(itm.tracker5);
						//keeps track of name of target and behavior
						//imageTargetDic.Add(obj.Key, behaviour5);
						//set the target status array to reflect that this target has been created
	
					}

		//set preivew images
		if (File.Exists(workingPath1))
			preview1.sprite = IMG2Sprite.LoadNewSprite(workingPath1);
		if (File.Exists(workingPath2))
			preview2.sprite = IMG2Sprite.LoadNewSprite(workingPath2);
		if (File.Exists(workingPath3))
			preview3.sprite = IMG2Sprite.LoadNewSprite(workingPath3);
		if (File.Exists(workingPath4))
			preview4.sprite = IMG2Sprite.LoadNewSprite(workingPath4);
		if (File.Exists(workingPath5))
			preview5.sprite = IMG2Sprite.LoadNewSprite(workingPath5);
		//call function to imported all loaded AR objects (pics/videos/models)
		ImportLoadedItems();
	}

	//imports all AR objects from save directory
	private void ImportLoadedItems() {
		for (int i = 0; i < 5; i ++)
		{
			//import model
			if (scd.targetStatus[i] == "model")
			{
				modelIndices[i] = 1;
				switch(i)
				{
					case 0:
						fm.targetStatus[0] = "model";
						ImportModel(scd.mod1);
						break;
					case 1:
						fm.targetStatus[1] = "model";
						ImportModel(scd.mod2);
						break;
					case 2:
						fm.targetStatus[2] = "model";
						ImportModel(scd.mod3);
						break;
					case 3:
						fm.targetStatus[3] = "model";
						ImportModel(scd.mod4);
						break;
					case 4:
						fm.targetStatus[4] = "model";
						ImportModel(scd.mod5);
						break;
				}
			}
			//import video
			else if (scd.targetStatus[i] == "video")
			{
				
				videoIndices[i] = 1;
				switch(i)
				{
					case 0:
						fm.targetStatus[0] = "video";
						itm.target1.SetActive(true);
						tom.videoPlayer1.SetActive(true);
						tom.videoPlayer1.GetComponent<SimplePlayback>().PlayYoutubeVideo(scd.vId[0]);
						break;
					case 1:
						fm.targetStatus[1] = "video";
						itm.target2.SetActive(true);
						tom.videoPlayer2.SetActive(true);
						tom.videoPlayer2.GetComponent<SimplePlayback>().PlayYoutubeVideo(scd.vId[1]);
						break;
					case 2:
						fm.targetStatus[2] = "video";
						itm.target3.SetActive(true);
						tom.videoPlayer3.SetActive(true);
						tom.videoPlayer3.GetComponent<SimplePlayback>().PlayYoutubeVideo(scd.vId[2]);
						break;
					case 3:
						fm.targetStatus[3] = "video";
						itm.target4.SetActive(true);
						tom.videoPlayer4.SetActive(true);
						tom.videoPlayer4.GetComponent<SimplePlayback>().PlayYoutubeVideo(scd.vId[3]);
						break;
					case 4:
						fm.targetStatus[4] = "video";
						itm.target5.SetActive(true);
						tom.videoPlayer5.SetActive(true);
						tom.videoPlayer5.GetComponent<SimplePlayback>().PlayYoutubeVideo(scd.vId[4]);
						break;
				}
				
			}
				else if (scd.targetStatus[i] == "image")
			{
				StartCoroutine(setImage(i));
			}
		}
	}

	public IEnumerator setImage(int index)
	{
		switch(index)
				{
					case 0:
						fm.targetStatus[0] = "image";
						using (WWW imageRequest = new WWW(scd.imageUrl[0]))
						{
							yield return imageRequest;
							pm.image1.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
						}
						break;
					case 1:
						fm.targetStatus[1] = "image";
						using (WWW imageRequest = new WWW(scd.imageUrl[1]))
						{
							yield return imageRequest;
							pm.image2.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
						}
						break;
					case 2:
						fm.targetStatus[2] = "image";
						using (WWW imageRequest = new WWW(scd.imageUrl[2]))
						{
							yield return imageRequest;
							pm.image3.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
						}
						break;
					case 3:
						fm.targetStatus[3] = "image";
						using (WWW imageRequest = new WWW(scd.imageUrl[3]))
						{
							yield return imageRequest;
							pm.image4.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
						}
						break;
					case 4:
						fm.targetStatus[4] = "image";
						using (WWW imageRequest = new WWW(scd.imageUrl[4]))
						{
							yield return imageRequest;
							pm.image5.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
						}
						break;
				}
	}

	private void ImportModel(string modelId) {
		string assetString = "assets/" + modelId;
		PolyApi.GetAsset(modelId, GetAssetCallback);
	}

	void GetAssetCallback(PolyStatusOr<PolyAsset> result) {
  	if (!result.Ok) 
		{
			Debug.Log("There was an error importing the loaded asset");
			return;
  	}
		List<PolyAsset> assets = new List<PolyAsset>();
		assets.Add(result.Value);
		allAssets.Add(result.Value);
		tom.attribs.Add(PolyApi.GenerateAttributions(includeStatic: true, runtimeAssets: assets));

		PolyImportOptions options = PolyImportOptions.Default();
		// We want to rescale the imported meshes to a specific size.
		options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
		// The specific size we want assets rescaled to (fit in a 1x1x1 box):
		options.desiredSize = 1.0f;
		// We want the imported assets to be recentered such that their centroid coincides with the origin:
		options.recenter = true;
		PolyApi.Import(result.Value, options, GetModelCallback);
	}

	void GetModelCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result) {
  if (!result.Ok) {
    Debug.Log("There was an error importing the loaded model");
    return;
  }
	for (int j = 0; j < 5 ;j++)
	{
		if (modelIndices[j] == 1)
		{
			//assigns the imported model to GameObject scripts and sets the models parent as the correct indexed target
			switch(j)
			{
				case 0:
					GameObject model1 = result.Value.gameObject;
					Transform transform1 = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
					transform1.position = new Vector3(0.0f, 0.75f, 0f);
					transform1.tag = "importedModel1";
					transform1.parent = itm.target1.transform;
					fm.targetStatus[0] = "model";
					tom.model1 = model1;
					tom.modelId1 = scd.mod1;
					break;
				case 1:
					GameObject model2 = result.Value.gameObject;
					Transform transform2 = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
					transform2.position = new Vector3(0.0f, 0.75f, 0f);
					transform2.tag = "importedModel2";
					transform2.parent = itm.target2.transform;
					fm.targetStatus[1] = "model";
					tom.model2 = model2;
					tom.modelId2 = scd.mod2;
					break;
				case 2:
					GameObject model3 = result.Value.gameObject;
					Transform transform3 = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
					transform3.position = new Vector3(0.0f, 0.75f, 0f);
					transform3.tag = "importedModel3";
					transform3.parent = itm.target3.transform;
					fm.targetStatus[2] = "model";
					tom.model3 = model3;
					tom.modelId3 = scd.mod3;
					break;
				case 3:
					GameObject model4 = result.Value.gameObject;
					Transform transform4 = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
					transform4.position = new Vector3(0.0f, 0.75f, 0f);
					transform4.tag = "importedModel4";
					transform4.parent = itm.target4.transform;
					fm.targetStatus[3] = "model";
					tom.model4 = model4;
					tom.modelId4 = scd.mod4;
					break;
				case 4:
					GameObject model5 = result.Value.gameObject;
					Transform transform5 = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
					transform5.position = new Vector3(0.0f, 0.75f, 0f);
					transform5.tag = "importedModel5";
					transform5.parent = itm.target5.transform;
					fm.targetStatus[4] = "model";
					tom.model5 = model5;
					tom.modelId5 = scd.mod5;
					break;
			}
			modelIndices[j] = 0;
			return;
		}
	}
	}
/* 
	public void setLoadedImageThumb(int whichThumb)
	{
		using (WWW previewRequest = new WWW(imagePreviewUrl[index]))
		{
			yield return previewRequest;
			switch(whichThumb)
			{
				case 1:
					thumb1.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
					break;
			}
		}
	}
	*/
	public void setLoadedVideoThumb()
	{
		
	}
	public void setLoadedModelThumb()
	{
		
	}

	public void nextPreview()
	{
		if (previewIndex == 4)
			return;

		switch(previewIndex)
		{
			case 0:
				preview1.gameObject.SetActive(false);
				filledIn1.SetActive(false);
				unfilled1.SetActive(true);
				break;
			case 1:
				preview2.gameObject.SetActive(false);
				filledIn2.SetActive(false);
				unfilled2.SetActive(true);
				break;
			case 2:
				preview3.gameObject.SetActive(false);
				filledIn3.SetActive(false);
				unfilled3.SetActive(true);
				break;
			case 3:
				preview4.gameObject.SetActive(false);
				filledIn4.SetActive(false);
				unfilled4.SetActive(true);
				break;
			case 4:
				preview5.gameObject.SetActive(false);
				filledIn5.SetActive(false);
				unfilled5.SetActive(true);
				break;
		}
		previewIndex++;
		switch(previewIndex)
		{
			case 0:
				preview1.gameObject.SetActive(true);
				filledIn1.SetActive(true);
				unfilled1.SetActive(false);
				break;
			case 1:
				preview2.gameObject.SetActive(true);
				filledIn2.SetActive(true);
				unfilled2.SetActive(false);
				break;
			case 2:
				preview3.gameObject.SetActive(true);
				filledIn3.SetActive(true);
				unfilled3.SetActive(false);
				break;
			case 3:
				preview4.gameObject.SetActive(true);
				filledIn4.SetActive(true);
				unfilled4.SetActive(false);
				break;
			case 4:
				preview5.gameObject.SetActive(true);
				filledIn5.SetActive(true);
				unfilled5.SetActive(false);
				break;
		}
	}

		public void prevPreview()
	{
		if (previewIndex == 0)
			return;

		switch(previewIndex)
		{
			case 0:
				preview1.gameObject.SetActive(false);
				filledIn1.SetActive(false);
				unfilled1.SetActive(true);
				break;
			case 1:
				preview2.gameObject.SetActive(false);
				filledIn2.SetActive(false);
				unfilled2.SetActive(true);
				break;
			case 2:
				preview3.gameObject.SetActive(false);
				filledIn3.SetActive(false);
				unfilled3.SetActive(true);
				break;
			case 3:
				preview4.gameObject.SetActive(false);
				filledIn4.SetActive(false);
				unfilled4.SetActive(true);
				break;
			case 4:
				preview5.gameObject.SetActive(false);
				filledIn5.SetActive(false);
				unfilled5.SetActive(true);
				break;
		}
		previewIndex--;
		switch(previewIndex)
		{
			case 0:
				preview1.gameObject.SetActive(true);
				filledIn1.SetActive(true);
				unfilled1.SetActive(false);
				break;
			case 1:
				preview2.gameObject.SetActive(true);
				filledIn2.SetActive(true);
				unfilled2.SetActive(false);
				break;
			case 2:
				preview3.gameObject.SetActive(true);
				filledIn3.SetActive(true);
				unfilled3.SetActive(false);
				break;
			case 3:
				preview4.gameObject.SetActive(true);
				filledIn4.SetActive(true);
				unfilled4.SetActive(false);
				break;
			case 4:
				preview5.gameObject.SetActive(true);
				filledIn5.SetActive(true);
				unfilled5.SetActive(false);
				break;
		}

	}


}