using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sample;

public class UiManager : MonoBehaviour {
	public FilesManager fm;
	public FirebaseManager fbm;
	public SaveManager sm;

	public int currentTargetNum;

	public Image videoHighlight;
	public Image modelHighlight;
	public Image imageHighlight;
	public Image textHighlight;


	public Text ArLabel1;
	public Text ArLabel2;
	public Text ArLabel3;
	public Text ArLabel4;
	public Text ArLabel5;

	public Text description;

	public InputField targetSwitchTitle;

	public InputField summaryTitle;


	// Use this for initialization
	void Start () {
		videoHighlight.gameObject.SetActive(false);
		modelHighlight.gameObject.SetActive(false);
		imageHighlight.gameObject.SetActive(false);
		textHighlight.gameObject.SetActive(false);

		summaryTitle.onValueChange.AddListener(delegate {OnSummaryTitleChange(); });
		targetSwitchTitle.onValueChange.AddListener(delegate {OnSwitchTitleChange(); });
	}


	public void OnSummaryTitleChange()
	{
			targetSwitchTitle.text = summaryTitle.text;
	}

	public void OnSwitchTitleChange()
	{
			summaryTitle.text = targetSwitchTitle.text;
	}
	
	// handles what UI will be displayed based on the 1 model/video/pic per target rule
	void Update () {

		if (fm.currentTarget == 0)
		{
			videoHighlight.gameObject.SetActive(false);
			modelHighlight.gameObject.SetActive(false);
			imageHighlight.gameObject.SetActive(false);
			textHighlight.gameObject.SetActive(false);
		}



		if(sm.title.text != "")
			summaryTitle.text = sm.title.text;

	}
}

