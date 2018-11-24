//=============================================================================================================================
//
// Copyright (c) 2015-2018 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using EasyAR;
using System.Collections;
using System.IO;

namespace Sample
{
    public class ImageTargetManager : MonoBehaviour
    {
        public FilesManager pathManager;
        public targetObjectManager tom;
        public ImageTargetSetter its;
        public FilesManager fm;
        public GameObject target1;
        public GameObject target2;
        public GameObject target3;
        public GameObject target4;
        public GameObject target5;
        public UnityEngine.UI.Image viewPreview1;
        public UnityEngine.UI.Image viewPreview2;
        public UnityEngine.UI.Image viewPreview3;
        public UnityEngine.UI.Image viewPreview4;
        public UnityEngine.UI.Image viewPreview5;
        public UnityEngine.UI.Image blankImage;
        public ImageTrackerBehaviour tracker1;
        public ImageTrackerBehaviour tracker2;
        public ImageTrackerBehaviour tracker3;
        public ImageTrackerBehaviour tracker4;
        public ImageTrackerBehaviour tracker5;
        public int count = 0;
        public bool activeTarget1;
        public bool activeTarget2;
        public bool activeTarget3;
        public bool activeTarget4;
        public bool activeTarget5;
        public Text title;
        public Text editTitle;
        public Text description;
        private Dictionary<string, DynamicImageTagetBehaviour> imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();
        //public List<GameObject> targetList = new List<GameObject>();

        void Start()
        {
           
        }

       

        //deletes all targets
        public void ClearAllTarget()
        {
            foreach (var obj in imageTargetDic)
            {
                Destroy(obj.Value.gameObject.GetComponent<DynamicImageTagetBehaviour>());
                //if a model has been added to the target, delete it
                if(obj.Value.gameObject.transform.childCount == 4)
                    Destroy(obj.Value.gameObject.transform.GetChild(3).gameObject);
                obj.Value.gameObject.SetActive(false);
            }

            its.imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();

            for(int index =0; index < 5; index++)
            {
                pathManager.targetStatus[index] = "none";
            }

            //set target to represent to targets saved
            pathManager.targetCount = 0;
            pathManager.currentTarget = 0;
            count = 0;
        }


        //deletes all targets (current version)
        public void DeleteAllTargetsAndText()
        {
            Debug.Log("1. starting itm.DeleteAllTargetsAndText...");
            foreach (var obj in imageTargetDic)
            {
                Destroy(obj.Value.gameObject.GetComponent<DynamicImageTagetBehaviour>());
            }

            Destroy(target1.GetComponent<DynamicImageTagetBehaviour>());
            Destroy(target2.GetComponent<DynamicImageTagetBehaviour>());
            Destroy(target3.GetComponent<DynamicImageTagetBehaviour>());
            Destroy(target4.GetComponent<DynamicImageTagetBehaviour>());
            Destroy(target5.GetComponent<DynamicImageTagetBehaviour>());

            MeshRenderer m1 = target1.GetComponent<MeshRenderer>();
            MeshRenderer m2 = target2.GetComponent<MeshRenderer>();
            MeshRenderer m3 = target3.GetComponent<MeshRenderer>();
            MeshRenderer m4 = target4.GetComponent<MeshRenderer>();
            MeshRenderer m5 = target5.GetComponent<MeshRenderer>();

            m1.enabled = true;
            m2.enabled = true;
            m3.enabled = true;
            m4.enabled = true;
            m5.enabled = true;


            //reset all objects for targets
            for (int i = 0; i<5; i++)
            {
                tom.manualRemoveTargetObject(i+1);
                pathManager.targetStatus[i] = "none";
            }

            //set strings for each target photo path
            string target1Path = Path.Combine(pathManager.MarksDirectory, "targetPhoto1.jpg");
            string target2Path = Path.Combine(pathManager.MarksDirectory, "targetPhoto2.jpg");
            string target3Path = Path.Combine(pathManager.MarksDirectory, "targetPhoto3.jpg");
            string target4Path = Path.Combine(pathManager.MarksDirectory, "targetPhoto4.jpg");
            string target5Path = Path.Combine(pathManager.MarksDirectory, "targetPhoto5.jpg");

            //delete each target photo if it exists
            if (File.Exists(target1Path))
            {
                File.Delete(target1Path);
            }
            if (File.Exists(target2Path))
            {
                File.Delete(target2Path);
            }
            if (File.Exists(target3Path))
            {
                File.Delete(target3Path);
            }
            if (File.Exists(target4Path))
            {
                File.Delete(target4Path);
            }
            if (File.Exists(target5Path))
            {
                File.Delete(target5Path);
            }

            viewPreview1.sprite = blankImage.sprite;
            viewPreview2.sprite = blankImage.sprite;
            viewPreview3.sprite = blankImage.sprite;
            viewPreview4.sprite = blankImage.sprite;
            viewPreview5.sprite = blankImage.sprite;

            pathManager.currentTarget = 0;
            its.imageTargetDic= new Dictionary<string, DynamicImageTagetBehaviour>();

            title.text = "";
            editTitle.text = "";
            description.text = "";

            pathManager.targetCount = 0;
        }
        
