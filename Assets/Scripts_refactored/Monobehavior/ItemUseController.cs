using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
   public void UseItem()
    {

        GameObject clickedItem = ItemGetter.clickeditem;
        if (ItemGetter.clickeditem.GetComponent<Item>().itemType == Item.Type.Misc)
        {
            ObjectFactory.Instance.Dialog.text = "사용할 수 없는 아이템입니다.";
            ObjectFactory.Instance.Dialog.gameObject.SetActive(true);
            return;
        }
            
        //equipment형 아이템을 숨긴다
        if (clickedItem.tag == "item" && clickedItem.GetComponent<Item>().itemType == Item.Type.Equipment && clickedItem.name != "cat")
        {
            Renderer[] renderers = clickedItem.transform.GetComponentsInChildren<Renderer>();
            Collider[] cols = clickedItem.transform.GetComponentsInChildren<Collider>();

            foreach (var ren in renderers)
            {
                ren.enabled = false;
            }
            foreach (var col in cols)
            {
                col.enabled = false;
            }

        }
        clickedItem.GetComponent<Item>().UseItem();
    }

    public void UseItemViaImage()
    {   
        if(CanvasRaycaster.slot != null)
        {
            CanvasRaycaster.slot.item.UseItem();
        }

    }

}
