using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour {
    public float maxBlur = .001f;
    public float blurInterval = .0005f;
    public bool blur = false;
    public Material material;

    public void onBlur()
    {
            blur = true;
    }

    public void offBlur()
    {
            blur = false;
    }
	
	// Update is called once per frame
	void Update () {
        float curBlur = material.GetFloat("_Blur");
        if(blur && curBlur < maxBlur)
        {
            material.SetFloat("_Blur",curBlur + blurInterval);
        }
        if(!blur && curBlur > 0)
        {
            material.SetFloat("_Blur", curBlur - blurInterval);
        }
	}
}
