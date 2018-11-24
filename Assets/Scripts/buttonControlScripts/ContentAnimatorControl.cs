using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAnimatorControl : MonoBehaviour {
	public Animator contentAnimator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startSearchAnimation()
	{
		//returning animator info by tag hash is the only thing I can get working

		//** tag legend**
		//searchHidden = 838648830
		//searchInputPopup = 1817850389
		//searchMade = -1263097638

		//if the tag of the current animator is "searchHidden"
		if (contentAnimator.GetCurrentAnimatorStateInfo(0).tagHash == 838648830)
		{
			contentAnimator.SetTrigger("search");
		}
		else
		{
				Debug.Log("repeat button press");
		}
	}
}