        //gets called by UI to delete currently indexed target
        public void DeleteTarget(int whichTarget)
        {
            //if there is no target loaded to this index, do nothing
            if (pathManager.targetStatus[whichTarget-1] == "none")
                return;
            int localCount = 1;

            Debug.Log("1. Starting DeleteTarget()");

            string thisPath = "";
            switch(whichTarget)
            {
                case 1:
                    Debug.Log("2. Deleting targetPhoto1.jpg");
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto1.jpg");
                    File.Delete(thisPath);
                    Destroy(target1.GetComponent<DynamicImageTagetBehaviour>());
                    its.imageTargetDic.Remove("targetPhoto1");
                    tom.removeTargetObject(1);
                    fm.targetStatus[0] = "none";
                    break;
                case 2:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto2.jpg");
                    File.Delete(thisPath);
                    Destroy(target2.GetComponent<DynamicImageTagetBehaviour>());
                    its.imageTargetDic.Remove("targetPhoto2");
                    tom.removeTargetObject(2);
                    fm.targetStatus[1] = "none";
                    break;
                case 3:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto3.jpg");
                    File.Delete(thisPath);
                    Destroy(target3.GetComponent<DynamicImageTagetBehaviour>());
                    its.imageTargetDic.Remove("targetPhoto3");
                    tom.removeTargetObject(3);
                    fm.targetStatus[2] = "none";
                    break;
                case 4:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto4.jpg");
                    File.Delete(thisPath);
                    Destroy(target4.GetComponent<DynamicImageTagetBehaviour>());
                    its.imageTargetDic.Remove("targetPhoto4");
                    tom.removeTargetObject(4);
                    fm.targetStatus[3] = "none";
                    break;
                case 5:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto5.jpg");
                    File.Delete(thisPath);
                    Destroy(target5.GetComponent<DynamicImageTagetBehaviour>());
                    its.imageTargetDic.Remove("targetPhoto5");
                    tom.removeTargetObject(5);
                    fm.targetStatus[4] = "none";
                    break;
            }

            pathManager.targetCount --;

            //destroy the indexed target script, model assigned to target, and reset youtube player
            /* 
            foreach (var obj in imageTargetDic)
            {
                
                Destroy(obj.Value.gameObject.GetComponent<DynamicImageTagetBehaviour>());
                
                if (localCount == pathManager.currentTarget)
                {
                    if (obj.Value.gameObject.transform.childCount == 4)
                        Destroy(obj.Value.gameObject.transform.GetChild(3).gameObject);
                    
                    obj.Value.gameObject.transform.GetChild(0).gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().url = "none";
                    obj.Value.gameObject.transform.GetChild(0).gameObject.GetComponent<HighQualityPlayback>().videoId = "bc0sJvtKrRM";
                }
                localCount++;
            }
            string thisPath = "";
            switch(pathManager.currentTarget)
            {
                case 1:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto1.jpg");
                    File.Delete(thisPath);
                    break;
                case 2:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto2.jpg");
                    File.Delete(thisPath);
                    break;
                case 3:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto3.jpg");
                    File.Delete(thisPath);
                    break;
                case 4:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto4.jpg");
                    File.Delete(thisPath);
                    break;
                case 5:
                    thisPath = Path.Combine(pathManager.MarksDirectory, "targetPhoto5.jpg");
                    File.Delete(thisPath);
                    break;
            }

            pathManager.targetStatus[pathManager.currentTarget-1] = "none";
            pathManager.targetCount --;
            imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();
            count = 0;
            */
        }

    // THIS FUNCTION CURRENTLY UNUSED
    //used for finding closest target index that exists and making that the current target when we delete the current target
        private void setToClosestExistingTarget()
        {
            //check for availability of next target (pathManager.currentTarget - 1 represents the starting target)
            if (pathManager.currentTarget < 5 && pathManager.currentTarget > -1 && pathManager.targetStatus[pathManager.currentTarget] != "none") 
                pathManager.currentTarget++;

            //check for availability of prev target
            else if (pathManager.currentTarget-2 < 5 && pathManager.currentTarget-2 > -1 && pathManager.targetStatus[pathManager.currentTarget-2] != "none")
                pathManager.currentTarget--;

            //check for availability of target 2 places next
            else if (pathManager.currentTarget+1 < 5 && pathManager.currentTarget+1 > -1 && pathManager.targetStatus[pathManager.currentTarget+1] != "none")
                pathManager.currentTarget += 2;
            
            //check for availability of target 2 places previous
            else if (pathManager.currentTarget-3 < 5 && pathManager.currentTarget-3 > -1 && pathManager.targetStatus[pathManager.currentTarget-3] != "none")
                pathManager.currentTarget -= 2;

            //check for availability of target 3 places next
            else if (pathManager.currentTarget+2 < 5 && pathManager.currentTarget+2 > -1 && pathManager.targetStatus[pathManager.currentTarget+2] != "none")
                pathManager.currentTarget += 3;

            //check for availability of target 3 places previous
            else if (pathManager.currentTarget-4 < 5 && pathManager.currentTarget-4 > -1 && pathManager.targetStatus[pathManager.currentTarget-4] != "none")
                pathManager.currentTarget -= 3;
            
            //check for availability of target 4 places next (last possible next index)
            else if (pathManager.currentTarget+3 < 5 && pathManager.currentTarget+3 > -1 && pathManager.targetStatus[pathManager.currentTarget+3] != "none")
                pathManager.currentTarget += 4;
            
            //check for availability of target 4 places previous (last possible previous index)
            else if (pathManager.currentTarget-5 < 5 && pathManager.currentTarget-5 > -1 && pathManager.targetStatus[pathManager.currentTarget-5] != "none")
                pathManager.currentTarget -= 4;

            //else, there are no targets available, set to 0 because user must make a target
            else    
                pathManager.currentTarget = 0;
        }
    }
}

