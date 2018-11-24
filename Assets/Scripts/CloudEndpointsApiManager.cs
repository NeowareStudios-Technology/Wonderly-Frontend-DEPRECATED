using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using Sample;
using System.Threading.Tasks;

public class CloudEndpointsApiManager : MonoBehaviour {
	public FirebaseManager fbm;
	public FirebaseStorageManager fsm;
	public SaveManager sm;
	public FilesManager fm;
	public SignUpManager sum;
	public Text firstName;
	public Text lastName;
	public Text displayFirstName;
	public Text displayLastName;
	public Text displayExpNum;
	public Text password;
	public Text email;
	public Text UiCode;

	public GameObject loadingPanel;

	public GameObject libraryPanel;

	public string code;

	public GameObject ref1;
	public GameObject ref2;
	public GameObject ref3;
	public GameObject ref4;
	public GameObject ref5;
	public GameObject ref6;
	public GameObject ref7;
	public GameObject ref8;
	public GameObject ref9;
	public GameObject ref10;
	public GameObject ref11;
	public GameObject ref12;
	public GameObject ref13;
	public GameObject ref14;
	public GameObject ref15;
	public GameObject ref16;
	public GameObject ref17;
	public GameObject ref18;
	public GameObject ref19;
	public GameObject ref20;
	public GameObject ref21;
	public GameObject ref22;
	public GameObject ref23;
	public GameObject ref24;
	public GameObject ref25;
	public GameObject ref26;
	public GameObject ref27;
	public GameObject ref28;
	public GameObject ref29;
	public GameObject ref30;
	public GameObject ref31;
	public GameObject ref32;
	public GameObject ref33;
	public GameObject ref34;
	public GameObject ref35;
	public GameObject ref36;
	public GameObject ref37;
	public GameObject ref38;
	public GameObject ref39;
	public GameObject ref40;
	public GameObject ref41;
	public GameObject ref42;
	public GameObject ref43;
	public GameObject ref44;
	public GameObject ref45;
	public GameObject ref46;
	public GameObject ref47;
	public GameObject ref48;
	public GameObject ref49;
	public GameObject ref50;

	public Text title1;
	public Text title2;
	public Text title3;
	public Text title4;
	public Text title5;
	public Text title6;
	public Text title7;
	public Text title8;
	public Text title9;
	public Text title10;
	public Text title11;
	public Text title12;
	public Text title13;
	public Text title14;
	public Text title15;
	public Text title16;
	public Text title17;
	public Text title18;
	public Text title19;
	public Text title20;
	public Text title21;
	public Text title22;
	public Text title23;
	public Text title24;
	public Text title25;
	public Text title26;
	public Text title27;
	public Text title28;
	public Text title29;
	public Text title30;
	public Text title31;
	public Text title32;
	public Text title33;
	public Text title34;
	public Text title35;
	public Text title36;
	public Text title37;
	public Text title38;
	public Text title39;
	public Text title40;
	public Text title41;
	public Text title42;
	public Text title43;
	public Text title44;
	public Text title45;
	public Text title46;
	public Text title47;
	public Text title48;
	public Text title49;
	public Text title50;


	public Text date1;
	public Text date2;
	public Text date3;
	public Text date4;
	public Text date5;
	public Text date6;
	public Text date7;
	public Text date8;
	public Text date9;
	public Text date10;
	public Text date11;
	public Text date12;
	public Text date13;
	public Text date14;
	public Text date15;
	public Text date16;
	public Text date17;
	public Text date18;
	public Text date19;
	public Text date20;
	public Text date21;
	public Text date22;
	public Text date23;
	public Text date24;
	public Text date25;
	public Text date26;
	public Text date27;
	public Text date28;
	public Text date29;
	public Text date30;
	public Text date31;
	public Text date32;
	public Text date33;
	public Text date34;
	public Text date35;
	public Text date36;
	public Text date37;
	public Text date38;
	public Text date39;
	public Text date40;
	public Text date41;
	public Text date42;
	public Text date43;
	public Text date44;
	public Text date45;
	public Text date46;
	public Text date47;
	public Text date48;
	public Text date49;
	public Text date50;

