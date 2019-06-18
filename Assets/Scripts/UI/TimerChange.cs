using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerChange : MonoBehaviour
{

    public GameObject timer;
    public Text timertext;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TimerAni(timer.GetComponent<Text>().text);
    }

    public void TimerAni(string value)
    {
        timer.GetComponent<Text>().text = value;


        if (value.Equals("00 : 30"))
        {
            anim.Play("TimerAni");

        }



       
    }
}
