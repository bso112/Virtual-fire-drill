using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite defaultImg;
    public Type itemType;
    public Usage state;
   


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

    public void ItemToIdel()
    {
        state = Usage.IDEL;
    }



}
