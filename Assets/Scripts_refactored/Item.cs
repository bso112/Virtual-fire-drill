using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void UseItem()
    {

        //오브젝트 팩토리에 등록된 아이템이 아니면 파괴한다.
        List<string> itemNames = new List<string>();
        ObjectFactory factory = ObjectFactory.Instance;

        foreach (var obj in factory.itemPrefabs)
        {
            itemNames.Add(obj.name);
        }
        if (!itemNames.Contains(gameObject.name))
            Destroy(gameObject);
        //아니면 사용한다.
        else
        {
            if(gameObject.name == factory.extinguisher.name || gameObject.name == factory.sand.name
                || gameObject.name == factory.waterBucket.name)
            {
                gameObject.GetComponent<Shooter>().ActivateShooter();
            }
            else if (gameObject.name == factory.alarm.name)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
            }
            else if (gameObject.name == factory.alarm.name)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
            }



        }
    
    }

    



}
