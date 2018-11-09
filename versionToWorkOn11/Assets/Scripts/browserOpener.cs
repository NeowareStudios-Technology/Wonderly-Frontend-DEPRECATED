using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class browserOpener : MonoBehaviour {
	public LoadManager lm;

	public void openWebLink()
	{
		Application.OpenURL(lm.scd.browserLink);
		Debug.Log("opening in browser: "+lm.scd.browserLink);
	}
}
