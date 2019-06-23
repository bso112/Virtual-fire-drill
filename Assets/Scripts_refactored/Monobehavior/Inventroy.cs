using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    public refactored.Slot[] slots;


    /// <summary>
    /// 슬롯에 아이템을 넣는다.
    /// </summary>
    public void PickItem()
    {
        if (ItemGetter.clickeditem.GetComponent<Item>().itemType != Item.Type.Equipment)
        {
            ObjectFactory.Instance.Dialog.text = "습득할 수 없는 아이템입니다"; ObjectFactory.Instance.Dialog.gameObject.SetActive(true);
        }
        else
        {
            foreach (var s in slots)
            {
                if (s.IsEmptySlot())
                {
                    s.SetSlot();
                    break;
                }
            }
        }
        
    }

    public void DiscardSlot()
    {
        foreach (var s in slots)
        {
            if (!s.IsEmptySlot())
            {
                s.DisCardSlot();
                break;
            }
        }
    }

   
}
