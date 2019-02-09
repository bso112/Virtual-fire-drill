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
        
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = string.Format ("{0:N2}", time);
        time += Time.deltaTime;
        
        
       
    }
    
}
