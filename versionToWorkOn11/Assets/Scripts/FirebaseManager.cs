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
using UnityEngine.SceneManagement;


public class FirebaseManager : MonoBehaviour {
	public CloudEndpointsApiManager ceam;
	public InputField email;
  public InputField Password;
	public Text newEmail;
	public Text newPassword;
	public string token;
	protected Firebase.Auth.FirebaseAuth auth;
	public FirebaseApp fbApp;
	public Firebase.Storage.FirebaseStorage fbStorage;
	public Firebase.Storage.StorageReference fbStorageRef;

	public GameObject profileIcon1;
	public GameObject profileIcon2;
	public GameObject createIcon1;
	public GameObject createIcon2;
	public GameObject libraryIcon1;
	public GameObject libraryIcon2;

	public GameObject wrongLoginNotification;

	public bool isLoggedIn;


	public void createNewFirebaseUser()
	{
		
		//make sure password is at least 6 chars long
		if (newPassword.text.Length < 6)
		{
			Debug.Log("password must be at least 6 characters long");
			return;
		}

		//create new firebase user
		auth.CreateUserWithEmailAndPasswordAsync(newEmail.text, newPassword.text).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);

			//login
			StartCoroutine("InternalLoginProcess");
		});

	}

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }

	//only used for signing in directly after creating a new profile
	private IEnumerator InternalLoginProcess()
	{
			//login credentials
			string e = newEmail.text;
			string p = newPassword.text;
			//this is used for automatically logging you in 
			/*if (PlayerPrefs.GetString("email", "email") != "email" && PlayerPrefs.GetString("password", "password") != "password")
			{
					e = PlayerPrefs.GetString("email", "email");
					p = PlayerPrefs.GetString("password", "password");
			}
			else
			{
					//sets your email and password for later
					PlayerPrefs.SetString("email", email.text);
					PlayerPrefs.SetString("password", Password.text);

			}*/
			//firebase signin
			FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
			auth.SignInWithEmailAndPasswordAsync(e, p).ContinueWith(task =>
			{
					if (task.IsCanceled)
					{
							Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
							return;
					}
					if (task.IsFaulted)
					{
							Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
							return;
					}

					Firebase.Auth.FirebaseUser newUser = task.Result;
					Debug.LogFormat("User signed in successfully: {0} ({1})",
							newUser.DisplayName, newUser.UserId);
					GetToken(auth);

					Debug.Log("Logging in: " + e + " " + p);
			});
			yield return new WaitForSeconds(1);

			ceam.startProfileCreate();

	}

	public void StartLoginProcess()
	{
			//login credentials
			string e = email.text;
			string p = Password.text;
			//this is used for automatically logging you in 
			/*if (PlayerPrefs.GetString("email", "email") != "email" && PlayerPrefs.GetString("password", "password") != "password")
			{
					e = PlayerPrefs.GetString("email", "email");
					p = PlayerPrefs.GetString("password", "password");
			}
			else
			{
					//sets your email and password for later
					PlayerPrefs.SetString("email", email.text);
					PlayerPrefs.SetString("password", Password.text);

			}*/
			//firebase signin
			FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
			auth.SignInWithEmailAndPasswordAsync(e, p).ContinueWith(task =>
			{
					if (task.IsCanceled)
					{
							Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
							return;
					}
					if (task.IsFaulted)
					{
							Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
							Debug.Log("Password or email incorrect");
							wrongLoginNotification.SetActive(true);
							return;
					}

					Firebase.Auth.FirebaseUser newUser = task.Result;
					Debug.LogFormat("User signed in successfully: {0} ({1})",
							newUser.DisplayName, newUser.UserId);
					GetToken(auth);

					Debug.Log("Logging in: " + e + " " + p);
			});
	}


	public void GetToken(FirebaseAuth auth)
	{
			FirebaseUser user = auth.CurrentUser;

			user.TokenAsync(true).ContinueWith(task =>
			{
					if (task.IsCanceled)
					{
							Debug.LogError("TokenAsync was canceled.");
							return;
					}

					if (task.IsFaulted)
					{
							Debug.LogError("TokenAsync encountered an error: " + task.Exception);
							Debug.Log("Password or email incorrect");
							wrongLoginNotification.SetActive(true);
							return;
					}

					token = task.Result;
					Debug.Log(token);
					isLoggedIn = true;

					profileIcon1.SetActive(false);
					profileIcon2.SetActive(true);
					libraryIcon1.SetActive(false);
					libraryIcon2.SetActive(true);
					createIcon1.SetActive(false);
					createIcon2.SetActive(true);
		});
	}

	// Use this for initialization
	void Start () 
	{
		isLoggedIn= false;
        //firebase init
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available)
			{
					Debug.Log("Firebase OK!");
			}
			else
			{
					UnityEngine.Debug.LogError(System.String.Format(
						"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
					// Firebase Unity SDK is not safe to use here.
			}
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://aliceone-221018.firebaseio.com");

				//set class variable to auth instance
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;       
				//set class variable to firebase app instance
				fbApp = FirebaseApp.DefaultInstance;
				//set class variable to firebase storage instance
			  fbStorage = Firebase.Storage.FirebaseStorage.DefaultInstance;
				fbStorageRef = fbStorage.GetReferenceFromUrl("gs://aliceone-221018.appspot.com");

		});
	}

	public void signOutFirebase()
	{
		auth.SignOut();
		isLoggedIn = false;
	}
}
