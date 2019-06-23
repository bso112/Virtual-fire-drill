using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Item : MonoBehaviour
{
    public Sprite defaultImg;
    public Type itemType;
    public Usage state;
    public ItemInfo itemInfo;


    public enum Type
    {
        Equipment,//장비. 사용할 수 있고 장착할 수 있는 아이템.
        Useable, // 사용할 수 있고 장착할 수 없는 아이템.
        Misc//기타 사용할 수 없고 장착할 수 없는 아이템(정보만 볼 수 있음)
    }

    public enum Usage
    {
        IDEL,
        USING,
        USED,

    }

    public void Start()
    {
        //오브젝트 팩토리에 등록된 아이템이 아니면 파괴한다.
        ObjectFactory factory = ObjectFactory.Instance;
        factory.DestroyIFNotRegisterd(gameObject);
    }

    public void UseItem()
    {
        ObjectFactory factory = ObjectFactory.Instance;
        GameObject clickedItem = ItemGetter.clickeditem;
        Text Dialog = factory.Dialog;
        GameObject DialogPanel = factory.DialogPanel;

        if (gameObject.name == factory.extinguisher.name || gameObject.name == factory.sand.name
            || gameObject.name == factory.waterBucket.name)
        {
            gameObject.GetComponent<Shooter>().ActivateShooter();
        }
        else if (gameObject.name == factory.alarm.name || gameObject.name == factory.cat.name)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (gameObject.name == factory.gasValve.name)
        {
            //이미 사용됬으면 아무것도 안함
            if (clickedItem.GetComponent<Item>().state == Item.Usage.USED) return;

            Mission mission = GameManager.GetInstance().mission;
            //스타트미션에서 랜덤으로 붙은 불이 꺼졌을때
            if (GameManager.GetInstance().stm.ranObj != "towel" && Mission.missionName == "startMission")
            {
                Dialog.text = mission.missionDialog[0];
                DialogPanel.SetActive(true);
                mission.OnMissionSucceded();
                Mission.isMissonSucced = true; Mission.isMissionOn = false;
            }

            this.state = Item.Usage.USED;
        }
        else if (gameObject.name == factory.elevator.name)
        {
            factory.VideoWindow.SetActive(true);

            gameObject.GetComponent<VideoPlayer>().loopPointReached += delegate (VideoPlayer vp)
            {
                factory.hp.value -= 10f;
                Dialog.text = "화재시 엘리베이터를 사용하면 위험합니다!";
                DialogPanel.SetActive(true);
            };

        }
        else if (gameObject.name == factory.door.name)
        {
            if (Mission.ComplishedMissionCount >= 1)
            {
                factory.GameComplitedPanel.GetComponent<InfoSetter>().SetPlayerInfo();
                factory.GameComplitedPanel.SetActive(true);
            }
            else
                Dialog.text = "아직 일러요! 더 많은 미션을 성공하고 오세요!";
                DialogPanel.SetActive(true);
        }






    }





}
