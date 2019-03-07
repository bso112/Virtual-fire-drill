using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //난이도, 스코어, 타이머 등 전반적인 게임진행에 관한 스크립트
    private GameManager() { }
    public static GameManager instance;
    public Text timer;
    [HideInInspector] public float time;

    [HideInInspector] public float Second = 0f;
    [HideInInspector] public int Min = 0;
    [HideInInspector] public float PlayTime = 0f;
    //체력바에 관한
    public Slider hpSlider;
    public float hp=10f;
    public float currentHP=10f;
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");

        }
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //게임이 시작되면
        //타이머
        Second =0f;
        Min = 1;
        PlayTime = Min*60;

        //체력바
        currentHP = hp;
    }

    // Update is called once per frame
    void Update()
    {

        hpSlider.maxValue = hp;

        hpSlider.value = currentHP;
         
        //TImer를 위한 
        if (Second >= 0)
        {

            Second -= Time.deltaTime;
            timer.text = string.Format("{0:D2} : {1:D2}", Min, (int)Second);
            if (Second < 0)
            {
                Min -= 1;
                Second += 60;
            }
        }
        if (Min < 0)
        {
            Second = 0f;
        }
        // timer.text = string.Format ("{0:N2}", time);
        // time += Time.deltaTime;

        //체력바를 위한



    }

}
