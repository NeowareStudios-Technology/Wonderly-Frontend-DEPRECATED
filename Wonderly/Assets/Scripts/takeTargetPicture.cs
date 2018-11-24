using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;
using Sample;

public class takeTargetPicture : MonoBehaviour {
	public FilesManager fm;
	public ImageTargetManager itm;

	public GUISkin skin;
	public void takePicture() 
	{
		fm.StartTakePhoto();
	}

	public void ClearTargets() 
	{
		fm.ClearTexture();
    itm.ClearAllTarget();
	}

}
