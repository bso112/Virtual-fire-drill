using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    delegate void NoFactorDel();
    delegate void MissionDel(Mission mission);

    //난이도, 스코어, 타이머 등 전반적인 게임진행에 관한 스크립트
    private GameManager() { }
    public static GameManager instance;
    public Text timer;
    [HideInInspector] public int min;
    [HideInInspector] public float second;
    [HideInInspector] public float time;

    public Text score, missionTimeLimit;
    public GameObject hp;
    public Mission mission;
    private bool[] flags = new bool[10]; // 같은 조건(Mission.isMissionOn = true)에서 각 미션을 한번만 실행하기 위한 일회용 플래그

    public GameObject MissionTimePanel;

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
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = true;
        }


    }


    float timelimit = 0;
    float missionTime = 0;
    private void Update()
    {
        

        second += Time.deltaTime;
        time += Time.deltaTime;
        timer.text = string.Format("{0:D2} : {1:D2}", min, (int)second);
        if (second >= 60)
        {
            min += 1;
            second = 0;
        }

        //타임오버도 미션에서 할수는 없나?

        if (time > 18f)
        {
            if (flags[4]) { this.mission = new ExtinguisherMission(); timelimit = 10f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[4] = false; }  //한번만 실행
            MissionStart(mission, 5);
        }
        else if (time > 11f) // 10보다 작고 6보다 클때
        {

            if (flags[2]) { this.mission = new TestMission(); timelimit = 5f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[2] = false; }
            MissionStart(mission, 3);
        }
        else //6보다 작을때
        {

            if (flags[0]) { this.mission = new StartMission(); timelimit = 5f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[0] = false; } 
            MissionStart(mission, 1);

        }

       

    }

    public void MissionStart(Mission mission, int flagIndex)
    {
        mission.MissionEvent(); //클릭마다 실행
        missionTime -= Time.deltaTime; //매프레임마다 실행
        if (missionTime < 0) missionTime = 0;
        missionTimeLimit.text = missionTime.ToString();  //매프레임마다 실행
        if (time > mission.timeSnapshot + timelimit) { if (flags[flagIndex]) { TimeOut(); MissionTimePanel.SetActive(false); flags[flagIndex] = false; } } // 한번만 실행
        if (!Mission.isMissionOn) MissionTimePanel.SetActive(false);
    }


    void TimeOut() //한번만 실행되어야함
    {
        if (Mission.isMissionOn)
        {
            Debug.Log("타임아웃");
            mission.dialog.text = mission.missionDialog[1];
            mission.dialogPanel.SetActive(true);
            Mission.isMissonSucced = false;
            Mission.isMissionOn = false;
        }
    }


   

}
