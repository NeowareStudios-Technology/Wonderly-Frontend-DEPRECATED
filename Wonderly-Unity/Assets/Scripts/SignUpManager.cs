using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpManager : MonoBehaviour {
	public int signUpIndex;
	public GameObject signUp1;
	public GameObject signUp2;
	public GameObject signUp3;
	public GameObject signUp4;
	public GameObject signUp5;

	public GameObject badEmailNotification;
	public GameObject badPasswordNotification;
	public GameObject badPasswordNotification2;
	public GameObject emptyfirstNameNotification;
	public GameObject emptylastNameNotification;

	public Animator accountCreationNotification;

	public Text email;
	public Text firstName;
	public Text lastName;
	public Text password;
	public Text password2;

	public GameObject lastButton;
	public GameObject secondLastButton;

	public GameObject wholeSignUp;

	// Use this for initialization
	void Start () {
		signUpIndex=0;
	}

	public void prevSignUpPanel()
	{
		switch(signUpIndex)
		{
			case 0:
				wholeSignUp.SetActive(false);
				break;
			case 1:
				signUp1.SetActive(true);
				signUp2.SetActive(false);
				signUpIndex--;
				break;
			case 2:
				signUp2.SetActive(true);
				signUp3.SetActive(false);
				signUpIndex--;
				break;
			case 3:
				signUp3.SetActive(true);
				signUp4.SetActive(false);
				signUpIndex--;
				break;
			case 4:
				signUp4.SetActive(true);
				signUp5.SetActive(false);
				secondLastButton.SetActive(true);
				lastButton.SetActive(false);
				signUpIndex--;
				break;
		}
	}

	public void nextSignUpIndex()
	{
		switch(signUpIndex)
		{
			//for email input validation
			case 0:
				if (email.text.Contains("@") && email.text.Contains(".com"))
				{
					signUp1.SetActive(false);
					signUp2.SetActive(true);
					signUpIndex++;
				}
				else
				{
						Debug.Log("invalid email");
						badEmailNotification.SetActive(true);
				}
				break;
			//for first name input validation
			case 1:
				if (firstName.text != "")
				{
					signUp2.SetActive(false);
					signUp3.SetActive(true);
					signUpIndex++;
				}
				else
				{
					emptyfirstNameNotification.SetActive(true);
					Debug.Log("blank input");
				}
				break;
			//for last name input validation
			case 2:
				if (lastName.text != "")
				{
					signUp2.SetActive(false);
					signUp3.SetActive(false);
					signUp4.SetActive(true);
					signUpIndex++;
				}
				else
				{
					emptylastNameNotification.SetActive(true);
					Debug.Log("blank input");
				}
				break;
			//for password input validation
			case 3:
				if (password.text.Length >= 6 && password.text.Length <= 20)
				{
					signUp2.SetActive(false);
					signUp4.SetActive(false);
					signUp5.SetActive(true);
					secondLastButton.SetActive(false);
					lastButton.SetActive(true);
					signUpIndex++;
				}
				else
				{
					badPasswordNotification.SetActive(true);
				}
				break;
		}
	}	

	//for matching passwords input validation
	public void ensureMatchingPasswords()
	{
		if (password.text == password2.text)
		{
			accountCreationNotification.SetTrigger("Notification");
		}
		else
		{
			Debug.Log("password does not match");
			badPasswordNotification2.SetActive(true);
		}
	}

	public void resetIndex()
	{
		signUpIndex =0;
		signUp1.SetActive(true);
	}
}


