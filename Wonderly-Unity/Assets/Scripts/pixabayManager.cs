using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Sample;

public class pixabayManager : MonoBehaviour {
	public string searchUrl = "https://pixabay.com/api/?key=10416046-da227ed77f5d1960a9126dc7c&";
	public Text searchTerm;
	public Image img1;
	public Image img2;
	public Image img3;
	public Image img4;
	public Image img5;
	public Image img6;
	public Image img7;
	public Image img8;
	public Image img9;
	public Image img10;
	public Image img11;
	public Image img12;
	public Image img13;
	public Image img14;
	public Image img15;
	public Image img16;
	public Image img17;
	public Image img18;
	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;
	public string[] chosenUrls = new string[5];
  public string[] imagePreviewUrl = new string[18];
	public string[] imageUrl = new string[18];
	public pixabayClass pxc;
	public FilesManager fm;
	public ArPairDisplayManager apdm;

	//starts coroutine because coroutine cannot be called by UI
	public void startSearch() {
		StartCoroutine("searchPic");
	}

	//makes web call for searching for image in pixabay repo
	public IEnumerator searchPic() {
		//holds the search term
		string searchString = searchTerm.text;
		Debug.Log(searchString);
		//makes search term url safe
		string urlSafeSearchTerm = searchString.Replace(" ", "+");
		//add delineator to search term for url
		string finalizedSearchTerm = "q=" + urlSafeSearchTerm;
		Debug.Log(finalizedSearchTerm);
		//create full search url
		string thisSearchUrl = searchUrl + finalizedSearchTerm;

		Debug.Log(thisSearchUrl);

		//create web request
		using (UnityWebRequest imageSearchRequest = UnityWebRequest.Get(thisSearchUrl))
		{
			//set content type
			imageSearchRequest.SetRequestHeader("Content-Type", "application/json");
			
			yield return imageSearchRequest.SendWebRequest();

			//catch errors
			if (imageSearchRequest.isNetworkError || imageSearchRequest.isHttpError)
    	{
			Debug.Log("Error getting image");
			}

			//show previews of each image
			else 
			{
				Debug.Log(imageSearchRequest.responseCode);
				byte[] results = imageSearchRequest.downloadHandler.data;
        string jsonString = Encoding.UTF8.GetString(results);
				Debug.Log(jsonString);
				pxc = JsonUtility.FromJson<pixabayClass>(jsonString);

				//get the url for each image returned in the image search request
				int count = 0;
				foreach (pixabayHitClass phc in pxc.hits)
				{
					if (count == 17)
					{
						break;
					}
					imagePreviewUrl[count] = phc.previewURL;
					imageUrl[count] = phc.largeImageURL;
					count ++;
				}

				//load the image previews to their UI
				for (int j = 0; j < count; j++)
				{
					Debug.Log("download started");
					StartCoroutine(loadPreviewImage(j));
				}
			}
		}
	}

	private IEnumerator loadPreviewImage(int index) {
		using (WWW previewRequest = new WWW(imagePreviewUrl[index]))
		{
			yield return previewRequest;
			//catch errors
			if (previewRequest.error != null)
    	{
			Debug.Log("Error getting image");
			}
			else{
				switch(index)
				{
					case 0:
						img1.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 1:
						img2.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 2:
						img3.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 3:
						img4.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 4:
						img5.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 5:
						img6.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 6:
						img7.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 7:
						img8.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 8:
						img9.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 9:
						img10.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 10:
						img11.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 11:
						img12.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 12:
						img13.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 13:
						img14.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 14:
						img15.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 15:
						img16.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 16:
						img17.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
					case 18:
						img18.sprite = Sprite.Create(previewRequest.texture, new Rect(0, 0, previewRequest.texture.width, previewRequest.texture.height), new Vector2(0, 0));
						break;
				}
			}
		}
	}

		public void chooseImageStarter(int index) {
			switch(fm.currentTarget)
			{
				case 0:
					break;
				case 1:
					chosenUrls[0] = imageUrl[index];
					break;
				case 2:
					chosenUrls[1] = imageUrl[index];
					break;
				case 3:
					chosenUrls[2] = imageUrl[index];
					break;
				case 4:
					chosenUrls[3] = imageUrl[index];
					break;
				case 5:
					chosenUrls[4] = imageUrl[index];
					break;
			}
			StartCoroutine(ChooseImage(index));
		}

