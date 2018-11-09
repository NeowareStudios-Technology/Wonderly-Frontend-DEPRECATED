using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Vuforia;
using PolyToolkit;
using Sample;

public class ModelRenderer : MonoBehaviour {

	//private TrackableBehaviour mTrackableBehaviour;

    public GameObject currentTarget;
 
    public Transform myModelPrefab;
		
    public Text keyword;

    public ModelInitializer mi;

    public FilesManager fm;

    public int modelFlag = 0;

    public ImageTargetManager itm;

    public targetObjectManager tom;

    public string attributeString;
 
    // Update is called once per frame
    void Update ()
    {
    }
 
    /* 
    public void OnTrackableStateChanged(
              TrackableBehaviour.Status previousStatus,
              TrackableBehaviour.Status newStatus)
    {
        destroyChildren();
        GameObject.FindObjectOfType<HighQualityPlayback>().unityVideoPlayer.Pause();
        if ((newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)&&modelFlag == 0)
        {
            OnTrackingFound();
        }
    }


    private void OnTrackingFound()
    {
        GameObject.FindObjectOfType<HighQualityPlayback>().unityVideoPlayer.Play();
        if (myModelPrefab != null)
        {
            Transform myModelTrf = GameObject.Instantiate(myModelPrefab) as Transform;
 
             myModelTrf.parent = mTrackableBehaviour.transform;    
             myModelTrf.name = "renderedModel";         
             myModelTrf.localPosition = new Vector3(0f, 0.7f, 0f);
             myModelTrf.localRotation = Quaternion.identity;
             myModelTrf.localScale = new Vector3(4f, 4f, 4f);
 
             myModelTrf.gameObject.active = true;
         }
     }
    */
    private void destroyChildren(GameObject currentTarget) {
        for (int x = 0; x < currentTarget.transform.childCount; x++)
            Destroy(currentTarget.transform.GetChild(x).gameObject);
    }

    // Callback invoked when the featured assets results are returned.
    public void renderModel(int whichModel) {
        //if target not crerated for index yet or no targets exist, do nothing
        if (fm.currentTarget == 0)
            return;
        if (fm.targetStatus[fm.currentTarget-1] == "none")
            return;


        List<PolyAsset> renderList = new List<PolyAsset>();
        renderList.Add(mi.assetsInUse[whichModel]);
        
        attributeString = PolyApi.GenerateAttributions(includeStatic: true, runtimeAssets: renderList);


        //get rid of previous import and get asset and save model ID
        switch(fm.currentTarget)
        {
            case 0:
                return;
            case 1:
                if(itm.target1.transform.childCount == 4)
                    Destroy(itm.target1.transform.GetChild(3).gameObject);
                tom.modelId1 = ParseForModelId(attributeString);
                if (!tom.attribs.Contains(attributeString))
                    tom.attribs.Add(attributeString);
                PolyApi.GetAsset(tom.modelId1, GetAssetCallback);
                break;
            case 2:
                if(itm.target2.transform.childCount == 4)
                    Destroy(itm.target2.transform.GetChild(3).gameObject);
                tom.modelId2 = ParseForModelId(attributeString);
                if (!tom.attribs.Contains(attributeString))
                    tom.attribs.Add(attributeString);
                PolyApi.GetAsset(tom.modelId2, GetAssetCallback);
                break;
            case 3:
                if(itm.target3.transform.childCount == 4)
                    Destroy(itm.target3.transform.GetChild(3).gameObject);
                tom.modelId3 = ParseForModelId(attributeString);
                if (!tom.attribs.Contains(attributeString))
                    tom.attribs.Add(attributeString);
                PolyApi.GetAsset(tom.modelId3, GetAssetCallback);
                break;
            case 4:
                if(itm.target4.transform.childCount == 4)
                    Destroy(itm.target4.transform.GetChild(3).gameObject);
                tom.modelId4 = ParseForModelId(attributeString);
                if (!tom.attribs.Contains(attributeString))
                    tom.attribs.Add(attributeString);
                PolyApi.GetAsset(tom.modelId4, GetAssetCallback);
                break;
            case 5:
                if(itm.target5.transform.childCount == 4)
                    Destroy(itm.target5.transform.GetChild(3).gameObject);
                tom.modelId5 = ParseForModelId(attributeString);
                if (!tom.attribs.Contains(attributeString))
                    tom.attribs.Add(attributeString);
                PolyApi.GetAsset(tom.modelId5, GetAssetCallback);
                break;
        }
    }

    void GetAssetCallback(PolyStatusOr<PolyAsset> result) 
    {
        if (!result.Ok) 
            {
                Debug.Log("There was an error importing the loaded asset");
                return;
            }

        // Set the import options.
        PolyImportOptions options = PolyImportOptions.Default();
        // We want to rescale the imported meshes to a specific size.
        options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
        // The specific size we want assets rescaled to (fit in a 1x1x1 box):
        options.desiredSize = 1.0f;
        // We want the imported assets to be recentered such that their centroid coincides with the origin:
        options.recenter = true;

        PolyApi.Import(result.Value, options, ImportAssetCallback);
    }

    // Callback invoked when an asset has just been imported.
    private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result) {
        //only set "model" status on an already created target
        if (fm.targetStatus[fm.currentTarget-1] != "none")
        {
            GameObject myModelObject = result.Value.gameObject;
            myModelPrefab = result.Value.gameObject.GetComponent(typeof(Transform)) as Transform;
            myModelPrefab.transform.position = new Vector3(0.0f, 0.75f, 0f);
            fm.targetStatus[fm.currentTarget-1] = "model";

             //to decide which target to render the model to
            switch(fm.currentTarget)
            {
                case 0:
                    return;
                case 1:
                    myModelPrefab.tag = "importedModel1";
                    myModelPrefab.transform.parent = itm.target1.transform;
                    //model1 needs to get the model ID of the first model from attributesString
                    tom.modelId1 = ParseForModelId(attributeString);
                    tom.model1 = myModelObject;
                    break;
                case 2:
                    myModelPrefab.tag = "importedModel2";
                    myModelPrefab.transform.parent = itm.target2.transform;
                    tom.modelId2 = ParseForModelId(attributeString);
                    tom.model2 = myModelObject;
                    break;
                case 3:
                    myModelPrefab.tag = "importedModel3";
                    myModelPrefab.transform.parent = itm.target3.transform;
                    tom.modelId3 = ParseForModelId(attributeString);
                    tom.model3 = myModelObject;
                    break;
                case 4:
                    myModelPrefab.tag = "importedModel4";
                    myModelPrefab.transform.parent = itm.target4.transform;
                    tom.modelId4 = ParseForModelId(attributeString);
                    tom.model4 = myModelObject;
                    break;
                case 5:
                    myModelPrefab.tag = "importedModel5";
                    myModelPrefab.transform.parent = itm.target5.transform;
                    tom.modelId5 = ParseForModelId(attributeString);
                    tom.model5 = myModelObject;
                    break;
            }
        }
    }

    public void changeModelFlag() {
        if (modelFlag == 1)
        {
            modelFlag = 0;
        }
        else if (modelFlag == 0)
        {
            modelFlag = 1;
            
        }
    }

    private string ParseForModelId(string attribString)
    {
        //get beginning index of model ID
        int position1 = attribString.IndexOf("/view/");
        position1 += 6;

        //get ending index of model ID
        int position2 = attribString.IndexOf("License");
        position2 -= 1;

        string modelID = attribString.Substring(position1, position2-position1);

        return modelID;
    }
}
