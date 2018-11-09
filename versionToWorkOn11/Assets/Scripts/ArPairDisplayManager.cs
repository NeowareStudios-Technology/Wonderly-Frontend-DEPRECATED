using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sample;
using System.IO;

public class ArPairDisplayManager : MonoBehaviour {

	public GameObject pair1;
	public GameObject pair2;
	public GameObject pair3;
	public GameObject pair4;
	public GameObject pair5;

	public Image targetThumb1;
	public Image targetThumb2;
	public Image targetThumb3;
	public Image targetThumb4;
	public Image targetThumb5;

	public Image targetObjectThumb1;
	public Image targetObjectThumb2;
	public Image targetObjectThumb3;
	public Image targetObjectThumb4;
	public Image targetObjectThumb5;

	public Image blankImage;

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

	public Image imgModel1;
	public Image imgModel2;
	public Image imgModel3;
	public Image imgModel4;
	public Image imgModel5;
	public Image imgModel6;
	public Image imgModel7;
	public Image imgModel8;
	public Image imgModel9;
	public Image imgModel10;
	public Image imgModel11;
	public Image imgModel12;
	public Image imgModel13;
	public Image imgModel14;
	public Image imgModel15;
	public Image imgModel16;
	public Image imgModel17;
	public Image imgModel18;

	public int[] targetThumbCheck = {0,0,0,0,0};

	public FilesManager fm;
	public VideoSearchDemo2 vsd;


	// IMAGE THUMBNAIL HANDLING FOR AR PAIRS HANDLED IN IMAGE SCRIPTS



	public void blankTargetObjectThumb()
	{
		switch(fm.currentTarget)
		{
			case 1:
				targetObjectThumb1.sprite = blankImage.sprite;
				break;
			case 2:
				targetObjectThumb2.sprite = blankImage.sprite;
				break;
			case 3:
				targetObjectThumb3.sprite = blankImage.sprite;
				break;
			case 4:
				targetObjectThumb4.sprite = blankImage.sprite;
				break;
			case 5:
				targetObjectThumb5.sprite = blankImage.sprite;
				break;
		}
	}