	public Text code1;
	public Text code2;
	public Text code3;
	public Text code4;
	public Text code5;
	public Text code6;
	public Text code7;
	public Text code8;
	public Text code9;
	public Text code10;
	public Text code11;
	public Text code12;
	public Text code13;
	public Text code14;
	public Text code15;
	public Text code16;
	public Text code17;
	public Text code18;
	public Text code19;
	public Text code20;
	public Text code21;
	public Text code22;
	public Text code23;
	public Text code24;
	public Text code25;
	public Text code26;
	public Text code27;
	public Text code28;
	public Text code29;
	public Text code30;
	public Text code31;
	public Text code32;
	public Text code33;
	public Text code34;
	public Text code35;
	public Text code36;
	public Text code37;
	public Text code38;
	public Text code39;
	public Text code40;
	public Text code41;
	public Text code42;
	public Text code43;
	public Text code44;
	public Text code45;
	public Text code46;
	public Text code47;
	public Text code48;
	public Text code49;
	public Text code50;

	public string deleteCode;
	public string editCode;

	public OwnedExperiencesClass oec;
	public TargetIndicesClass tic;
	public ProfileInfoClass pic;
	public checkEmailClass cec;
	public checkEmailResponseClass cerc;

	private string getProfileUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/profile";
	private string createProfileUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/profile";
	private string getOwnedCodesUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/profile/codes";
	private string deleteExpUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/exp/delete";
	private string editExpUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/exp/edit";
	private string emailCheckUrl = "https://aliceone-221018.appspot.com/_ah/api/aliceOne/v1/profile/check";

	public bool checkEmail;

	public void startProfileCreate()
	{
		StartCoroutine("profileCreate");
	}

