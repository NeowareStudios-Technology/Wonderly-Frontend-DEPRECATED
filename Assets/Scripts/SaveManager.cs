using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Sample;

public class SaveManager : MonoBehaviour {

	public FilesManager fm;
	public targetObjectManager tom;
	public pixabayManager pm;
	public FirebaseStorageManager fsm;
	public CloudEndpointsApiManager ceam;
	public LoadManager lm;

	public Text title;
	public Text description;
	public Text browserLink;

	public void CreateSaveFile()
	{
		Debug.Log("1. Starting SaveManager.CreateSaveFile()...");
		//delete the previous save
		deleteOldSave();

		//create a new save class instance
		SaveClassDeclaration save = new SaveClassDeclaration();
		save.targetNum = fm.targetCount;
		save.targetStatus = fm.targetStatus;
		Debug.Log("2. number of targets being saved (first element to be saved): " +save.targetNum);

		//save the title and description of the experience
		save.title = title.text;
		save.description = description.text;

		//save browser url link
		save.browserLink = browserLink.text;



		//create save directory
		Directory.CreateDirectory(fm.SaveDirectory);
		Debug.Log("3. Save directory being created: " + fm.SaveDirectory);

		//copy working directory target photos to save directory
		string targetPath1 = Path.Combine(fm.MarksDirectory, "targetPhoto1.jpg");
		string targetPath2 = Path.Combine(fm.MarksDirectory, "targetPhoto2.jpg");
		string targetPath3 = Path.Combine(fm.MarksDirectory, "targetPhoto3.jpg");
		string targetPath4 = Path.Combine(fm.MarksDirectory, "targetPhoto4.jpg");
		string targetPath5 = Path.Combine(fm.MarksDirectory, "targetPhoto5.jpg");
		string destPath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string destPath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string destPath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string destPath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string destPath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");
		
		if (File.Exists(targetPath1))
			System.IO.File.Copy(targetPath1, destPath1, true);
		if (File.Exists(targetPath2))
			System.IO.File.Copy(targetPath2, destPath2, true);
		if (File.Exists(targetPath3))
			System.IO.File.Copy(targetPath3, destPath3, true);
		if (File.Exists(targetPath4))
			System.IO.File.Copy(targetPath4, destPath4, true);
		if (File.Exists(targetPath5))
			System.IO.File.Copy(targetPath5, destPath5, true);


		//make sure to iterate over all 5 possible targets to make sure we get all targets if one has been deleted
		for (int i = 0; i < 5; i++)
		{
			//only save this target if the it has objects save to it
			if (fm.targetStatus[i] != "none" || fm.targetStatus[i] != "created")
			{
				//int oneIndex = i+1;
				float x = 0.0f;
				float y = 0.0f;
				float z = 0.0f;
				switch(fm.targetStatus[i])
				{
					//save all video url info and position info to save class
					case "video":
						string videoUrl = "";
						string audioUrl = "";
						switch(i)
						{
							case 0:
								save.vId[0] = tom.videoPlayer1.GetComponent<SimplePlayback>().videoId;
								//save.vUrl1 = tom.videoPlayer1.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl1 = tom.videoPlayer1.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer1.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer1.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.vId[1] = tom.videoPlayer2.GetComponent<SimplePlayback>().videoId;
								//save.vUrl2 = tom.videoPlayer2.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl2 = tom.videoPlayer2.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer2.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer2.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.vId[2] = tom.videoPlayer3.GetComponent<SimplePlayback>().videoId;
								//save.vUrl3 = tom.videoPlayer3.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl3 = tom.videoPlayer3.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer3.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer3.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.vId[3] = tom.videoPlayer4.GetComponent<SimplePlayback>().videoId;
								//save.vUrl4 = tom.videoPlayer4.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl4 = tom.videoPlayer4.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer4.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer4.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.vId[4] = tom.videoPlayer5.GetComponent<SimplePlayback>().videoId;
								//save.vUrl5 = tom.videoPlayer5.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl5 = tom.videoPlayer5.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer5.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer5.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;

					//save model ID info and model rotation info to save class
					case "model":
						string modelId = "";
						switch(i)
						{
							case 0:
								save.mod1 = tom.modelId1;
								x = tom.model1.transform.rotation.eulerAngles.x;
								y = tom.model1.transform.rotation.eulerAngles.y;
								z = tom.model1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.mod2 = tom.modelId2;
								x = tom.model2.transform.rotation.eulerAngles.x;
								y = tom.model2.transform.rotation.eulerAngles.y;
								z = tom.model2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.mod3 = tom.modelId3;
								x = tom.model3.transform.rotation.eulerAngles.x;
								y = tom.model3.transform.rotation.eulerAngles.y;
								z = tom.model3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.mod4 = tom.modelId4;
								x = tom.model4.transform.rotation.eulerAngles.x;
								y = tom.model4.transform.rotation.eulerAngles.y;
								z = tom.model4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.mod5 = tom.modelId5;
								x = tom.model5.transform.rotation.eulerAngles.x;
								y = tom.model5.transform.rotation.eulerAngles.y;
								z = tom.model5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;
					case "image":
						switch(i)
						{
							case 0:
								save.imageUrl[0] = pm.chosenUrls[0];
								x = tom.image1.transform.rotation.eulerAngles.x;
								y = tom.image1.transform.rotation.eulerAngles.y;
								z = tom.image1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.imageUrl[1] = pm.chosenUrls[1];
								x = tom.image2.transform.rotation.eulerAngles.x;
								y = tom.image2.transform.rotation.eulerAngles.y;
								z = tom.image2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.imageUrl[2] = pm.chosenUrls[2];
								x = tom.image3.transform.rotation.eulerAngles.x;
								y = tom.image3.transform.rotation.eulerAngles.y;
								z = tom.image3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.imageUrl[3] = pm.chosenUrls[3];
								x = tom.image4.transform.rotation.eulerAngles.x;
								y = tom.image4.transform.rotation.eulerAngles.y;
								z = tom.image4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.imageUrl[4] = pm.chosenUrls[4];
								x = tom.image5.transform.rotation.eulerAngles.x;
								y = tom.image5.transform.rotation.eulerAngles.y;
								z = tom.image5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;
				}
			}
		}
		string saveFilePath = Path.Combine(fm.SaveDirectory, "aoSave.json");
		string thisSave = JsonUtility.ToJson(save);
		File.WriteAllText(saveFilePath, thisSave);

		Debug.Log("4. AR Experience Being Saved...");
		if (File.Exists(saveFilePath))
		{
			Debug.Log("5. Save file created!");
		}
		else
		{
			Debug.Log("5. Save file could not be created.");
		}

		if (File.Exists(saveFilePath))
			Debug.Log("**0** sm 266, Save file exists: "+saveFilePath);
		else	
			Debug.Log("**0** sm 266, Save file missing: "+saveFilePath);

		fsm.startExperienceUpload();
	}




	public void CreateSaveFileForEdit()
	{
		//delete the previous save
		deleteOldSave();

		//create a new save class instance
		SaveClassDeclaration save = new SaveClassDeclaration();
		save.targetNum = fm.targetCount;
		save.targetStatus = fm.targetStatus;

		//save the title and description of the experience
		save.title = title.text;
		save.description = description.text;

		//save browser url link
		save.browserLink = browserLink.text;



		//create save directory
		Directory.CreateDirectory(fm.SaveDirectory);

		//copy working directory target photos to save directory
		string targetPath1 = Path.Combine(fm.MarksDirectory, "targetPhoto1.jpg");
		string targetPath2 = Path.Combine(fm.MarksDirectory, "targetPhoto2.jpg");
		string targetPath3 = Path.Combine(fm.MarksDirectory, "targetPhoto3.jpg");
		string targetPath4 = Path.Combine(fm.MarksDirectory, "targetPhoto4.jpg");
		string targetPath5 = Path.Combine(fm.MarksDirectory, "targetPhoto5.jpg");
		string destPath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string destPath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string destPath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string destPath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string destPath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");
		
		if (File.Exists(targetPath1))
			System.IO.File.Copy(targetPath1, destPath1, true);
		if (File.Exists(targetPath2))
			System.IO.File.Copy(targetPath2, destPath2, true);
		if (File.Exists(targetPath3))
			System.IO.File.Copy(targetPath3, destPath3, true);
		if (File.Exists(targetPath4))
			System.IO.File.Copy(targetPath4, destPath4, true);
		if (File.Exists(targetPath5))
			System.IO.File.Copy(targetPath5, destPath5, true);


		//make sure to iterate over all 5 possible targets to make sure we get all targets if one has been deleted
		for (int i = 0; i < 5; i++)
		{
			//only save this target if the it has objects save to it
			if (fm.targetStatus[i] != "none" || fm.targetStatus[i] != "created")
			{
				//int oneIndex = i+1;
				float x = 0.0f;
				float y = 0.0f;
				float z = 0.0f;
				switch(fm.targetStatus[i])
				{
					//save all video url info and position info to save class
					case "video":
						string videoUrl = "";
						string audioUrl = "";
						switch(i)
						{
							case 0:
								save.vId[0] = tom.videoPlayer1.GetComponent<SimplePlayback>().videoId;
								//save.vUrl1 = tom.videoPlayer1.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl1 = tom.videoPlayer1.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer1.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer1.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.vId[1] = tom.videoPlayer2.GetComponent<SimplePlayback>().videoId;
								//save.vUrl2 = tom.videoPlayer2.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl2 = tom.videoPlayer2.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer2.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer2.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.vId[2] = tom.videoPlayer3.GetComponent<SimplePlayback>().videoId;
								//save.vUrl3 = tom.videoPlayer3.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl3 = tom.videoPlayer3.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer3.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer3.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.vId[3] = tom.videoPlayer4.GetComponent<SimplePlayback>().videoId;
								//save.vUrl4 = tom.videoPlayer4.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl4 = tom.videoPlayer4.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer4.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer4.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.vId[4] = tom.videoPlayer5.GetComponent<SimplePlayback>().videoId;
								//save.vUrl5 = tom.videoPlayer5.GetComponent<HighQualityPlayback>().videoUrl;
								//save.aUrl5 = tom.videoPlayer5.GetComponent<HighQualityPlayback>().audioVideoUrl;
								x = tom.videoPlayer5.transform.rotation.eulerAngles.x;
								y = tom.videoPlayer5.transform.rotation.eulerAngles.y;
								z = tom.videoPlayer5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;

					//save model ID info and model rotation info to save class
					case "model":
						string modelId = "";
						switch(i)
						{
							case 0:
								save.mod1 = tom.modelId1;
								x = tom.model1.transform.rotation.eulerAngles.x;
								y = tom.model1.transform.rotation.eulerAngles.y;
								z = tom.model1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.mod2 = tom.modelId2;
								x = tom.model2.transform.rotation.eulerAngles.x;
								y = tom.model2.transform.rotation.eulerAngles.y;
								z = tom.model2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.mod3 = tom.modelId3;
								x = tom.model3.transform.rotation.eulerAngles.x;
								y = tom.model3.transform.rotation.eulerAngles.y;
								z = tom.model3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.mod4 = tom.modelId4;
								x = tom.model4.transform.rotation.eulerAngles.x;
								y = tom.model4.transform.rotation.eulerAngles.y;
								z = tom.model4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.mod5 = tom.modelId5;
								x = tom.model5.transform.rotation.eulerAngles.x;
								y = tom.model5.transform.rotation.eulerAngles.y;
								z = tom.model5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;
					case "image":
						switch(i)
						{
							case 0:
								save.imageUrl[0] = lm.scd.imageUrl[0];
								x = tom.image1.transform.rotation.eulerAngles.x;
								y = tom.image1.transform.rotation.eulerAngles.y;
								z = tom.image1.transform.rotation.eulerAngles.z;
								save.rot1[0] = x;
								save.rot1[1] = y;
								save.rot1[2] = z;
								break;
							case 1:
								save.imageUrl[1] = lm.scd.imageUrl[1];
								x = tom.image2.transform.rotation.eulerAngles.x;
								y = tom.image2.transform.rotation.eulerAngles.y;
								z = tom.image2.transform.rotation.eulerAngles.z;
								save.rot2[0] = x;
								save.rot2[1] = y;
								save.rot2[2] = z;
								break;
							case 2:
								save.imageUrl[2] = lm.scd.imageUrl[2];
								x = tom.image3.transform.rotation.eulerAngles.x;
								y = tom.image3.transform.rotation.eulerAngles.y;
								z = tom.image3.transform.rotation.eulerAngles.z;
								save.rot3[0] = x;
								save.rot3[1] = y;
								save.rot3[2] = z;
								break;
							case 3:
								save.imageUrl[3] = lm.scd.imageUrl[3];
								x = tom.image4.transform.rotation.eulerAngles.x;
								y = tom.image4.transform.rotation.eulerAngles.y;
								z = tom.image4.transform.rotation.eulerAngles.z;
								save.rot4[0] = x;
								save.rot4[1] = y;
								save.rot4[2] = z;
								break;
							case 4:
								save.imageUrl[4] = lm.scd.imageUrl[4];
								x = tom.image5.transform.rotation.eulerAngles.x;
								y = tom.image5.transform.rotation.eulerAngles.y;
								z = tom.image5.transform.rotation.eulerAngles.z;
								save.rot5[0] = x;
								save.rot5[1] = y;
								save.rot5[2] = z;
								break;
						}
						break;
				}
			}
		}
		string saveFilePath = Path.Combine(fm.SaveDirectory, "aoSave.json");
		string thisSave = JsonUtility.ToJson(save);
		File.WriteAllText(saveFilePath, thisSave);

		Debug.Log("AR Experience Saved");

		ceam.startExperienceEdit2();
	}

	public void deleteOldSave() 
	{
		if (Directory.Exists(fm.SaveDirectory))
			Directory.Delete(fm.SaveDirectory, true);
	}

}