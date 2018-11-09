using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sample;

public class targetIndexModifier : MonoBehaviour {
	public FilesManager fm;
	public Text targetIndexText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		targetIndexText.text = fm.currentTarget.ToString();
	}
}
