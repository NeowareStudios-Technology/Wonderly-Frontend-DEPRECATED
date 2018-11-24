using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharing : MonoBehaviour
{

    // Use this for initialization
    public void SharingTest()
    {
        new NativeShare().SetText("http://google.com").Share();
    }
}