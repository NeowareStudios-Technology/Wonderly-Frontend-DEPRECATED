using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;
using System.IO;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using Firebase.Storage;
using UnityEngine.SceneManagement;
using Sample;
using System.Threading.Tasks;

public class FirebaseStorageManager : MonoBehaviour {
	public FirebaseManager fbm;
	public FilesManager fm;
	public SaveManager sm;
	public ImageTargetManager itm;
	public CloudEndpointsApiManager ceam;
	public LoadManager lm;
	public Text codeDisplay;

	public string editCode;

	public string saveFolderPath;
	public string saveFilePath;
	private string targetPath1;
	private string targetPath2;
	private string targetPath3;
	private string targetPath4;
	private string targetPath5;

	public GameObject wrongCodeNotification;

	public GameObject viewScreenButton1;
	public GameObject viewScreenButton2;
	public GameObject viewScreenButton3;
	public GameObject viewScreenCoachmark;

	public GameObject libraryPanel;

	public ExperienceCodeClass ecc;

	private int whichIndex;

	private string saveApiUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/exp";


	// Use this for initialization
	void Start () {
		saveFolderPath = Path.Combine(fm.MarksDirectory, "SaveFolder");
		
		saveFilePath = Path.Combine(saveFolderPath, "aoSave.json");

		targetPath1 = Path.Combine(saveFolderPath, "targetPhoto1.jpg");
		targetPath2 = Path.Combine(saveFolderPath, "targetPhoto2.jpg");
		targetPath3 = Path.Combine(saveFolderPath, "targetPhoto3.jpg");
		targetPath4 = Path.Combine(saveFolderPath, "targetPhoto4.jpg");
		targetPath5 = Path.Combine(saveFolderPath, "targetPhoto5.jpg");
	}

	public void startExperienceUpload()
	{
		if (File.Exists(saveFilePath))
			Debug.Log("**1** fsm 65, Save file exists: "+saveFilePath);
		else	
			Debug.Log("**1** fsm 67, Save file missing: "+saveFilePath);
		StartCoroutine("experienceUpload");
	}