	public void setYoutubeThumbnailArPair(int index)
  {
		switch(fm.currentTarget)
					{
						case 1:
							switch(index)
							{
								case 0:
									targetObjectThumb1.sprite = img1.sprite;
									break;
								case 1:
									targetObjectThumb1.sprite = img2.sprite;
									break;
								case 2:
									targetObjectThumb1.sprite = img3.sprite;
									break;
								case 3:
									targetObjectThumb1.sprite = img4.sprite;
									break;
								case 4:
									targetObjectThumb1.sprite = img5.sprite;
									break;
								case 5:
									targetObjectThumb1.sprite = img6.sprite;
									break;
								case 6:
									targetObjectThumb1.sprite = img7.sprite;
									break;
								case 7:
									targetObjectThumb1.sprite = img8.sprite;
									break;
								case 8:
									targetObjectThumb1.sprite = img9.sprite;
									break;
								case 9:
									targetObjectThumb1.sprite = img10.sprite;
									break;
								case 10:
									targetObjectThumb1.sprite = img11.sprite;
									break;
								case 11:
									targetObjectThumb1.sprite = img12.sprite;
									break;
								case 12:
									targetObjectThumb1.sprite = img13.sprite;
									break;
								case 13:
									targetObjectThumb1.sprite = img14.sprite;
									break;
								case 14:
									targetObjectThumb1.sprite = img15.sprite;
									break;
								case 15:
									targetObjectThumb1.sprite = img16.sprite;
									break;
								case 16:
									targetObjectThumb1.sprite = img17.sprite;
									break;
								case 18:
									targetObjectThumb1.sprite = img18.sprite;
									break;
							}
							break;
						case 2:
							switch(index)
							{
								case 0:
									targetObjectThumb2.sprite = img1.sprite;
									break;
								case 1:
									targetObjectThumb2.sprite = img2.sprite;
									break;
								case 2:
									targetObjectThumb2.sprite = img3.sprite;
									break;
								case 3:
									targetObjectThumb2.sprite = img4.sprite;
									break;
								case 4:
									targetObjectThumb2.sprite = img5.sprite;
									break;
								case 5:
									targetObjectThumb2.sprite = img6.sprite;
									break;
								case 6:
									targetObjectThumb2.sprite = img7.sprite;
									break;
								case 7:
									targetObjectThumb2.sprite = img8.sprite;
									break;
								case 8:
									targetObjectThumb2.sprite = img9.sprite;
									break;
								case 9:
									targetObjectThumb2.sprite = img10.sprite;
									break;
								case 10:
									targetObjectThumb2.sprite = img11.sprite;
									break;
								case 11:
									targetObjectThumb2.sprite = img12.sprite;
									break;
								case 12:
									targetObjectThumb2.sprite = img13.sprite;
									break;
								case 13:
									targetObjectThumb2.sprite = img14.sprite;
									break;
								case 14:
									targetObjectThumb2.sprite = img15.sprite;
									break;
								case 15:
									targetObjectThumb2.sprite = img16.sprite;
									break;
								case 16:
									targetObjectThumb2.sprite = img17.sprite;
									break;
								case 18:
									targetObjectThumb2.sprite = img18.sprite;
									break;
							}
							break;
						case 3:
							switch(index)
							{
								case 0:
									targetObjectThumb3.sprite = img1.sprite;
									break;
								case 1:
									targetObjectThumb3.sprite = img2.sprite;
									break;
								case 2:
									targetObjectThumb3.sprite = img3.sprite;
									break;
								case 3:
									targetObjectThumb3.sprite = img4.sprite;
									break;
								case 4:
									targetObjectThumb3.sprite = img5.sprite;
									break;
								case 5:
									targetObjectThumb3.sprite = img6.sprite;
									break;
								case 6:
									targetObjectThumb3.sprite = img7.sprite;
									break;
								case 7:
									targetObjectThumb3.sprite = img8.sprite;
									break;
								case 8:
									targetObjectThumb3.sprite = img9.sprite;
									break;
								case 9:
									targetObjectThumb3.sprite = img10.sprite;
									break;
								case 10:
									targetObjectThumb3.sprite = img11.sprite;
									break;
								case 11:
									targetObjectThumb3.sprite = img12.sprite;
									break;
								case 12:
									targetObjectThumb3.sprite = img13.sprite;
									break;
								case 13:
									targetObjectThumb3.sprite = img14.sprite;
									break;
								case 14:
									targetObjectThumb3.sprite = img15.sprite;
									break;
								case 15:
									targetObjectThumb3.sprite = img16.sprite;
									break;
								case 16:
									targetObjectThumb3.sprite = img17.sprite;
									break;
								case 18:
									targetObjectThumb3.sprite = img18.sprite;
									break;
							}
							break;
						case 4:
							switch(index)
							{
								case 0:
									targetObjectThumb4.sprite = img1.sprite;
									break;
								case 1:
									targetObjectThumb4.sprite = img2.sprite;
									break;
								case 2:
									targetObjectThumb4.sprite = img3.sprite;
									break;
								case 3:
									targetObjectThumb4.sprite = img4.sprite;
									break;
								case 4:
									targetObjectThumb4.sprite = img5.sprite;
									break;
								case 5:
									targetObjectThumb4.sprite = img6.sprite;
									break;
								case 6:
									targetObjectThumb4.sprite = img7.sprite;
									break;
								case 7:
									targetObjectThumb4.sprite = img8.sprite;
									break;
								case 8:
									targetObjectThumb4.sprite = img9.sprite;
									break;
								case 9:
									targetObjectThumb4.sprite = img10.sprite;
									break;
								case 10:
									targetObjectThumb4.sprite = img11.sprite;
									break;
								case 11:
									targetObjectThumb4.sprite = img12.sprite;
									break;
								case 12:
									targetObjectThumb4.sprite = img13.sprite;
									break;
								case 13:
									targetObjectThumb4.sprite = img14.sprite;
									break;
								case 14:
									targetObjectThumb4.sprite = img15.sprite;
									break;
								case 15:
									targetObjectThumb4.sprite = img16.sprite;
									break;
								case 16:
									targetObjectThumb4.sprite = img17.sprite;
									break;
								case 18:
									targetObjectThumb4.sprite = img18.sprite;
									break;
							}
							break;
						case 5:
							switch(index)
							{
								case 0:
									targetObjectThumb5.sprite = img1.sprite;
									break;
								case 1:
									targetObjectThumb5.sprite = img2.sprite;
									break;
								case 2:
									targetObjectThumb5.sprite = img3.sprite;
									break;
								case 3:
									targetObjectThumb5.sprite = img4.sprite;
									break;
								case 4:
									targetObjectThumb5.sprite = img5.sprite;
									break;
								case 5:
									targetObjectThumb5.sprite = img6.sprite;
									break;
								case 6:
									targetObjectThumb5.sprite = img7.sprite;
									break;
								case 7:
									targetObjectThumb5.sprite = img8.sprite;
									break;
								case 8:
									targetObjectThumb5.sprite = img9.sprite;
									break;
								case 9:
									targetObjectThumb5.sprite = img10.sprite;
									break;
								case 10:
									targetObjectThumb5.sprite = img11.sprite;
									break;
								case 11:
									targetObjectThumb5.sprite = img12.sprite;
									break;
								case 12:
									targetObjectThumb5.sprite = img13.sprite;
									break;
								case 13:
									targetObjectThumb5.sprite = img14.sprite;
									break;
								case 14:
									targetObjectThumb5.sprite = img15.sprite;
									break;
								case 15:
									targetObjectThumb5.sprite = img16.sprite;
									break;
								case 16:
									targetObjectThumb5.sprite = img17.sprite;
									break;
								case 18:
									targetObjectThumb5.sprite = img18.sprite;
									break;
							}
							break;

				}
			
	}

