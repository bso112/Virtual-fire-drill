using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;

/// <summary>
/// Uses 4 frame corner objects to visualize an AugmentedImage.
/// </summary>
public class FirePropSpawner : MonoBehaviour
{
    public GameObject extinguisherPrefab;
    public GameObject alarmPrefab;
    public GameObject gasPrefab;
    public GameObject sandPrefab;
    public GameObject multitabPrefab;

    private List<AugmentedImage> currentDetectedAugementedImages = new List<AugmentedImage>();

    public void Update()
    {
        Session.GetTrackables<AugmentedImage>(currentDetectedAugementedImages, TrackableQueryFilter.New);

        foreach (var image in currentDetectedAugementedImages)
        {

            //float targetExtenX = image.ExtentX / 2; //¿ìÃø/2
            //float targetExtenZ = image.ExtentZ / 2; //ÁÂ°ª/2
            
            

            

            if (image.Name == "extinguisher")
            {
                Instantiate(extinguisherPrefab, image.CenterPose.position, image.CenterPose.rotation);

                //extinguisherPrefab.transform.position = image.CenterPose.position;
                //extinguisherPrefab.transform.rotation = image.CenterPose.rotation;
                //extinguisherPrefab.transform.Rotate(0, 2, 0);
                //Instantiate(extinguisherPrefab);



            }
            else if (image.Name == "alarm")
            {
                //Instantiate(alarmPrefab, image.CenterPose.position, image.CenterPose.rotation);
                alarmPrefab.transform.position = image.CenterPose.position;
                alarmPrefab.transform.rotation = image.CenterPose.rotation;
                alarmPrefab.transform.Rotate(0, 2, 0);
                Instantiate(alarmPrefab);
            }
            else if (image.Name == "gas")
            {
                //Instantiate(gasPrefab, image.CenterPose.position, image.CenterPose.rotation);
                gasPrefab.transform.position = image.CenterPose.position;
                gasPrefab.transform.rotation = image.CenterPose.rotation;
                gasPrefab.transform.Rotate(0, 2, 0);
                Instantiate(gasPrefab);
            }
            else if (image.Name == "multitab")
            {
                //Instantiate(multitabPrefab, image.CenterPose.position, image.CenterPose.rotation);
                multitabPrefab.transform.position = image.CenterPose.position;
                multitabPrefab.transform.rotation = image.CenterPose.rotation;
                multitabPrefab.transform.Rotate(0, 2, 0);
                Instantiate(multitabPrefab);
            }
            else if (image.Name == "sand")
            {
                //Instantiate(sandPrefab, image.CenterPose.position, image.CenterPose.rotation);
                sandPrefab.transform.position = image.CenterPose.position;
                sandPrefab.transform.rotation = image.CenterPose.rotation;
                sandPrefab.transform.Rotate(0, 2, 0);
                Instantiate(sandPrefab);
            }
           
        }

    }
}