		public IEnumerator SetArPairThumbnail(int index)
		{
			switch(fm.currentTarget)
					{
						case 1:
							switch(index)
							{
								case 0:
									apdm.targetObjectThumb1.sprite = img1.sprite;
									break;
								case 1:
									apdm.targetObjectThumb1.sprite = img2.sprite;
									break;
								case 2:
									apdm.targetObjectThumb1.sprite = img3.sprite;
									break;
								case 3:
									apdm.targetObjectThumb1.sprite = img4.sprite;
									break;
								case 4:
									apdm.targetObjectThumb1.sprite = img5.sprite;
									break;
								case 5:
									apdm.targetObjectThumb1.sprite = img6.sprite;
									break;
								case 6:
									apdm.targetObjectThumb1.sprite = img7.sprite;
									break;
								case 7:
									apdm.targetObjectThumb1.sprite = img8.sprite;
									break;
								case 8:
									apdm.targetObjectThumb1.sprite = img9.sprite;
									break;
								case 9:
									apdm.targetObjectThumb1.sprite = img10.sprite;
									break;
								case 10:
									apdm.targetObjectThumb1.sprite = img11.sprite;
									break;
								case 11:
									apdm.targetObjectThumb1.sprite = img12.sprite;
									break;
								case 12:
									apdm.targetObjectThumb1.sprite = img13.sprite;
									break;
								case 13:
									apdm.targetObjectThumb1.sprite = img14.sprite;
									break;
								case 14:
									apdm.targetObjectThumb1.sprite = img15.sprite;
									break;
								case 15:
									apdm.targetObjectThumb1.sprite = img16.sprite;
									break;
								case 16:
									apdm.targetObjectThumb1.sprite = img17.sprite;
									break;
								case 18:
									apdm.targetObjectThumb1.sprite = img18.sprite;
									break;
							}
							break;
						case 2:
							switch(index)
							{
								case 0:
									apdm.targetObjectThumb2.sprite = img1.sprite;
									break;
								case 1:
									apdm.targetObjectThumb2.sprite = img2.sprite;
									break;
								case 2:
									apdm.targetObjectThumb2.sprite = img3.sprite;
									break;
								case 3:
									apdm.targetObjectThumb2.sprite = img4.sprite;
									break;
								case 4:
									apdm.targetObjectThumb2.sprite = img5.sprite;
									break;
								case 5:
									apdm.targetObjectThumb2.sprite = img6.sprite;
									break;
								case 6:
									apdm.targetObjectThumb2.sprite = img7.sprite;
									break;
								case 7:
									apdm.targetObjectThumb2.sprite = img8.sprite;
									break;
								case 8:
									apdm.targetObjectThumb2.sprite = img9.sprite;
									break;
								case 9:
									apdm.targetObjectThumb2.sprite = img10.sprite;
									break;
								case 10:
									apdm.targetObjectThumb2.sprite = img11.sprite;
									break;
								case 11:
									apdm.targetObjectThumb2.sprite = img12.sprite;
									break;
								case 12:
									apdm.targetObjectThumb2.sprite = img13.sprite;
									break;
								case 13:
									apdm.targetObjectThumb2.sprite = img14.sprite;
									break;
								case 14:
									apdm.targetObjectThumb2.sprite = img15.sprite;
									break;
								case 15:
									apdm.targetObjectThumb2.sprite = img16.sprite;
									break;
								case 16:
									apdm.targetObjectThumb2.sprite = img17.sprite;
									break;
								case 18:
									apdm.targetObjectThumb2.sprite = img18.sprite;
									break;
							}
							break;
						case 3:
							switch(index)
							{
								case 0:
									apdm.targetObjectThumb3.sprite = img1.sprite;
									break;
								case 1:
									apdm.targetObjectThumb3.sprite = img2.sprite;
									break;
								case 2:
									apdm.targetObjectThumb3.sprite = img3.sprite;
									break;
								case 3:
									apdm.targetObjectThumb3.sprite = img4.sprite;
									break;
								case 4:
									apdm.targetObjectThumb3.sprite = img5.sprite;
									break;
								case 5:
									apdm.targetObjectThumb3.sprite = img6.sprite;
									break;
								case 6:
									apdm.targetObjectThumb3.sprite = img7.sprite;
									break;
								case 7:
									apdm.targetObjectThumb3.sprite = img8.sprite;
									break;
								case 8:
									apdm.targetObjectThumb3.sprite = img9.sprite;
									break;
								case 9:
									apdm.targetObjectThumb3.sprite = img10.sprite;
									break;
								case 10:
									apdm.targetObjectThumb3.sprite = img11.sprite;
									break;
								case 11:
									apdm.targetObjectThumb3.sprite = img12.sprite;
									break;
								case 12:
									apdm.targetObjectThumb3.sprite = img13.sprite;
									break;
								case 13:
									apdm.targetObjectThumb3.sprite = img14.sprite;
									break;
								case 14:
									apdm.targetObjectThumb3.sprite = img15.sprite;
									break;
								case 15:
									apdm.targetObjectThumb3.sprite = img16.sprite;
									break;
								case 16:
									apdm.targetObjectThumb3.sprite = img17.sprite;
									break;
								case 18:
									apdm.targetObjectThumb3.sprite = img18.sprite;
									break;
							}
							break;
						case 4:
							switch(index)
							{
								case 0:
									apdm.targetObjectThumb4.sprite = img1.sprite;
									break;
								case 1:
									apdm.targetObjectThumb4.sprite = img2.sprite;
									break;
								case 2:
									apdm.targetObjectThumb4.sprite = img3.sprite;
									break;
								case 3:
									apdm.targetObjectThumb4.sprite = img4.sprite;
									break;
								case 4:
									apdm.targetObjectThumb4.sprite = img5.sprite;
									break;
								case 5:
									apdm.targetObjectThumb4.sprite = img6.sprite;
									break;
								case 6:
									apdm.targetObjectThumb4.sprite = img7.sprite;
									break;
								case 7:
									apdm.targetObjectThumb4.sprite = img8.sprite;
									break;
								case 8:
									apdm.targetObjectThumb4.sprite = img9.sprite;
									break;
								case 9:
									apdm.targetObjectThumb4.sprite = img10.sprite;
									break;
								case 10:
									apdm.targetObjectThumb4.sprite = img11.sprite;
									break;
								case 11:
									apdm.targetObjectThumb4.sprite = img12.sprite;
									break;
								case 12:
									apdm.targetObjectThumb4.sprite = img13.sprite;
									break;
								case 13:
									apdm.targetObjectThumb4.sprite = img14.sprite;
									break;
								case 14:
									apdm.targetObjectThumb4.sprite = img15.sprite;
									break;
								case 15:
									apdm.targetObjectThumb4.sprite = img16.sprite;
									break;
								case 16:
									apdm.targetObjectThumb4.sprite = img17.sprite;
									break;
								case 18:
									apdm.targetObjectThumb4.sprite = img18.sprite;
									break;
							}
							break;
						case 5:
							switch(index)
							{
								case 0:
									apdm.targetObjectThumb5.sprite = img1.sprite;
									break;
								case 1:
									apdm.targetObjectThumb5.sprite = img2.sprite;
									break;
								case 2:
									apdm.targetObjectThumb5.sprite = img3.sprite;
									break;
								case 3:
									apdm.targetObjectThumb5.sprite = img4.sprite;
									break;
								case 4:
									apdm.targetObjectThumb5.sprite = img5.sprite;
									break;
								case 5:
									apdm.targetObjectThumb5.sprite = img6.sprite;
									break;
								case 6:
									apdm.targetObjectThumb5.sprite = img7.sprite;
									break;
								case 7:
									apdm.targetObjectThumb5.sprite = img8.sprite;
									break;
								case 8:
									apdm.targetObjectThumb5.sprite = img9.sprite;
									break;
								case 9:
									apdm.targetObjectThumb5.sprite = img10.sprite;
									break;
								case 10:
									apdm.targetObjectThumb5.sprite = img11.sprite;
									break;
								case 11:
									apdm.targetObjectThumb5.sprite = img12.sprite;
									break;
								case 12:
									apdm.targetObjectThumb5.sprite = img13.sprite;
									break;
								case 13:
									apdm.targetObjectThumb5.sprite = img14.sprite;
									break;
								case 14:
									apdm.targetObjectThumb5.sprite = img15.sprite;
									break;
								case 15:
									apdm.targetObjectThumb5.sprite = img16.sprite;
									break;
								case 16:
									apdm.targetObjectThumb5.sprite = img17.sprite;
									break;
								case 18:
									apdm.targetObjectThumb5.sprite = img18.sprite;
									break;
							}
							break;

				}

				yield return null;
		}

		public IEnumerator ChooseImage(int index) {
			using (WWW imageRequest = new WWW(imageUrl[index]))
			{
				yield return imageRequest;
				Debug.Log("request worked");
				//catch errors
				if (imageRequest.error != null)
				{
					Debug.Log("Error getting image:" + imageRequest.error);
				}
				else
				{
					switch(fm.currentTarget)
					{
						case 1:
							image1.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
							StartCoroutine(SetArPairThumbnail(index));
							fm.targetStatus[0] = "image";
							break;
						case 2:
							image2.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
							StartCoroutine(SetArPairThumbnail(index));
							fm.targetStatus[1] = "image";
							break;
						case 3:
							image3.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
							StartCoroutine(SetArPairThumbnail(index));
							fm.targetStatus[2] = "image";
							break;
						case 4:
							image4.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
							StartCoroutine(SetArPairThumbnail(index));
							fm.targetStatus[3] = "image";
							break;
						case 5:
							image5.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
							StartCoroutine(SetArPairThumbnail(index));
							fm.targetStatus[4] = "image";
							break;
					}
				}
			}
		}
}