	public IEnumerator profileCreate () 
	{
		ProfileClass newProfile = new ProfileClass();
		newProfile.firstName = firstName.text;
		newProfile.lastName = lastName.text;

		//convert profile clas instance into json string
		string newProfJson = JsonUtility.ToJson(newProfile);

		using (UnityWebRequest newProfileRequest = UnityWebRequest.Put(createProfileUrl,newProfJson))
		{
			//set content type
			newProfileRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			newProfileRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);

			yield return newProfileRequest.SendWebRequest();

			Debug.Log(newProfileRequest.responseCode);
		}
	}


	public void startCheckEmail()
	{
		StartCoroutine("emailCheck");
	}

	public IEnumerator emailCheck() 
	{
		checkEmailClass emailCheck = new checkEmailClass();
		emailCheck.email = email.text;

		//convert profile clas instance into json string
		string emailCheckJson = JsonUtility.ToJson(emailCheck);

		using (UnityWebRequest emailCheckRequest = UnityWebRequest.Put(emailCheckUrl,emailCheckJson))
		{
			//set content type
			emailCheckRequest.SetRequestHeader("Content-Type", "application/json");

			yield return emailCheckRequest.SendWebRequest();

			Debug.Log(emailCheckRequest.responseCode);

			byte[] results = emailCheckRequest.downloadHandler.data;
			string jsonString = Encoding.UTF8.GetString(results);
			cerc = JsonUtility.FromJson<checkEmailResponseClass>(jsonString);
			Debug.Log(cerc.exists);
			if (cerc.exists == "n")
			{
				sum.signUp1.SetActive(false);
				sum.signUp2.SetActive(true);
				sum.signUpIndex++;
			}

			if (cerc.exists == "y")
				sum.existingEmailNotification.SetActive(true);
		}
	}


	public void startExperienceDelete(int index)
	{
		switch(index)
		{
			case 1:
				deleteCode = code1.text;
				break;
			case 2:
				deleteCode = code2.text;
				break;
			case 3:
				deleteCode = code3.text;
				break;
			case 4:
				deleteCode = code4.text;
				break;
			case 5:
				deleteCode = code5.text;
				break;
			case 6:
				deleteCode = code6.text;
				break;
			case 7:
				deleteCode = code7.text;
				break;
			case 8:
				deleteCode = code8.text;
				break;
			case 9:
				deleteCode = code9.text;
				break;
			case 10:
				deleteCode = code10.text;
				break;
			case 11:
				deleteCode = code11.text;
				break;
			case 12:
				deleteCode = code12.text;
				break;
			case 13:
				deleteCode = code13.text;
				break;
			case 14:
				deleteCode = code14.text;
				break;
			case 15:
				deleteCode = code15.text;
				break;
			case 16:
				deleteCode = code16.text;
				break;
			case 17:
				deleteCode = code17.text;
				break;
			case 18:
				deleteCode = code18.text;
				break;
			case 19:
				deleteCode = code19.text;
				break;
			case 20:
				deleteCode = code20.text;
				break;
			case 21:
				deleteCode = code21.text;
				break;
			case 22:
				deleteCode = code22.text;
				break;
			case 23:
				deleteCode = code23.text;
				break;
			case 24:
				deleteCode = code24.text;
				break;
			case 25:
				deleteCode = code25.text;
				break;
			case 26:
				deleteCode = code26.text;
				break;
			case 27:
				deleteCode = code27.text;
				break;
			case 28:
				deleteCode = code28.text;
				break;
			case 29:
				deleteCode = code29.text;
				break;
			case 30:
				deleteCode = code30.text;
				break;
			case 31:
				deleteCode = code31.text;
				break;
			case 32:
				deleteCode = code32.text;
				break;
			case 33:
				deleteCode = code33.text;
				break;
			case 34:
				deleteCode = code34.text;
				break;
			case 35:
				deleteCode = code35.text;
				break;
			case 36:
				deleteCode = code36.text;
				break;
			case 37:
				deleteCode = code37.text;
				break;
			case 38:
				deleteCode = code38.text;
				break;
			case 39:
				deleteCode = code39.text;
				break;
			case 40:
				deleteCode = code40.text;
				break;
			case 41:
				deleteCode = code41.text;
				break;
			case 42:
				deleteCode = code42.text;
				break;
			case 43:
				deleteCode = code43.text;
				break;
			case 44:
				deleteCode = code44.text;
				break;
			case 45:
				deleteCode = code45.text;
				break;
			case 46:
				deleteCode = code46.text;
				break;
			case 47:
				deleteCode = code47.text;
				break;
			case 48:
				deleteCode = code48.text;
				break;
			case 49:
				deleteCode = code49.text;
				break;
			case 50:
				deleteCode = code50.text;
				break;
			
		}
		StartCoroutine("experienceDelete");

		fsm.DeleteExperience(deleteCode);
	}

	public IEnumerator experienceDelete() 
	{
		ExperienceCodeClass expCode = new ExperienceCodeClass();
		expCode.code = deleteCode;

		//convert profile clas instance into json string
		string delExpJson = JsonUtility.ToJson(expCode);

		using (UnityWebRequest deleteExperienceRequest = UnityWebRequest.Put(deleteExpUrl,delExpJson))
		{
			//set content type
			deleteExperienceRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			deleteExperienceRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);

			yield return deleteExperienceRequest.SendWebRequest();
			byte[] results = deleteExperienceRequest.downloadHandler.data;
			string jsonString = Encoding.UTF8.GetString(results);
			Debug.Log(jsonString);
		}
	}


	public void startExperienceEdit(int index)
	{
		switch(index)
		{
			case 1:
				editCode = code1.text;
				break;
			case 2:
				editCode = code2.text;
				break;
			case 3:
				editCode = code3.text;
				break;
			case 4:
				editCode = code4.text;
				break;
			case 5:
				editCode = code5.text;
				break;
			case 6:
				editCode = code6.text;
				break;
			case 7:
				editCode = code7.text;
				break;
			case 8:
				editCode = code8.text;
				break;
			case 9:
				editCode = code9.text;
				break;
			case 10:
				editCode = code10.text;
				break;
			case 11:
				editCode = code11.text;
				break;
			case 12:
				editCode = code12.text;
				break;
			case 13:
				editCode = code13.text;
				break;
			case 14:
				editCode = code14.text;
				break;
			case 15:
				editCode = code15.text;
				break;
			case 16:
				editCode = code16.text;
				break;
			case 17:
				editCode = code17.text;
				break;
			case 18:
				editCode = code18.text;
				break;
			case 19:
				editCode = code19.text;
				break;
			case 20:
				editCode = code20.text;
				break;
			case 21:
				editCode = code21.text;
				break;
			case 22:
				editCode = code22.text;
				break;
			case 23:
				editCode = code23.text;
				break;
			case 24:
				editCode = code24.text;
				break;
			case 25:
				editCode = code25.text;
				break;
			case 26:
				editCode = code26.text;
				break;
			case 27:
				editCode = code27.text;
				break;
			case 28:
				editCode = code28.text;
				break;
			case 29:
				editCode = code29.text;
				break;
			case 30:
				editCode = code30.text;
				break;
			case 31:
				editCode = code31.text;
				break;
			case 32:
				editCode = code32.text;
				break;
			case 33:
				editCode = code33.text;
				break;
			case 34:
				editCode = code34.text;
				break;
			case 35:
				editCode = code35.text;
				break;
			case 36:
				editCode = code36.text;
				break;
			case 37:
				editCode = code37.text;
				break;
			case 38:
				editCode = code38.text;
				break;
			case 39:
				editCode = code39.text;
				break;
			case 40:
				editCode = code40.text;
				break;
			case 41:
				editCode = code41.text;
				break;
			case 42:
				editCode = code42.text;
				break;
			case 43:
				editCode = code43.text;
				break;
			case 44:
				editCode = code44.text;
				break;
			case 45:
				editCode = code45.text;
				break;
			case 46:
				editCode = code46.text;
				break;
			case 47:
				editCode = code47.text;
				break;
			case 48:
				editCode = code48.text;
				break;
			case 49:
				editCode = code49.text;
				break;
			case 50:
				editCode = code50.text;
				break;
			
		}
		fsm.ecc.code = editCode;
		UiCode.text = editCode;
		code = editCode;
		//deactivate the library stubs so they do not get in the way of buttons
		libraryPanel.SetActive(false);
		fsm.startDownloadExperienceFilesForEdit(editCode);
	}

	public void startExperienceEdit2()
	{
		StartCoroutine("experienceEdit");
		StartCoroutine("deleteAndUploadExperienceFiles");
	}

	private IEnumerator deleteAndUploadExperienceFiles()
	{
		fsm.DeleteExperience(code);
		yield return new WaitForSeconds(2);
		fsm.uploadExperienceFiles();
	}

	private IEnumerator experienceEdit() 
	{
		editExperienceClass editExperience = new editExperienceClass();
		editExperience.title = sm.title.text;
		editExperience.code = code;
		if (sm.browserLink.text == "")
			editExperience.weblink = false;
		else
			editExperience.weblink = true;

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
		}
		Debug.Log(modelCount);
		editExperience.model = modelCount;
		editExperience.video = videoCount;
		editExperience.image = imageCount;
		if (sm.description.text == "")
			editExperience.text = false;
		else
			editExperience.text = true;

		//convert editExperience clas instance into json string
		string editExperienceJson = JsonUtility.ToJson(editExperience);

		using (UnityWebRequest editExperienceRequest = UnityWebRequest.Put(editExpUrl,editExperienceJson))
		{
			//set content type
			editExperienceRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			editExperienceRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);

			yield return editExperienceRequest.SendWebRequest();

			Debug.Log(editExperienceRequest.responseCode);
		}
	}


