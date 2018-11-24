using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveClassDeclaration {
	public int targetNum;
	public string[] targetStatus = new string[5];
	public string title = "";
	public string description = "";
	public string browserLink = "";
	public string[] vId = new string[5];
	public string[] imageUrl = new string[5];
	public string mod1 = "";
	public string mod2 = "";
	public string mod3 = "";
	public string mod4 = "";
	public string mod5 = "";
	public float[] rot1 = new float[3];
	public float[] rot2 = new float[3];
	public float[] rot3 = new float[3];
	public float[] rot4 = new float[3];
	public float[] rot5 = new float[3];

	public static SaveClassDeclaration CreateFromJSON(string jsonString)
  {
    return JsonUtility.FromJson<SaveClassDeclaration>(jsonString);
  }

}
