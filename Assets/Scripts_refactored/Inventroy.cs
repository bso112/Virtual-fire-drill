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
        foreach(var s in slots)
        {
            if (s.IsEmptySlot())
            {
                s.SetSlot();
                break;
            }
        }
    }
}
