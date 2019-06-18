using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderChange : MonoBehaviour
{
    public GameObject timer;
    public Text timertext;
   

    public Slider slider;
    public GameObject hp;
    public Image SliderColor;
  
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        

        TimerUpdate(timer.GetComponent<Text>().text);
        SliderColorUpdate((int)hp.GetComponent<Slider>().value);
       
    }

    public void SliderColorUpdate(int val)
    {
        slider.value = val;

        if (val < 101)
        {
            SliderColor.color = new Color(0, 1, 0);   // 슬라이더 색생 변경 (r,g,b)
           
        }
            
        if (val < 60)
        {
            SliderColor.color = new Color(1, 1, 0);
            
        }

        if (val < 30)
        {
            SliderColor.color = new Color(1, 0, 0);
           
        }

    }

    public void TimerUpdate(string value)
    {
        timer.GetComponent<Text>().text = value;
      

        if (value.Equals("00 : 30"))
        {
            timertext.color = new Color(1, 0, 0);
           
        }
           


        if(value.Equals("03 : 00"))
        {
            timertext.color = new Color(1, 1, 1);

        }
          

    }
}