		public void setModelThumbnailArPair(int index)
  {
		switch(fm.currentTarget)
					{
						case 1:
							switch(index)
							{
								case 0:
									targetObjectThumb1.sprite = imgModel1.sprite;
									break;
								case 1:
									targetObjectThumb1.sprite = imgModel2.sprite;
									break;
								case 2:
									targetObjectThumb1.sprite = imgModel3.sprite;
									break;
								case 3:
									targetObjectThumb1.sprite = imgModel4.sprite;
									break;
								case 4:
									targetObjectThumb1.sprite = imgModel5.sprite;
									break;
								case 5:
									targetObjectThumb1.sprite = imgModel6.sprite;
									break;
								case 6:
									targetObjectThumb1.sprite = imgModel7.sprite;
									break;
								case 7:
									targetObjectThumb1.sprite = imgModel8.sprite;
									break;
								case 8:
									targetObjectThumb1.sprite = imgModel9.sprite;
									break;
								case 9:
									targetObjectThumb1.sprite = imgModel10.sprite;
									break;
								case 10:
									targetObjectThumb1.sprite = imgModel11.sprite;
									break;
								case 11:
									targetObjectThumb1.sprite = imgModel12.sprite;
									break;
								case 12:
									targetObjectThumb1.sprite = imgModel13.sprite;
									break;
								case 13:
									targetObjectThumb1.sprite = imgModel14.sprite;
									break;
								case 14:
									targetObjectThumb1.sprite = imgModel15.sprite;
									break;
								case 15:
									targetObjectThumb1.sprite = imgModel16.sprite;
									break;
								case 16:
									targetObjectThumb1.sprite = imgModel17.sprite;
									break;
								case 18:
									targetObjectThumb1.sprite = imgModel18.sprite;
									break;
							}
							break;
						case 2:
							switch(index)
							{
								case 0:
									targetObjectThumb2.sprite = imgModel1.sprite;
									break;
								case 1:
									targetObjectThumb2.sprite = imgModel2.sprite;
									break;
								case 2:
									targetObjectThumb2.sprite = imgModel3.sprite;
									break;
								case 3:
									targetObjectThumb2.sprite = imgModel4.sprite;
									break;
								case 4:
									targetObjectThumb2.sprite = imgModel5.sprite;
									break;
								case 5:
									targetObjectThumb2.sprite = imgModel6.sprite;
									break;
								case 6:
									targetObjectThumb2.sprite = imgModel7.sprite;
									break;
								case 7:
									targetObjectThumb2.sprite = imgModel8.sprite;
									break;
								case 8:
									targetObjectThumb2.sprite = imgModel9.sprite;
									break;
								case 9:
									targetObjectThumb2.sprite = imgModel10.sprite;
									break;
								case 10:
									targetObjectThumb2.sprite = imgModel11.sprite;
									break;
								case 11:
									targetObjectThumb2.sprite = imgModel12.sprite;
									break;
								case 12:
									targetObjectThumb2.sprite = imgModel13.sprite;
									break;
								case 13:
									targetObjectThumb2.sprite = imgModel14.sprite;
									break;
								case 14:
									targetObjectThumb2.sprite = imgModel15.sprite;
									break;
								case 15:
									targetObjectThumb2.sprite = imgModel16.sprite;
									break;
								case 16:
									targetObjectThumb2.sprite = imgModel17.sprite;
									break;
								case 18:
									targetObjectThumb2.sprite = imgModel18.sprite;
									break;
							}
							break;
						case 3:
							switch(index)
							{
								case 0:
									targetObjectThumb3.sprite = imgModel1.sprite;
									break;
								case 1:
									targetObjectThumb3.sprite = imgModel2.sprite;
									break;
								case 2:
									targetObjectThumb3.sprite = imgModel3.sprite;
									break;
								case 3:
									targetObjectThumb3.sprite = imgModel4.sprite;
									break;
								case 4:
									targetObjectThumb3.sprite = imgModel5.sprite;
									break;
								case 5:
									targetObjectThumb3.sprite = imgModel6.sprite;
									break;
								case 6:
									targetObjectThumb3.sprite = imgModel7.sprite;
									break;
								case 7:
									targetObjectThumb3.sprite = imgModel8.sprite;
									break;
								case 8:
									targetObjectThumb3.sprite = imgModel9.sprite;
									break;
								case 9:
									targetObjectThumb3.sprite = imgModel10.sprite;
									break;
								case 10:
									targetObjectThumb3.sprite = imgModel11.sprite;
									break;
								case 11:
									targetObjectThumb3.sprite = imgModel12.sprite;
									break;
								case 12:
									targetObjectThumb3.sprite = imgModel13.sprite;
									break;
								case 13:
									targetObjectThumb3.sprite = imgModel14.sprite;
									break;
								case 14:
									targetObjectThumb3.sprite = imgModel15.sprite;
									break;
								case 15:
									targetObjectThumb3.sprite = imgModel16.sprite;
									break;
								case 16:
									targetObjectThumb3.sprite = imgModel17.sprite;
									break;
								case 18:
									targetObjectThumb3.sprite = imgModel18.sprite;
									break;
							}
							break;
						case 4:
							switch(index)
							{
								case 0:
									targetObjectThumb4.sprite = imgModel1.sprite;
									break;
								case 1:
									targetObjectThumb4.sprite = imgModel2.sprite;
									break;
								case 2:
									targetObjectThumb4.sprite = imgModel3.sprite;
									break;
								case 3:
									targetObjectThumb4.sprite = imgModel4.sprite;
									break;
								case 4:
									targetObjectThumb4.sprite = imgModel5.sprite;
									break;
								case 5:
									targetObjectThumb4.sprite = imgModel6.sprite;
									break;
								case 6:
									targetObjectThumb4.sprite = imgModel7.sprite;
									break;
								case 7:
									targetObjectThumb4.sprite = imgModel8.sprite;
									break;
								case 8:
									targetObjectThumb4.sprite = imgModel9.sprite;
									break;
								case 9:
									targetObjectThumb4.sprite = imgModel10.sprite;
									break;
								case 10:
									targetObjectThumb4.sprite = imgModel11.sprite;
									break;
								case 11:
									targetObjectThumb4.sprite = imgModel12.sprite;
									break;
								case 12:
									targetObjectThumb4.sprite = imgModel13.sprite;
									break;
								case 13:
									targetObjectThumb4.sprite = imgModel14.sprite;
									break;
								case 14:
									targetObjectThumb4.sprite = imgModel15.sprite;
									break;
								case 15:
									targetObjectThumb4.sprite = imgModel16.sprite;
									break;
								case 16:
									targetObjectThumb4.sprite = imgModel17.sprite;
									break;
								case 18:
									targetObjectThumb4.sprite = imgModel18.sprite;
									break; 
							} 
							break; 
						case 5: 
							switch(index) 
							{ 
								case 0: 
									targetObjectThumb5.sprite = imgModel1.sprite;
									break;
								case 1:
									targetObjectThumb5.sprite = imgModel2.sprite;
									break;
								case 2:
									targetObjectThumb5.sprite = imgModel3.sprite;
									break;
								case 3:
									targetObjectThumb5.sprite = imgModel4.sprite;
									break;
								case 4:
									targetObjectThumb5.sprite = imgModel5.sprite;
									break;
								case 5:
									targetObjectThumb5.sprite = imgModel6.sprite;
									break;
								case 6:
									targetObjectThumb5.sprite = imgModel7.sprite;
									break;
								case 7:
									targetObjectThumb5.sprite = imgModel8.sprite;
									break;
								case 8:
									targetObjectThumb5.sprite = imgModel9.sprite;
									break;
								case 9:
									targetObjectThumb5.sprite = imgModel10.sprite;
									break;
								case 10:
									targetObjectThumb5.sprite = imgModel11.sprite;
									break;
								case 11:
									targetObjectThumb5.sprite = imgModel12.sprite;
									break;
								case 12:
									targetObjectThumb5.sprite = imgModel13.sprite;
									break;
								case 13:
									targetObjectThumb5.sprite = imgModel14.sprite;
									break;
								case 14:
									targetObjectThumb5.sprite = imgModel15.sprite;
									break;
								case 15:
									targetObjectThumb5.sprite = imgModel16.sprite;
									break;
								case 16:
									targetObjectThumb5.sprite = imgModel17.sprite;
									break;
								case 18:
									targetObjectThumb5.sprite = imgModel18.sprite;
									break;
							}
							break;

				}
			
	}
	


