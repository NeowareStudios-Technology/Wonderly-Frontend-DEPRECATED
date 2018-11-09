using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using EasyAR;
using System.Collections;
using System.IO;
using Sample;

public class ImageTargetSetter : MonoBehaviour {
	public FilesManager pathManager;
	public Dictionary<string, DynamicImageTagetBehaviour> imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();
	public int count = 0;
	public ImageTargetManager itm;

	// Use this for initialization
	void Start () {
		
	}
	
	 void Update()
        {
            var imageTargetName_FileDic = pathManager.GetDirectoryName_FileDic();
            int[] fileCheck = {0,0,0,0,0};
            
            foreach (var obj in imageTargetName_FileDic.Where(obj => !imageTargetDic.ContainsKey(obj.Key)))
            {
                if(count < pathManager.TARGET_LIMIT)
                {
                    //creates new target game object
                    //GameObject imageTarget = new GameObject(obj.Key);

                    //2.0 
                    //determine which tag to use
                    switch (obj.Key)
                    {
                        case "targetPhoto1":
                            //creates new target game object
                            //GameObject itm.target1 = new GameObject(obj.Key);
                            //target1 = itm.target1;
                          
                           
                                itm.target1.SetActive(true);
                                itm.target1.tag = "target1";
                                var behaviour1 = itm.target1.AddComponent<DynamicImageTagetBehaviour>();
                                behaviour1.whichTargetAmI = 1;
                                behaviour1.Name = obj.Key;
                                behaviour1.Path = obj.Value.Replace(@"\", "/");
                                behaviour1.Storage = StorageType.Absolute;
                                //binds tracking behaviour to target behavior script (required)
                                behaviour1.Bind(itm.tracker1);
                                //keeps track of name of target and behavior
                                imageTargetDic.Add(obj.Key, behaviour1);
                                //set the target status array to reflect that this target has been created
                                if (pathManager.targetStatus[0] == "none")
                                    pathManager.targetStatus[0] = "created";
                                fileCheck[0] = 1;
                                count++;
                        
                            break;
                        case "targetPhoto2":
                            //GameObject imageTarget2= new GameObject(obj.Key);
                            //target2 = imageTarget2;
                       
                            
                                itm.target2.SetActive(true);
                                itm.target2.tag = "target2";
                                var behaviour2 = itm.target2.AddComponent<DynamicImageTagetBehaviour>();
                                behaviour2.whichTargetAmI = 2;
                                behaviour2.Name = obj.Key;
                                behaviour2.Path = obj.Value.Replace(@"\", "/");
                                behaviour2.Storage = StorageType.Absolute;
                                //binds tracking behaviour to target behavior script (required)
                                behaviour2.Bind(itm.tracker2);
                                //keeps track of name of target and behavior
                                imageTargetDic.Add(obj.Key, behaviour2);
                                //set the target status array to reflect that this target has been created
                                if (pathManager.targetStatus[1] == "none")
                                    pathManager.targetStatus[1] = "created";
                                fileCheck[1] = 1;
                                count++;
                      
                            break;
                        case "targetPhoto3":
                            //GameObject imageTarget3 = new GameObject(obj.Key);
                            //target3 = imageTarget3;
                            
                         
                                itm.target3.SetActive(true);
                                itm.target3.tag = "target3";
                                var behaviour3 = itm.target3.AddComponent<DynamicImageTagetBehaviour>();
                                behaviour3.whichTargetAmI = 3;
                                behaviour3.Name = obj.Key;
                                behaviour3.Path = obj.Value.Replace(@"\", "/");
                                behaviour3.Storage = StorageType.Absolute;
                                //binds tracking behaviour to target behavior script (required)
                                behaviour3.Bind(itm.tracker3);
                                //keeps track of name of target and behavior
                                imageTargetDic.Add(obj.Key, behaviour3);
                                //set the target status array to reflect that this target has been created
                                if (pathManager.targetStatus[2] == "none")
                                    pathManager.targetStatus[2] = "created";
                                fileCheck[2] = 1;
                                count++;
                          
                            break;
                        case "targetPhoto4":
                            //GameObject imageTarget4 = new GameObject(obj.Key);
                            //target4 = imageTarget4;
                     
                          
                                itm.target4.SetActive(true);
                                itm.target4.tag = "target4";
                                var behaviour4 = itm.target4.AddComponent<DynamicImageTagetBehaviour>();
                                behaviour4.whichTargetAmI = 4;
                                behaviour4.Name = obj.Key;
                                behaviour4.Path = obj.Value.Replace(@"\", "/");
                                behaviour4.Storage = StorageType.Absolute;
                                //binds tracking behaviour to target behavior script (required)
                                behaviour4.Bind(itm.tracker4);
                                //keeps track of name of target and behavior
                                imageTargetDic.Add(obj.Key, behaviour4);
                                //set the target status array to reflect that this target has been created
                                if (pathManager.targetStatus[3] == "none")
                                    pathManager.targetStatus[3] = "created";
                                fileCheck[3] = 1;
                                count++;
                         
                            break;
                        case "targetPhoto5":
                            //GameObject itm.target5 = new GameObject(obj.Key);
                            //target5 = itm.target5;
                   
                         
                                itm.target5.SetActive(true);
                                itm.target5.tag = "target5";
                                var behaviour5 = itm.target5.AddComponent<DynamicImageTagetBehaviour>();
                                behaviour5.whichTargetAmI = 5;
                                behaviour5.Name = obj.Key;
                                behaviour5.Path = obj.Value.Replace(@"\", "/");
                                behaviour5.Storage = StorageType.Absolute;
                                //binds tracking behaviour to target behavior script (required)
                                behaviour5.Bind(itm.tracker5);
                                //keeps track of name of target and behavior
                                imageTargetDic.Add(obj.Key, behaviour5);
                                //set the target status array to reflect that this target has been created
                                if (pathManager.targetStatus[4] == "none")
                                    pathManager.targetStatus[4] = "created";
                                fileCheck[4] = 1;
                                count++;
                          
                            break;
                    }
                }
            }


        }
}
