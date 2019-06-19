using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Sprite normalImg;
    public Sprite mouseOverImg;

    public void MouseOver()
    {
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
        gameObject.GetComponent<Image>().sprite = mouseOverImg;
    }

    public void MouseExit()
    {
        transform.GetChild(0).GetComponent<Text>().color = Color.white;
        gameObject.GetComponent<Image>().sprite = normalImg;
    }
}
