using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Uses 4 frame corner objects to visualize an AugmentedImage.
/// </summary>
public class FirePropSpawner : MonoBehaviour
{


    private List<AugmentedImage> currentDetectedAugementedImages = new List<AugmentedImage>();
    private Dictionary<String, GameObject> augmentedObjects;
    public GameObject player;
    public float spawnMergin = 4;
    private GameManager gm;
    private StartMission stm;
    
    

    private void Start()
    {
        gm = GameManager.GetInstance();
        augmentedObjects = ItemManager.items;

#if UNITY_EDITOR
        foreach (var objects in augmentedObjects)
        {
            
            GameObject item = Instantiate(objects.Value, new Vector3(spawnMergin, 0, 0), Quaternion.identity);
            item.transform.LookAt(player.transform);
            item.name = objects.Key;
            spawnMergin *= 0.6f;

        }
#endif
    }

    public void Update()
    {
        Session.GetTrackables<AugmentedImage>(currentDetectedAugementedImages, TrackableQueryFilter.New);

        
        
        if (Session.Status != SessionStatus.Tracking) return;

    
        foreach (var image in currentDetectedAugementedImages)
        {
           

            foreach (var objects in augmentedObjects)
            {
                

                if (image.Name == objects.Key)
                {
                    
                    GetComponent<AudioSource>().Play(); // 오브젝트 생성 효과음 플레이

                    if (image.TrackingState == TrackingState.Tracking)
                    {
                        Anchor anchor = image.CreateAnchor(image.CenterPose);
                        objects.Value.transform.parent = anchor.transform;
                        objects.Value.transform.localPosition = new Vector3(0, 0, 0);
                        Instantiate(objects.Value, objects.Value.transform.position, anchor.transform.rotation).name = objects.Key;
                    }
                    else
                    {
                        Instantiate(objects.Value, player.transform.position, Quaternion.LookRotation(player.transform.position)).name = objects.Key;
                    }
                    //스타트미션에서, 화재원을 찾으면 다이어로그를 표시한다.
                    if (gm.stm != null && Mission.missionName == "화재원 찾기")
                    {
                        if(objects.Key == gm.stm.ranObj)
                        {
                            gm.stm.SetFire();
                            gm.stm.ShowDialog();

                        }

                 
                    }
                    
                    
                    
                }
                    
                

            }

        }

    }
}