	void NextPair() 
	{
		
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//go through each possible ar target pair and set up with proper target photo and manage all target object photos
		for (int i=0; i<5; i++)
		{
			switch(i)
			{
				case 0:
					string thisPath1 = Path.Combine(fm.MarksDirectory, "targetPhoto1.jpg");
					//if the target thumb has not yet been set and the target photo exists, set the target thumb
					if (targetThumbCheck[0] == 0 && File.Exists(thisPath1))
					{
						targetThumb1.sprite = IMG2Sprite.LoadNewSprite(thisPath1);
						targetThumbCheck[0] = 1;
					}
					//else if the target thumb has been set, but the target photo has been deleted, blank the target thumb
					else if(targetThumbCheck[0] == 1 && !File.Exists(thisPath1))
					{
						targetThumb1.sprite = blankImage.sprite;
						targetObjectThumb1.sprite = blankImage.sprite;
						targetThumbCheck[0] = 0;
					}
					break;

				case 1:
					string thisPath2 = Path.Combine(fm.MarksDirectory, "targetPhoto2.jpg");
					if (targetThumbCheck[1] == 0 && File.Exists(thisPath2))
					{
						targetThumb2.sprite = IMG2Sprite.LoadNewSprite(thisPath2);
						targetThumbCheck[1] = 1;
					}
					else if(targetThumbCheck[1] == 1 && !File.Exists(thisPath2))
					{
						targetThumb2.sprite = blankImage.sprite;
						targetObjectThumb2.sprite = blankImage.sprite;
						targetThumbCheck[1] = 0;
					}
					break;
				case 2:
					string thisPath3 = Path.Combine(fm.MarksDirectory, "targetPhoto3.jpg");
					if (targetThumbCheck[2] == 0 && File.Exists(thisPath3))
					{
						targetThumb3.sprite = IMG2Sprite.LoadNewSprite(thisPath3);
						targetThumbCheck[2] = 1;
					}
					else if(targetThumbCheck[2] == 1 && !File.Exists(thisPath3))
					{
						targetThumb3.sprite = blankImage.sprite;
						targetObjectThumb3.sprite = blankImage.sprite;
						targetThumbCheck[2] = 0;
					}
					break;
				case 3:
					string thisPath4 = Path.Combine(fm.MarksDirectory, "targetPhoto4.jpg");
					if (targetThumbCheck[3] == 0 && File.Exists(thisPath4))
					{
						targetThumb4.sprite = IMG2Sprite.LoadNewSprite(thisPath4);
						targetThumbCheck[3] = 1;
					}
					else if(targetThumbCheck[3] == 1 && !File.Exists(thisPath4))
					{
						targetThumb4.sprite = blankImage.sprite;
						targetObjectThumb4.sprite = blankImage.sprite;
						targetThumbCheck[3] = 0;
					}
					break;
				case 4:
					string thisPath5 = Path.Combine(fm.MarksDirectory, "targetPhoto5.jpg");
					if (targetThumbCheck[4] == 0 && File.Exists(thisPath5))
					{
						targetThumb5.sprite = IMG2Sprite.LoadNewSprite(thisPath5);
						targetThumbCheck[4] = 1;
					}
					else if(targetThumbCheck[4] == 1 && !File.Exists(thisPath5))
					{
						targetThumb5.sprite = blankImage.sprite;
						targetObjectThumb5.sprite = blankImage.sprite;
						targetThumbCheck[4] = 0;
					}
					break;
			}
		}

	}
}