	//uploads experience meta data to Cloud Datastore using Cloud Endpoints AliceOne API
	public IEnumerator experienceUpload () {
		if (File.Exists(saveFilePath))
			Debug.Log("**2** fsm 74, Save file exists: "+saveFilePath);
		else	
			Debug.Log("**2** fsm 76, Save file missing: "+saveFilePath);

		Debug.Log("1. Starting fsm.experienceUpload()...");
		//reset code display
		codeDisplay.text = "Loading...";
		//create a new upload class instance
		UploadClassDeclaration upload = new UploadClassDeclaration();
		upload.title = sm.title.text;
		if (sm.browserLink.text == "")
			upload.weblink = false;
		else
			upload.weblink = true;

		//get target object counts
		int modelCount = 0;
		int videoCount = 0;
		int imageCount = 0;
		for(int i= 0; i < 5; i++)
		{
			switch(fm.targetStatus[i])
			{
				case "model":
					modelCount++;
					break;
				case "video":
					videoCount++;
					break;
				case "image":
					imageCount++;
					break;
			}

			if (fm.targetStatus[i] != "none")
			{
				switch(i)
				{
					case 0:
						upload.t1 = true;
						break;
					case 1:
						upload.t2 = true;
						break;
					case 2:
						upload.t3 = true;
						break;
					case 3:
						upload.t4 = true;
						break;
					case 4:
						upload.t5 = true;
						break;
				}
			}
			else
			{
				switch(i)
				{
					case 0:
						upload.t1 = false;
						break;
					case 1:
						upload.t2 = false;
						break;
					case 2:
						upload.t3 = false;
						break;
					case 3:
						upload.t4 = false;
						break;
					case 4:
						upload.t5 = false;
						break;
				}
			}
		}
		Debug.Log(modelCount);
		upload.model = modelCount;
		upload.video = videoCount;
		upload.image = imageCount;
		if (sm.description.text == "")
			upload.text = false;
		else
			upload.text = true;

		//convert upload clas instance into json string
		string uploadJson = JsonUtility.ToJson(upload);
		if (File.Exists(saveFilePath))
			Debug.Log("**3** fsm 164, Save file exists: "+saveFilePath);
		else	
			Debug.Log("**3** fsm 165, Save file missing: "+saveFilePath);

		Debug.Log("2. Experience info being saved to Goodle Datastore: " + uploadJson);

		//create web request
		using (UnityWebRequest experienceUploadRequest = UnityWebRequest.Put(saveApiUrl,uploadJson))
		{
			//set content type
			experienceUploadRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			experienceUploadRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);
			
			yield return experienceUploadRequest.SendWebRequest();

			byte[] results = experienceUploadRequest.downloadHandler.data;
      string jsonResponse = Encoding.UTF8.GetString(results);
			Debug.Log("3. Response from cloud endpoints after creating experience data entry: " +jsonResponse);
			ecc = JsonUtility.FromJson<ExperienceCodeClass>(jsonResponse);

			Debug.Log("4. code being saved for uploading the experience files: "+ecc.code);
			codeDisplay.text = ecc.code;

			if (File.Exists(saveFilePath))
				Debug.Log("**4** fsm 189, Save file exists: "+saveFilePath);
			else	
				Debug.Log("**4** fsm 191, Save file missing: "+saveFilePath);

			if (ecc.code != "")
				uploadExperienceFiles();
		}
	}

	//uploads experience files (target jpg files and save json) to firebase storage (filestore)
	public void uploadExperienceFiles()
	{
		Debug.Log("1. Starting fsm.uploadExperienceFiles");
		byte[] target1 = new byte[0];
		byte[] target2 = new byte[0];
		byte[] target3 = new byte[0];
		byte[] target4 = new byte[0];
		byte[] target5 = new byte[0];
		byte[] saveFile = new byte[0];

		if (File.Exists(saveFilePath))
		{	
			Debug.Log("2. save file exists: "+ saveFilePath);
			saveFile = System.IO.File.ReadAllBytes(saveFilePath);
		}
		else
		{
			Debug.Log("2. save file not found: "+ saveFilePath);
		}

		Firebase.Storage.StorageReference expRef = fbm.fbStorageRef.Child(ecc.code + "/" + "aoSave.json");
		expRef.PutBytesAsync(saveFile).ContinueWith ((Task<StorageMetadata> task) => {
    if (task.IsFaulted || task.IsCanceled) {
				Debug.Log("3. Could not upload the save file..");
        Debug.Log(task.Exception.ToString());
        // Uh-oh, an error occurred!
    } else {
        // Metadata contains file metadata such as size, content-type, and download URL.
        Firebase.Storage.StorageMetadata metadata = task.Result;
        Debug.Log("3. Finished uploading save file...");
    }
		});

		//convert any target jpegs that exist in save folder to byte arrays
		if (File.Exists(targetPath1))
		{
			target1 = System.IO.File.ReadAllBytes(targetPath1);
			Firebase.Storage.StorageReference target1Ref = fbm.fbStorageRef.Child(ecc.code + "/" + "targetPhoto1.jpg");
			target1Ref.PutBytesAsync(target1).ContinueWith ((Task<StorageMetadata> task) => {
			if (task.IsFaulted || task.IsCanceled) {
					Debug.Log(task.Exception.ToString());
					// Uh-oh, an error occurred!
			} else {
					// Metadata contains file metadata such as size, content-type, and download URL.
					Firebase.Storage.StorageMetadata metadata = task.Result;
					Debug.Log("Finished uploading target 1...");
    		}		
			});
		}
		if (File.Exists(targetPath2))
		{
			target2 = System.IO.File.ReadAllBytes(targetPath2);
			Firebase.Storage.StorageReference target2Ref = fbm.fbStorageRef.Child(ecc.code + "/" + "targetPhoto2.jpg");
			target2Ref.PutBytesAsync(target2).ContinueWith ((Task<StorageMetadata> task) => {
			if (task.IsFaulted || task.IsCanceled) {
					Debug.Log(task.Exception.ToString());
					// Uh-oh, an error occurred!
			} else {
					// Metadata contains file metadata such as size, content-type, and download URL.
					Firebase.Storage.StorageMetadata metadata = task.Result;
					Debug.Log("Finished uploading target 2...");
    		}		
			});
		}
		if (File.Exists(targetPath3))
		{
			target3 = System.IO.File.ReadAllBytes(targetPath3);
			Firebase.Storage.StorageReference target3Ref = fbm.fbStorageRef.Child(ecc.code + "/" + "targetPhoto3.jpg");
			target3Ref.PutBytesAsync(target3).ContinueWith ((Task<StorageMetadata> task) => {
			if (task.IsFaulted || task.IsCanceled) {
					Debug.Log(task.Exception.ToString());
					// Uh-oh, an error occurred!
			} else {
					// Metadata contains file metadata such as size, content-type, and download URL.
					Firebase.Storage.StorageMetadata metadata = task.Result;
					Debug.Log("Finished uploading target 3...");
    		}		
			});
		}
		if (File.Exists(targetPath4))
		{
			target4 = System.IO.File.ReadAllBytes(targetPath4);
			Firebase.Storage.StorageReference target4Ref = fbm.fbStorageRef.Child(ecc.code + "/" + "targetPhoto4.jpg");
			target4Ref.PutBytesAsync(target4).ContinueWith ((Task<StorageMetadata> task) => {
			if (task.IsFaulted || task.IsCanceled) {
					Debug.Log(task.Exception.ToString());
					// Uh-oh, an error occurred!
			} else {
					// Metadata contains file metadata such as size, content-type, and download URL.
					Firebase.Storage.StorageMetadata metadata = task.Result;
					Debug.Log("Finished uploading target 4...");
    		}		
			});
		}
		if (File.Exists(targetPath5))
		{
			target5 = System.IO.File.ReadAllBytes(targetPath5);
			Firebase.Storage.StorageReference target5Ref = fbm.fbStorageRef.Child(ecc.code + "/" + "targetPhoto5.jpg");
			target5Ref.PutBytesAsync(target5).ContinueWith ((Task<StorageMetadata> task) => {
			if (task.IsFaulted || task.IsCanceled) {
					Debug.Log(task.Exception.ToString());
					// Uh-oh, an error occurred!
			} else {
					// Metadata contains file metadata such as size, content-type, and download URL.
					Firebase.Storage.StorageMetadata metadata = task.Result;
					Debug.Log("Finished uploading target 5...");
    		}		
			});
		}

		itm.DeleteAllTargetsAndText();
	}

	public void startDownloadExperienceFiles()
	{
		StartCoroutine("downloadExperienceFiles");
	}

	private IEnumerator downloadExperienceFiles()
	{

		if (Directory.Exists(fm.SaveDirectory))
			Directory.Delete(fm.SaveDirectory, true);
		
		Debug.Log("in downloadExperienceFiles");
		
		Directory.CreateDirectory(fm.SaveDirectory);

		//references to local paths
		string saveFilePath = Path.Combine(fm.SaveDirectory, "aoSave.json");
		string targetPath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string targetPath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string targetPath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string targetPath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string targetPath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");

		//references to cloud filstore paths
		Firebase.Storage.StorageReference saveFileRef = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "aoSave.json");
		Firebase.Storage.StorageReference targetRef1 = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "targetPhoto1.jpg");
		Firebase.Storage.StorageReference targetRef2 = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "targetPhoto2.jpg");
		Firebase.Storage.StorageReference targetRef3 = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "targetPhoto3.jpg");
		Firebase.Storage.StorageReference targetRef4 = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "targetPhoto4.jpg");
		Firebase.Storage.StorageReference targetRef5 = fbm.fbStorage.GetReference(ceam.UiCode.text + "/" + "targetPhoto5.jpg");

		int maxAllowedSize = 1024*1024;
		byte[] saveFileBytes = new byte[maxAllowedSize];
		byte[] targetBytes1 = new byte[maxAllowedSize];
		byte[] targetBytes2 = new byte[maxAllowedSize];
		byte[] targetBytes3 = new byte[maxAllowedSize];
		byte[] targetBytes4 = new byte[maxAllowedSize];
		byte[] targetBytes5 = new byte[maxAllowedSize];

		targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents);

			}
});

targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target2 finished downloading!");
					File.WriteAllBytes(targetPath2, fileContents);

			}
});

targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target3 finished downloading!");
					File.WriteAllBytes(targetPath3, fileContents);

			}
});

targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target4 finished downloading!");
					File.WriteAllBytes(targetPath4, fileContents);

			}
});

targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target5 finished downloading!");
					File.WriteAllBytes(targetPath5, fileContents);

			}
});

yield return new WaitForSeconds(5);

saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				wrongCodeNotification.SetActive(true);
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);
				lm.LoadFile();
			}
});

/* 
		saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);
				targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task2) => {
				if (task2.IsFaulted || task2.IsCanceled) {
					Debug.Log(task2.Exception.ToString());
					// Uh-oh, an error occurred!
				} else {
					byte[] fileContents1 = task2.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents1);

					targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task3) => {
					if (task3.IsFaulted || task3.IsCanceled) {
						Debug.Log(task3.Exception.ToString());
						// Uh-oh, an error occurred!
					} else {
						byte[] fileContents2 = task3.Result;
						Debug.Log("target2 finished downloading!");
						File.WriteAllBytes(targetPath2, fileContents2);

						targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task4) => {
						if (task4.IsFaulted || task4.IsCanceled) {
							Debug.Log(task4.Exception.ToString());
							// Uh-oh, an error occurred!
						} else {
							byte[] fileContents3 = task4.Result;
							Debug.Log("target3 finished downloading!");
							File.WriteAllBytes(targetPath3, fileContents3);

							targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task5) => {
							if (task5.IsFaulted || task5.IsCanceled) {
								Debug.Log(task5.Exception.ToString());
								// Uh-oh, an error occurred!
							} else {
								byte[] fileContents4 = task5.Result;
								Debug.Log("target4 finished downloading!");
								File.WriteAllBytes(targetPath4, fileContents4);

								targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task6) => {
								if (task6.IsFaulted || task6.IsCanceled) {
									Debug.Log(task6.Exception.ToString());
									// Uh-oh, an error occurred!
								} else {
									byte[] fileContents5 = task6.Result;
									Debug.Log("target5 finished downloading!");
									File.WriteAllBytes(targetPath5, fileContents5);

									lm.LoadFile();
								}
								});
							}
							});
						}
						});
					}
					});
				}
				});
			}
		});


		saveFileRef.GetFileAsync(saveFilePath).ContinueWith(task => {
			if (!task.IsFaulted && !task.IsCanceled) 
			{
					Debug.Log("Save file downloaded.");

					targetRef1.GetFileAsync(targetPath1).ContinueWith(task1 => {
						if (!task1.IsFaulted && !task1.IsCanceled) {
								Debug.Log("Target 1 file downloaded.");
						}
						targetRef2.GetFileAsync(targetPath2).ContinueWith(task2 => {
							if (!task2.IsFaulted && !task2.IsCanceled) {
									Debug.Log("Target 2 file downloaded.");
							}
							targetRef3.GetFileAsync(targetPath3).ContinueWith(task3 => {
								if (!task3.IsFaulted && !task3.IsCanceled) {
										Debug.Log("Target 3 file downloaded.");
								}
								targetRef4.GetFileAsync(targetPath4).ContinueWith(task4 => {
									if (!task4.IsFaulted && !task4.IsCanceled) {
											Debug.Log("Target 4 file downloaded.");
									}
									targetRef5.GetFileAsync(targetPath5).ContinueWith(task5 => {
										if (!task5.IsFaulted && !task5.IsCanceled) {
												Debug.Log("Target 5 file downloaded.");
										}
										lm.LoadFile();
										});
									});
								});
							});
						});
			}
			else
			{
				Debug.Log("could not find experience");
				viewScreenButton1.SetActive(false);
				viewScreenButton2.SetActive(false);
				viewScreenButton3.SetActive(false);
				viewScreenCoachmark.SetActive(false);
				wrongCodeNotification.SetActive(true);
			}
		});
	*/

		yield return null;
	
			
	}




	public void startDownloadExperienceFilesForEdit(string codeToEdit)
	{
		editCode = codeToEdit;
		StartCoroutine("downloadExperienceFilesForEdit");
	}

	private IEnumerator downloadExperienceFilesForEdit()
	{

		if (Directory.Exists(fm.SaveDirectory))
			Directory.Delete(fm.SaveDirectory, true);
		
		Debug.Log("in downloadExperienceFiles");
		
		Directory.CreateDirectory(fm.SaveDirectory);

		//references to local paths
		string saveFilePath = Path.Combine(fm.SaveDirectory, "aoSave.json");
		string targetPath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string targetPath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string targetPath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string targetPath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string targetPath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");

		//references to cloud filstore paths
		Firebase.Storage.StorageReference saveFileRef = fbm.fbStorage.GetReference(editCode + "/" + "aoSave.json");
		Firebase.Storage.StorageReference targetRef1 = fbm.fbStorage.GetReference(editCode + "/" + "targetPhoto1.jpg");
		Firebase.Storage.StorageReference targetRef2 = fbm.fbStorage.GetReference(editCode + "/" + "targetPhoto2.jpg");
		Firebase.Storage.StorageReference targetRef3 = fbm.fbStorage.GetReference(editCode + "/" + "targetPhoto3.jpg");
		Firebase.Storage.StorageReference targetRef4 = fbm.fbStorage.GetReference(editCode + "/" + "targetPhoto4.jpg");
		Firebase.Storage.StorageReference targetRef5 = fbm.fbStorage.GetReference(editCode + "/" + "targetPhoto5.jpg");

		Debug.Log(editCode);
		codeDisplay.text = editCode;

		int maxAllowedSize = 1024*1024;
		byte[] saveFileBytes = new byte[maxAllowedSize];
		byte[] targetBytes1 = new byte[maxAllowedSize];
		byte[] targetBytes2 = new byte[maxAllowedSize];
		byte[] targetBytes3 = new byte[maxAllowedSize];
		byte[] targetBytes4 = new byte[maxAllowedSize];
		byte[] targetBytes5 = new byte[maxAllowedSize];

		targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents);

			}
});

targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target2 finished downloading!");
					File.WriteAllBytes(targetPath2, fileContents);

			}
});

targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target3 finished downloading!");
					File.WriteAllBytes(targetPath3, fileContents);

			}
});

targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target4 finished downloading!");
					File.WriteAllBytes(targetPath4, fileContents);

			}
});

targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target5 finished downloading!");
					File.WriteAllBytes(targetPath5, fileContents);

			}
});

yield return new WaitForSeconds(5);

saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);
				lm.LoadFile();
			}
});

/* 

		saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);

				targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task2) => {
				if (task2.IsFaulted || task2.IsCanceled) {
					Debug.Log(task2.Exception.ToString());
					// Uh-oh, an error occurred!
				} else {
					byte[] fileContents1 = task2.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents1);

					targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task3) => {
					if (task3.IsFaulted || task3.IsCanceled) {
						Debug.Log(task3.Exception.ToString());
						// Uh-oh, an error occurred!
					} else {
						byte[] fileContents2 = task3.Result;
						Debug.Log("target2 finished downloading!");
						File.WriteAllBytes(targetPath2, fileContents2);

						targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task4) => {
						if (task4.IsFaulted || task4.IsCanceled) {
							Debug.Log(task4.Exception.ToString());
							// Uh-oh, an error occurred!
						} else {
							byte[] fileContents3 = task4.Result;
							Debug.Log("target3 finished downloading!");
							File.WriteAllBytes(targetPath3, fileContents3);

							targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task5) => {
							if (task5.IsFaulted || task5.IsCanceled) {
								Debug.Log(task5.Exception.ToString());
								// Uh-oh, an error occurred!
							} else {
								byte[] fileContents4 = task5.Result;
								Debug.Log("target4 finished downloading!");
								File.WriteAllBytes(targetPath4, fileContents4);

								targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task6) => {
								if (task6.IsFaulted || task6.IsCanceled) {
									Debug.Log(task6.Exception.ToString());
									// Uh-oh, an error occurred!
								} else {
									byte[] fileContents5 = task6.Result;
									Debug.Log("target5 finished downloading!");
									File.WriteAllBytes(targetPath5, fileContents5);

									lm.LoadFile();
								}
								});
							}
							});
						}
						});
					}
					});
				}
				});
			}
		});

		saveFileRef.GetFileAsync(saveFilePath).ContinueWith(task => {
    if (!task.IsFaulted && !task.IsCanceled) {
			Debug.Log("Save file downloaded.");
			targetRef1.GetFileAsync(targetPath1).ContinueWith(task1 => {
				if (!task.IsFaulted && !task.IsCanceled) {
						Debug.Log("Target 1 file downloaded.");
				}
				targetRef2.GetFileAsync(targetPath2).ContinueWith(task2 => {
					if (!task.IsFaulted && !task.IsCanceled) {
							Debug.Log("Target 2 file downloaded.");
					}
					targetRef3.GetFileAsync(targetPath3).ContinueWith(task3 => {
						if (!task.IsFaulted && !task.IsCanceled) {
								Debug.Log("Target 3 file downloaded.");
						}
						targetRef4.GetFileAsync(targetPath4).ContinueWith(task4 => {
							if (!task.IsFaulted && !task.IsCanceled) {
									Debug.Log("Target 4 file downloaded.");
							}
							targetRef5.GetFileAsync(targetPath5).ContinueWith(task5 => {
								if (!task.IsFaulted && !task.IsCanceled) {
										Debug.Log("Target 5 file downloaded.");
								}
								lm.LoadFile();
							});
						});
					});
				});
			});
    }
		});
*/
	}











	public void startDownloadExperienceFilesDirect(int index)
	{
		whichIndex = index;
		StartCoroutine("downloadExperienceFilesDirect");
	}

	private IEnumerator downloadExperienceFilesDirect()
	{
		//for debugging iOS download problem
		Debug.Log("1. fsm484, Starting downloadExperienceFilesDirect()");

		if (Directory.Exists(fm.SaveDirectory))
			Directory.Delete(fm.SaveDirectory, true);
		
		Directory.CreateDirectory(fm.SaveDirectory);

		//references to local paths
		string saveFilePath = Path.Combine(fm.SaveDirectory, "aoSave.json");
		string targetPath1 = Path.Combine(fm.SaveDirectory, "targetPhoto1.jpg");
		string targetPath2 = Path.Combine(fm.SaveDirectory, "targetPhoto2.jpg");
		string targetPath3 = Path.Combine(fm.SaveDirectory, "targetPhoto3.jpg");
		string targetPath4 = Path.Combine(fm.SaveDirectory, "targetPhoto4.jpg");
		string targetPath5 = Path.Combine(fm.SaveDirectory, "targetPhoto5.jpg");

		//for debugging iOS download problem
		Debug.Log("2a. fsm500, Save file path = "+saveFilePath);
		//for debugging iOS download problem
		Debug.Log("2b. fsm502, Target 1 path = "+targetPath1);
		//for debugging iOS download problem
		Debug.Log("2c. fsm504, Target 2 path = "+targetPath2);
		//for debugging iOS download problem
		Debug.Log("2d. fsm506, Target 3 path = "+targetPath3);
		//for debugging iOS download problem
		Debug.Log("2e. fsm508, Target 4 path = "+targetPath4);
		//for debugging iOS download problem
		Debug.Log("2f. fsm510, Target 5 path = "+targetPath5);

		string expCode = "";
		switch(whichIndex)
		{
			case 1:
				expCode = ceam.code1.text;
				break;
			case 2:
				expCode = ceam.code2.text;
				break;
			case 3:
				expCode = ceam.code3.text;
				break;
			case 4:
				expCode = ceam.code4.text;
				break;
			case 5:
				expCode = ceam.code5.text;
				break;
			case 6:
				expCode = ceam.code6.text;
				break;
			case 7:
				expCode = ceam.code7.text;
				break;
			case 8:
				expCode = ceam.code8.text;
				break;
			case 9:
				expCode = ceam.code9.text;
				break;
			case 10:
				expCode = ceam.code10.text;
				break;
			case 11:
				expCode = ceam.code11.text;
				break;
			case 12:
				expCode = ceam.code12.text;
				break;
			case 13:
				expCode = ceam.code13.text;
				break;
			case 14:
				expCode = ceam.code14.text;
				break;
			case 15:
				expCode = ceam.code15.text;
				break;
			case 16:
				expCode = ceam.code16.text;
				break;
			case 17:
				expCode = ceam.code17.text;
				break;
			case 18:
				expCode = ceam.code18.text;
				break;
			case 19:
				expCode = ceam.code19.text;
				break;
			case 20:
				expCode = ceam.code20.text;
				break;
			case 21:
				expCode = ceam.code21.text;
				break;
			case 22:
				expCode = ceam.code22.text;
				break;
			case 23:
				expCode = ceam.code23.text;
				break;
			case 24:
				expCode = ceam.code24.text;
				break;
			case 25:
				expCode = ceam.code25.text;
				break;
			case 26:
				expCode = ceam.code26.text;
				break;
			case 27:
				expCode = ceam.code27.text;
				break;
			case 28:
				expCode = ceam.code28.text;
				break;
			case 29:
				expCode = ceam.code29.text;
				break;
			case 30:
				expCode = ceam.code30.text;
				break;
			case 31:
				expCode = ceam.code31.text;
				break;
			case 32:
				expCode = ceam.code32.text;
				break;
			case 33:
				expCode = ceam.code33.text;
				break;
			case 34:
				expCode = ceam.code34.text;
				break;
			case 35:
				expCode = ceam.code35.text;
				break;
			case 36:
				expCode = ceam.code36.text;
				break;
			case 37:
				expCode = ceam.code37.text;
				break;
			case 38:
				expCode = ceam.code38.text;
				break;
			case 39:
				expCode = ceam.code39.text;
				break;
			case 40:
				expCode = ceam.code40.text;
				break;
			case 41:
				expCode = ceam.code41.text;
				break;
			case 42:
				expCode = ceam.code42.text;
				break;
			case 43:
				expCode = ceam.code43.text;
				break;
			case 44:
				expCode = ceam.code44.text;
				break;
			case 45:
				expCode = ceam.code45.text;
				break;
			case 46:
				expCode = ceam.code46.text;
				break;
			case 47:
				expCode = ceam.code47.text;
				break;
			case 48:
				expCode = ceam.code48.text;
				break;
			case 49:
				expCode = ceam.code49.text;
				break;
			case 50:
				expCode = ceam.code50.text;
				break;
		}

		//for debugging iOS download problem
		Debug.Log("3. fsm668, Code from library = "+expCode);

		//deactivate the library stubs so they do not get in the way of buttons
		libraryPanel.SetActive(false);

		//references to cloud filstore paths
		Firebase.Storage.StorageReference saveFileRef = fbm.fbStorage.GetReference(expCode + "/" + "aoSave.json");
		Firebase.Storage.StorageReference targetRef1 = fbm.fbStorage.GetReference(expCode + "/" + "targetPhoto1.jpg");
		Firebase.Storage.StorageReference targetRef2 = fbm.fbStorage.GetReference(expCode + "/" + "targetPhoto2.jpg");
		Firebase.Storage.StorageReference targetRef3 = fbm.fbStorage.GetReference(expCode + "/" + "targetPhoto3.jpg");
		Firebase.Storage.StorageReference targetRef4 = fbm.fbStorage.GetReference(expCode + "/" + "targetPhoto4.jpg");
		Firebase.Storage.StorageReference targetRef5 = fbm.fbStorage.GetReference(expCode + "/" + "targetPhoto5.jpg");

		//for debugging iOS download problem
		Debug.Log("4a. fsm682, Save file download path = "+expCode + "/" + "aoSave.json");
		//for debugging iOS download problem
		Debug.Log("4b. fsm684, Target 1 download path = "+expCode + "/" + "targetPhoto1.jpg");
		//for debugging iOS download problem
		Debug.Log("4c. fsm686, Target 2 download path = "+expCode + "/" + "targetPhoto2.jpg");
		//for debugging iOS download problem
		Debug.Log("4d. fsm688, Target 3 download path = "+expCode + "/" + "targetPhoto3.jpg");
		//for debugging iOS download problem
		Debug.Log("4e. fsm690, Target 4 download path = "+expCode + "/" + "targetPhoto4.jpg");
		//for debugging iOS download problem
		Debug.Log("4f. fsm692, Target 5 download path = "+expCode + "/" + "targetPhoto5.jpg");

		int maxAllowedSize = 1024*1024;
		byte[] saveFileBytes = new byte[maxAllowedSize];
		byte[] targetBytes1 = new byte[maxAllowedSize];
		byte[] targetBytes2 = new byte[maxAllowedSize];
		byte[] targetBytes3 = new byte[maxAllowedSize];
		byte[] targetBytes4 = new byte[maxAllowedSize];
		byte[] targetBytes5 = new byte[maxAllowedSize];


targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents);

			}
});

targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target2 finished downloading!");
					File.WriteAllBytes(targetPath2, fileContents);

			}
});

targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target3 finished downloading!");
					File.WriteAllBytes(targetPath3, fileContents);

			}
});

targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target4 finished downloading!");
					File.WriteAllBytes(targetPath4, fileContents);

			}
});

targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
					Debug.Log("target5 finished downloading!");
					File.WriteAllBytes(targetPath5, fileContents);

			}
});

yield return new WaitForSeconds(5);

saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);
				lm.LoadFile();
			}
});

/* 

		saveFileRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task1) => {
			if (task1.IsFaulted || task1.IsCanceled) {
				Debug.Log(task1.Exception.ToString());
				// Uh-oh, an error occurred!
			} else {
				byte[] fileContents = task1.Result;
				string saveContent = System.Text.Encoding.UTF8.GetString(fileContents, 0, fileContents.Length);
				Debug.Log("Save File finished downloading!");
				File.WriteAllText(saveFilePath, saveContent);

				targetRef1.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task2) => {
				if (task2.IsFaulted || task2.IsCanceled) {
					Debug.Log(task2.Exception.ToString());
					// Uh-oh, an error occurred!
				} else {
					byte[] fileContents1 = task2.Result;
					Debug.Log("target1 finished downloading!");
					File.WriteAllBytes(targetPath1, fileContents1);

					targetRef2.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task3) => {
					if (task3.IsFaulted || task3.IsCanceled) {
						Debug.Log(task3.Exception.ToString());
						// Uh-oh, an error occurred!
					} else {
						byte[] fileContents2 = task3.Result;
						Debug.Log("target2 finished downloading!");
						File.WriteAllBytes(targetPath2, fileContents2);

						targetRef3.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task4) => {
						if (task4.IsFaulted || task4.IsCanceled) {
							Debug.Log(task4.Exception.ToString());
							// Uh-oh, an error occurred!
						} else {
							byte[] fileContents3 = task4.Result;
							Debug.Log("target3 finished downloading!");
							File.WriteAllBytes(targetPath3, fileContents3);

							targetRef4.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task5) => {
							if (task5.IsFaulted || task5.IsCanceled) {
								Debug.Log(task5.Exception.ToString());
								// Uh-oh, an error occurred!
							} else {
								byte[] fileContents4 = task5.Result;
								Debug.Log("target4 finished downloading!");
								File.WriteAllBytes(targetPath4, fileContents4);

								targetRef5.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task6) => {
								if (task6.IsFaulted || task6.IsCanceled) {
									Debug.Log(task6.Exception.ToString());
									// Uh-oh, an error occurred!
								} else {
									byte[] fileContents5 = task6.Result;
									Debug.Log("target5 finished downloading!");
									File.WriteAllBytes(targetPath5, fileContents5);

									lm.LoadFile();
								}
								});
							}
							});
						}
						});
					}
					});
				}
				});
			}
		});

		saveFileRef.GetFileAsync(saveFilePath).ContinueWith(task => {
    if (!task.IsFaulted && !task.IsCanceled) {
			//for debugging iOS download problem
			Debug.Log("5. fsm697, Save file downloaded.");
			string debug = File.ReadAllText(saveFilePath);
			Debug.Log("6. fsm700, Contents of downloaded save file = "+debug);
			targetRef1.GetFileAsync(targetPath1).ContinueWith(task1 => {
				if (!task.IsFaulted && !task.IsCanceled) {
						Debug.Log("7. fsm703, Target 1 file downloaded.");
				}
				targetRef2.GetFileAsync(targetPath2).ContinueWith(task2 => {
					if (!task.IsFaulted && !task.IsCanceled) {
							Debug.Log("8, fsm707, Target 2 file downloaded.");
					}
					targetRef3.GetFileAsync(targetPath3).ContinueWith(task3 => {
						if (!task.IsFaulted && !task.IsCanceled) {
								Debug.Log("9, fsm711, Target 3 file downloaded.");
						}
						targetRef4.GetFileAsync(targetPath4).ContinueWith(task4 => {
							if (!task.IsFaulted && !task.IsCanceled) {
									Debug.Log("10, fsm715, Target 4 file downloaded.");
							}
							targetRef5.GetFileAsync(targetPath5).ContinueWith(task5 => {
								if (!task.IsFaulted && !task.IsCanceled) {
										Debug.Log("11, fsm719, Target 5 file downloaded.");
								}
								lm.LoadFile();
							});
						});
					});
				});
			});
    }
		else
		{
			Debug.Log("5. fsm732, Save File failed to download: error = "+ task.Exception);
		}
		});
		*/
		yield return null;
		whichIndex = 0;
	}

	public void DeleteExperience(string code)
	{
		Firebase.Storage.StorageReference saveRef = fbm.fbStorageRef.Child(code + "/" + "aoSave.json");
		Firebase.Storage.StorageReference photoRef1 = fbm.fbStorageRef.Child(code + "/" + "targetPhoto1.jpg");
		Firebase.Storage.StorageReference photoRef2 = fbm.fbStorageRef.Child(code + "/" + "targetPhoto2.jpg");
		Firebase.Storage.StorageReference photoRef3 = fbm.fbStorageRef.Child(code + "/" + "targetPhoto3.jpg");
		Firebase.Storage.StorageReference photoRef4 = fbm.fbStorageRef.Child(code + "/" + "targetPhoto4.jpg");
		Firebase.Storage.StorageReference photoRef5 = fbm.fbStorageRef.Child(code + "/" + "targetPhoto5.jpg");


		// Delete the file
		saveRef.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
		// Delete the file
		photoRef1.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
		// Delete the file
		photoRef2.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
		// Delete the file
		photoRef3.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
		// Delete the file
		photoRef4.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
		// Delete the file
		photoRef5.DeleteAsync().ContinueWith(task => {
    if (task.IsCompleted) {
        Debug.Log("File deleted successfully.");
    } else {
        // Uh-oh, an error occurred!
    }
		});	
	}
}
