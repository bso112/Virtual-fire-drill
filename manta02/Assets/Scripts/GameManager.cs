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

    public Text score;
    public GameObject hp;
    public Mission mission;
    private NoFactorDel timeOutDel;
    private MissionDel setMissionDel;



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
        //게임 시작과 동시에 첫 미션 스타트
       
        mission = new StartMission(); //미션의 생성자에서 타임스냅샷 찍음
        StartCoroutine("update");
        timeOutDel = new NoFactorDel(TimeOut);
        setMissionDel = new MissionDel(SetMission);
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = true;
        }

    }



    float timeSnapshot = 0; 

    IEnumerator update()
    {
        while (true)
        {

            second += Time.deltaTime;
            time += Time.deltaTime;
            timer.text = string.Format("{0:D2} : {1:D2}", min, (int)second);
            if (second >= 60)
            {
                min += 1;
                second = 0;
            }

            
            
            Mission.isMissionOn = true; //프레임마다 실행
            mission.MissionEvent(); //마우스를 누를 때마다 실행 
            MissionTime(9f, ref flags[0]); //한번만 실행

            // 메모)델리게이트 넘나 복잡 + 한 미션객체에 여러 미션을 넣으면 꼬여버린다.

            if (second > 10)
            {
                RunOnlyOneTime(setMissionDel, new ExtinguisherMission(), ref flags[1]);
                MissionTime(9f, ref flags[1]);
                
            }
           
            yield return null;

        }
        
    }

    void SetMission(Mission mission)
    {
        Mission.isMissionOn = true;
        this.mission = mission;
    }
    

    void MissionTime(float limit, ref bool flag)
    {
        if (second > timeSnapshot + limit)
        {
            RunOnlyOneTime(timeOutDel, ref flag);
        }

        
    }

    void TimeOut()
    {
        Debug.Log("타임아웃");
        mission.dialog.text = mission.missionDialog[1];
        mission.dialogPanel.SetActive(true);
        Mission.isMissonSucced = false;
        Mission.isMissionOn = false;
    }

    bool[] flags = new bool[10]; // RunOnlyOneTime 메소드를 위한 일회용 플래그들


    //메소드를 enable 처럼 끄고 켜주는 스위치
    void RunOnlyOneTime(NoFactorDel del, ref bool flag)
    {
        if (flag) { del();}
        flag = false;
        
    }

    void RunOnlyOneTime(MissionDel del, Mission mission, ref bool flag)
    {
        if (flag) { del(mission); }
        flag = false;

    }

    //이벤트로 구현해보기..
    void MissionControl()
    {

        


        
    }


}
