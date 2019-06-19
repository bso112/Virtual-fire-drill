using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public bool isEmpty = false;


    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Image>().sprite == null)
        {
            isEmpty = true;
        }
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            isEmpty = false;
        }
    }

    public void Additem(Sprite itemImg)
    {
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        this.gameObject.GetComponent<Image>().sprite = itemImg;
    }
}
