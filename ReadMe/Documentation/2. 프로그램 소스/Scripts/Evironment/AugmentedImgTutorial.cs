using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AugmentedImgTutorial : MonoBehaviour
{

    private List<AugmentedImage> currentDetectedAugementedImages = new List<AugmentedImage>();
    private Dictionary<String, GameObject> augmentedObjects;
    public GameObject player;
    public float spawnMergin = 4;

    public GameObject testObj; //테스트를 위한 오브젝트
    private GameObject objInstance; //나타난 오브젝트
    private GameObject ex; //소화기

    private int itemCount = 0; //찾은 아이템 수;
    public Text countText; // 찾은 아이템 수를 표시해주는 텍스트
    [HideInInspector] public Text imgTraking; //이미지 트래킹 스테이터스
    [HideInInspector] public Text centerPose; //이미지 중앙 위치 표시
    [HideInInspector] public Text objPose; //나타난 오브젝트 위치
    public GameObject FitToScanOverlay;

    public GameObject dialogPanel;
    public Text dialog;



    private void Start()
    {
        augmentedObjects = ItemManager.items;

    }

    public void SkipTutorial()
    {
        Debug.Log("클릭");
        dialog.text = "튜토리얼을 스킵하시겠습니까?";
        dialogPanel.SetActive(true);
    }
    public void LoadScene()
    {   
        SceneManager.LoadScene("Main");
    }

    public void UseEx()
    {
        ex.GetComponent<Shooter>().ActivateShooter();
    }

    public void CheckifExSucceded()
    {
        if(ex != null)
        {
            if(ex.GetComponent<Shooter>().extinguishedCount > 5)
            {
                dialog.text = "잘했어요! 이제 다른 아이템도 찾아볼까요?";
                dialogPanel.SetActive(true);
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (itemCount == 3)
        {
            dialogPanel.SetActive(true);
        }

        CheckifExSucceded();


        Session.GetTrackables<AugmentedImage>(currentDetectedAugementedImages, TrackableQueryFilter.New);

        if (Session.Status != SessionStatus.Tracking) return;

        FitToScanOverlay.SetActive(true);



        foreach (var image in currentDetectedAugementedImages)
        {
            foreach (var objects in augmentedObjects)
            {
                
                if (image.Name == objects.Key)
                {
                    GetComponent<AudioSource>().Play(); // 오브젝트 생성 효과음 플레이
                    itemCount++;
                    countText.text = itemCount.ToString();

                    //트래킹 상태면 이미지타깃 위에 생성하고
                    if (image.TrackingState == TrackingState.Tracking)
                    {
                        Anchor anchor = image.CreateAnchor(image.CenterPose);
                        objects.Value.transform.parent = anchor.transform;
                        objects.Value.transform.localPosition = new Vector3(0, 0, 0);

                        objInstance = Instantiate(objects.Value, objects.Value.transform.position, anchor.transform.rotation);
                        
                    }
                    //아니어도 플레이어 위치에 생성하고 (트래킹 상태가 잘 안되서 걍..)
                    else
                    {
                        objInstance = Instantiate(objects.Value, player.transform.position, Quaternion.LookRotation(player.transform.position));


                    }

                   

                }

            }
            

        }
        
        

    }
}