public void startGetProfileInfo()
	{
		StartCoroutine("getProfileInfo");
	}

	public IEnumerator getProfileInfo() 
	{
		using (UnityWebRequest newProfileInfoRequest = UnityWebRequest.Get(getProfileUrl))
		{
			//set content type
			newProfileInfoRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			newProfileInfoRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);

			yield return newProfileInfoRequest.SendWebRequest();

			Debug.Log(newProfileInfoRequest.responseCode);
			byte[] results = newProfileInfoRequest.downloadHandler.data;
			string jsonString = Encoding.UTF8.GetString(results);
			Debug.Log(jsonString);
			pic = JsonUtility.FromJson<ProfileInfoClass>(jsonString);

			displayFirstName.text = pic.firstName;
			displayLastName.text = pic.lastName;
			displayExpNum.text = pic.createdExp + " experiences";
		}

		loadingPanel.SetActive(false);
	}










	public void startGetOwnedCodes()
	{
		StartCoroutine("getOwnedCodes");
	}

	public IEnumerator getOwnedCodes() 
	{
		int numExperiences = 0;
		using (UnityWebRequest getOwnedCodesRequest = UnityWebRequest.Get(getOwnedCodesUrl))
		{
			//set content type
			getOwnedCodesRequest.SetRequestHeader("Content-Type", "application/json");
			//set auth header
			getOwnedCodesRequest.SetRequestHeader("Authorization", "Bearer " + fbm.token);

			yield return getOwnedCodesRequest.SendWebRequest();

			Debug.Log(getOwnedCodesRequest.responseCode);

			byte[] results = getOwnedCodesRequest.downloadHandler.data;
			string jsonString = Encoding.UTF8.GetString(results);
			Debug.Log(jsonString);
			oec = JsonUtility.FromJson<OwnedExperiencesClass>(jsonString);

			//count how many experiences the user has 
			foreach(string code in oec.codes)
			{
				if (code == null)
					break;

				numExperiences++;
			}

			Debug.Log("Number of codes: "+numExperiences);
		}

		for (int i = 0; i < numExperiences; i++)
		{
			switch(i)
			{
				case 0:
					ref1.SetActive(true);
					title1.text=oec.titles[i];
					date1.text=oec.dates[i];
					code1.text=oec.codes[i];
					break;
				case 1:
					ref2.SetActive(true);
					title2.text=oec.titles[i];
					date2.text=oec.dates[i];
					code2.text=oec.codes[i];
					break;
				case 2:
					ref3.SetActive(true);
					title3.text=oec.titles[i];
					date3.text=oec.dates[i];
					code3.text=oec.codes[i];
					break;
				case 3:
					ref4.SetActive(true);
					title4.text=oec.titles[i];
					date4.text=oec.dates[i];
					code4.text=oec.codes[i];
					break;
				case 4:
					ref5.SetActive(true);
					title5.text=oec.titles[i];
					date5.text=oec.dates[i];
					code5.text=oec.codes[i];
					break;
				case 5:
					ref6.SetActive(true);
					title6.text=oec.titles[i];
					date6.text=oec.dates[i];
					code6.text=oec.codes[i];
					break;
				case 6:
					ref7.SetActive(true);
					title7.text=oec.titles[i];
					date7.text=oec.dates[i];
					code7.text=oec.codes[i];
					break;
				case 7:
					ref8.SetActive(true);
					title8.text=oec.titles[i];
					date8.text=oec.dates[i];
					code8.text=oec.codes[i];
					break;
				case 8:
					ref9.SetActive(true);
					title9.text=oec.titles[i];
					date9.text=oec.dates[i];
					code9.text=oec.codes[i];
					break;
				case 9:
					ref10.SetActive(true);
					title10.text=oec.titles[i];
					date10.text=oec.dates[i];
					code10.text=oec.codes[i];
					break;
				case 10:
					ref11.SetActive(true);
					title11.text=oec.titles[i];
					date11.text=oec.dates[i];
					code11.text=oec.codes[i];
					break;
				case 11:
					ref12.SetActive(true);
					title12.text=oec.titles[i];
					date12.text=oec.dates[i];
					code12.text=oec.codes[i];
					break;
				case 12:
					ref13.SetActive(true);
					title13.text=oec.titles[i];
					date13.text=oec.dates[i];
					code13.text=oec.codes[i];
					break;
				case 13:
					ref14.SetActive(true);
					title14.text=oec.titles[i];
					date14.text=oec.dates[i];
					code14.text=oec.codes[i];
					break;
				case 14:
					ref15.SetActive(true);
					title15.text=oec.titles[i];
					date15.text=oec.dates[i];
					code15.text=oec.codes[i];
					break;
				case 15:
					ref16.SetActive(true);
					title16.text=oec.titles[i];
					date16.text=oec.dates[i];
					code16.text=oec.codes[i];
					break;
				case 16:
					ref17.SetActive(true);
					title17.text=oec.titles[i];
					date17.text=oec.dates[i];
					code17.text=oec.codes[i];
					break;
				case 17:
					ref18.SetActive(true);
					title18.text=oec.titles[i];
					date18.text=oec.dates[i];
					code18.text=oec.codes[i];
					break;
				case 18:
					ref19.SetActive(true);
					title19.text=oec.titles[i];
					date19.text=oec.dates[i];
					code19.text=oec.codes[i];
					break;
				case 19:
					ref20.SetActive(true);
					title20.text=oec.titles[i];
					date20.text=oec.dates[i];
					code20.text=oec.codes[i];
					break;
				case 20:
					ref21.SetActive(true);
					title21.text=oec.titles[i];
					date21.text=oec.dates[i];
					code21.text=oec.codes[i];
					break;
				case 21:
					ref22.SetActive(true);
					title22.text=oec.titles[i];
					date22.text=oec.dates[i];
					code22.text=oec.codes[i];
					break;
				case 22:
					ref23.SetActive(true);
					title23.text=oec.titles[i];
					date23.text=oec.dates[i];
					code23.text=oec.codes[i];
					break;
				case 23:
					ref24.SetActive(true);
					title24.text=oec.titles[i];
					date24.text=oec.dates[i];
					code24.text=oec.codes[i];
					break;
				case 24:
					ref25.SetActive(true);
					title25.text=oec.titles[i];
					date25.text=oec.dates[i];
					code25.text=oec.codes[i];
					break;
				case 25:
					ref26.SetActive(true);
					title26.text=oec.titles[i];
					date26.text=oec.dates[i];
					code26.text=oec.codes[i];
					break;
				case 26:
					ref27.SetActive(true);
					title27.text=oec.titles[i];
					date27.text=oec.dates[i];
					code27.text=oec.codes[i];
					break;
				case 27:
					ref28.SetActive(true);
					title28.text=oec.titles[i];
					date28.text=oec.dates[i];
					code28.text=oec.codes[i];
					break;
				case 28:
					ref29.SetActive(true);
					title29.text=oec.titles[i];
					date29.text=oec.dates[i];
					code29.text=oec.codes[i];
					break;
				case 29:
					ref30.SetActive(true);
					title30.text=oec.titles[i];
					date30.text=oec.dates[i];
					code30.text=oec.codes[i];
					break;
				case 30:
					ref31.SetActive(true);
					title31.text=oec.titles[i];
					date31.text=oec.dates[i];
					code31.text=oec.codes[i];
					break;
				case 31:
					ref32.SetActive(true);
					title32.text=oec.titles[i];
					date32.text=oec.dates[i];
					code32.text=oec.codes[i];
					break;
				case 32:
					ref33.SetActive(true);
					title33.text=oec.titles[i];
					date33.text=oec.dates[i];
					code33.text=oec.codes[i];
					break;
				case 33:
					ref34.SetActive(true);
					title34.text=oec.titles[i];
					date34.text=oec.dates[i];
					code34.text=oec.codes[i];
					break;
				case 34:
					ref35.SetActive(true);
					title35.text=oec.titles[i];
					date35.text=oec.dates[i];
					code35.text=oec.codes[i];
					break;
				case 35:
					ref36.SetActive(true);
					title36.text=oec.titles[i];
					date36.text=oec.dates[i];
					code36.text=oec.codes[i];
					break;
				case 36:
					ref37.SetActive(true);
					title37.text=oec.titles[i];
					date37.text=oec.dates[i];
					code37.text=oec.codes[i];
					break;
				case 37:
					ref38.SetActive(true);
					title38.text=oec.titles[i];
					date38.text=oec.dates[i];
					code38.text=oec.codes[i];
					break;
				case 38:
					ref39.SetActive(true);
					title39.text=oec.titles[i];
					date39.text=oec.dates[i];
					code39.text=oec.codes[i];
					break;
				case 39:
					ref40.SetActive(true);
					title40.text=oec.titles[i];
					date40.text=oec.dates[i];
					code40.text=oec.codes[i];
					break;
				case 40:
					ref41.SetActive(true);
					title41.text=oec.titles[i];
					date41.text=oec.dates[i];
					code41.text=oec.codes[i];
					break;
				case 41:
					ref42.SetActive(true);
					title42.text=oec.titles[i];
					date42.text=oec.dates[i];
					code42.text=oec.codes[i];
					break;
				case 42:
					ref43.SetActive(true);
					title43.text=oec.titles[i];
					date43.text=oec.dates[i];
					code43.text=oec.codes[i];
					break;
				case 43:
					ref44.SetActive(true);
					title44.text=oec.titles[i];
					date44.text=oec.dates[i];
					code44.text=oec.codes[i];
					break;
				case 44:
					ref45.SetActive(true);
					title45.text=oec.titles[i];
					date45.text=oec.dates[i];
					code45.text=oec.codes[i];
					break;
				case 45:
					ref46.SetActive(true);
					title46.text=oec.titles[i];
					date46.text=oec.dates[i];
					code46.text=oec.codes[i];
					break;
				case 46:
					ref47.SetActive(true);
					title47.text=oec.titles[i];
					date47.text=oec.dates[i];
					code47.text=oec.codes[i];
					break;
				case 47:
					ref48.SetActive(true);
					title48.text=oec.titles[i];
					date48.text=oec.dates[i];
					code48.text=oec.codes[i];
					break;
				case 48:
					ref49.SetActive(true);
					title49.text=oec.titles[i];
					date49.text=oec.dates[i];
					code49.text=oec.codes[i];
					break;
				case 49:
					ref50.SetActive(true);
					title50.text=oec.titles[i];
					date50.text=oec.dates[i];
					code50.text=oec.codes[i];
					break;
			}
		}
	}
	

	public void deactivateLibraryStubs()
	{
			for (int i = 0; i < oec.codes.Length; i++)
		{
			switch(i)
			{
				case 0:
					ref1.SetActive(false);
					break;
				case 1:
					ref2.SetActive(false);
					break;
				case 2:
					ref3.SetActive(false);
					break;
				case 3:
					ref4.SetActive(false);
					break;
				case 4:
					ref5.SetActive(false);
					break;
				case 5:
					ref6.SetActive(false);
					break;
				case 6:
					ref7.SetActive(false);
					break;
				case 7:
					ref8.SetActive(false);
					break;
				case 8:
					ref9.SetActive(false);
					break;
				case 9:
					ref10.SetActive(false);
					break;
				case 10:
					ref11.SetActive(false);
					break;
				case 11:
					ref12.SetActive(false);
					break;
				case 12:
					ref13.SetActive(false);
					break;
				case 13:
					ref14.SetActive(false);
					break;
				case 14:
					ref15.SetActive(false);
					break;
				case 15:
					ref16.SetActive(false);
					break;
				case 16:
					ref17.SetActive(false);
					break;
				case 17:
					ref18.SetActive(false);
					break;
				case 18:
					ref19.SetActive(false);
					break;
				case 19:
					ref20.SetActive(false);
					break;
				case 20:
					ref21.SetActive(false);
					break;
				case 21:
					ref22.SetActive(false);
					break;
				case 22:
					ref23.SetActive(false);
					break;
				case 23:
					ref24.SetActive(false);
					break;
				case 24:
					ref25.SetActive(false);
					break;
				case 25:
					ref26.SetActive(false);
					break;
				case 26:
					ref27.SetActive(false);
					break;
				case 27:
					ref28.SetActive(false);
					break;
				case 28:
					ref29.SetActive(false);
					break;
				case 29:
					ref30.SetActive(false);
					break;
				case 30:
					ref31.SetActive(false);
					break;
				case 31:
					ref32.SetActive(false);
					break;
				case 32:
					ref33.SetActive(false);
					break;
				case 33:
					ref34.SetActive(false);
					break;
				case 34:
					ref35.SetActive(false);
					break;
				case 35:
					ref36.SetActive(false);
					break;
				case 36:
					ref37.SetActive(false);
					break;
				case 37:
					ref38.SetActive(false);
					break;
				case 38:
					ref39.SetActive(false);
					break;
				case 39:
					ref40.SetActive(false);
					break;
				case 40:
					ref41.SetActive(false);
					break;
				case 41:
					ref42.SetActive(false);
					break;
				case 42:
					ref43.SetActive(false);
					break;
				case 43:
					ref44.SetActive(false);
					break;
				case 44:
					ref45.SetActive(false);
					break;
				case 45:
					ref46.SetActive(false);
					break;
				case 46:
					ref47.SetActive(false);
					break;
				case 47:
					ref48.SetActive(false);
					break;
				case 48:
					ref49.SetActive(false);
					break;
				case 49:
					ref50.SetActive(false);
					break;
			}
		}
	}
}
