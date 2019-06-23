using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace refactored
{
    public class Slot : MonoBehaviour
    {
        private Image image;
        [HideInInspector] public Item item; // 현재 슬롯에 들어있는 아이템.

        //슬롯에 이미지를 셋팅한다.
        public void SetSlot()
        {
            gameObject.SetActive(true);
            image.color = new Color(255, 255, 255, 255);
            item = ItemGetter.clickeditem.GetComponent<Item>();
            image.sprite = item.defaultImg;
        }
        //슬롯에서 이미지를 지운다.
        public void DisCardSlot()
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            gameObject.GetComponent<Image>().sprite = null;
            item = null;
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 슬롯이 비어있으면 true 슬롯이 차있으면 false
        /// </summary>
        /// <returns></returns>
        public bool IsEmptySlot()
        {
            image = gameObject.GetComponent<Image>();
            return (image.sprite == null)? true : false;
        }

        public void UseItem()
        {
            item.UseItem();
                
        }

        public void SetInfo()
        {
            ObjectFactory.Instance.InfoPanel.transform.GetComponentInChildren<Text>().text = item.itemInfo.itemInfo;
        }
        

    }
}

