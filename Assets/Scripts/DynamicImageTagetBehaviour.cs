//=============================================================================================================================
//
// Copyright (c) 2015-2017 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using EasyAR;

namespace Sample
{
    public class DynamicImageTagetBehaviour : ImageTargetBehaviour
    {
        public FilesManager fm;
        public ImageTargetManager itm;
        public int whichTargetAmI;
        public targetObjectManager tom;

         protected override void Awake()
        {
            base.Awake();
            fm = FindObjectOfType<FilesManager>();
            itm = FindObjectOfType<ImageTargetManager>();
            tom = FindObjectOfType<targetObjectManager>();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;
        }

        void OnTargetFound(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Found: " + Target.Id);
            //accesses fm to see if a video is assigned to this target and displays or does not display the video accordingly
            switch(whichTargetAmI)
            {
                case 1:
                        itm.activeTarget1 = true;
                        tom.videoPlayer1.gameObject.transform.position = new Vector3(0f,0f,0f);
                        GameObject.FindWithTag("videoPlayer1").GetComponent<SimplePlayback>().unityVideoPlayer.Play();
                    break;
                case 2:
                        itm.activeTarget2 = true;
                        tom.videoPlayer2.gameObject.transform.position = new Vector3(0f,0f,0f);
                        GameObject.FindWithTag("videoPlayer2").GetComponent<SimplePlayback>().unityVideoPlayer.Play();
                    break;
                case 3:
                        itm.activeTarget3 = true;
                        tom.videoPlayer3.gameObject.transform.position = new Vector3(0f,0f,0f);
                        GameObject.FindWithTag("videoPlayer3").GetComponent<SimplePlayback>().unityVideoPlayer.Play();
                    break;
                case 4:
                        itm.activeTarget4 = true;
                        tom.videoPlayer4.gameObject.transform.position = new Vector3(0f,0f,0f);
                        GameObject.FindWithTag("videoPlayer4").GetComponent<SimplePlayback>().unityVideoPlayer.Play();
                    break;
                case 5:
                        itm.activeTarget5 = true;
                        tom.videoPlayer5.gameObject.transform.position = new Vector3(0f,0f,0f);
                        GameObject.FindWithTag("videoPlayer5").GetComponent<SimplePlayback>().unityVideoPlayer.Play();
                    break;
            }
        }

        void OnTargetLost(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Lost: " + Target.Id);
            
            switch(whichTargetAmI)
            {
                case 1:
                    Debug.Log("new position");
                    itm.activeTarget1 = false;
                    itm.target1.transform.position = new Vector3(0f,0f,0f);
                    tom.videoPlayer1.gameObject.transform.position = new Vector3(2000f,0f,0f);
                    break;
                case 2:
                    itm.activeTarget2 = false;
                    itm.target2.transform.position = new Vector3(0f,0f,0f);
                    tom.videoPlayer2.gameObject.transform.position = new Vector3(2000f,0f,0f);
                    break;
                case 3:
                    itm.activeTarget3 = false;
                    itm.target3.transform.position = new Vector3(0f,0f,0f);
                    tom.videoPlayer3.gameObject.transform.position = new Vector3(2000f,0f,0f);
                    break;
                case 4:
                    itm.activeTarget4 = false;
                    itm.target4.transform.position = new Vector3(0f,0f,0f);
                    tom.videoPlayer4.gameObject.transform.position = new Vector3(2000f,0f,0f);
                    break;
                case 5:
                    itm.activeTarget5 = false;
                    itm.target5.transform.position = new Vector3(0f,0f,0f);
                    tom.videoPlayer5.gameObject.transform.position = new Vector3(2000f,0f,0f);
                    break;
            }
        }

        void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }

        void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }
    }
}
