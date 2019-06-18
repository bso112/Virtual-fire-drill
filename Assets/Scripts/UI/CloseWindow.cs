using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    //이 스크립트는 닫힐 필요가 있는 모든 UI에 붙이고, 그 UI게임오브젝트(자기자신)를 버튼 이벤트에 넣어야함.
    //함수를 호출하는 버튼객체에 접근하기 위해 따로 빼서 각 컴포넌트에 붙이고, gameObject로 접근함.

    public void CloseParent()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void CloseThis()
    {
        gameObject.SetActive(false);
        if(gameObject.name == "VideoWindow")
        {
            InGameUIControl.GetInstance().VideoOver(GameObject.Find(ItemManager.items["elevator"].name).GetComponent<UnityEngine.Video.VideoPlayer>());
            SoundManger.instance.musicSource.volume = .3f;
        }
    }

   

}
