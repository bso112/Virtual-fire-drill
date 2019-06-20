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
        private GameObject clickedItem = ItemGetter.Getclickeditem;

        private void Start()
        {
            image = gameObject.GetComponent<Image>();

        }
        //슬롯에 이미지를 셋팅한다.
        public void SetSlot()
        {
            image.color = new Color(255, 255, 255, 255);
            image.sprite = clickedItem.GetComponent<Item>().defaultImg;
        }
        //슬롯에서 이미지를 지운다.
        public void DisCardSlot()
        {
            image.color = new Color32(0, 0, 0, 0);
            image.sprite = null;

        }
        /// <summary>
        /// 슬롯이 비어있으면 true 슬롯이 차있으면 false
        /// </summary>
        /// <returns></returns>
        public bool IsEmptySlot()
        {
            return (image.sprite == null);
        }

    }
}

