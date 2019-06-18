using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{

    public static AnswerScript instance = null;
   public GameObject correct;
    public GameObject wrong;
    public void Awake()
    {
        if (!instance)
        {
           instance = this;
        }
    }

    // Use this for initialization

    void Start()
    {
        correct.SetActive(false);
        wrong.SetActive(false);
    }


}