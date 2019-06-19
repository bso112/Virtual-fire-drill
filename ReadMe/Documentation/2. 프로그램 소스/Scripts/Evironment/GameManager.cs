using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{


    //난이도, 스코어, 타이머 등 전반적인 게임진행에 관한 스크립트
    private GameManager() { }
    private static GameManager instance;
    public Text timer;
    [HideInInspector] public int min;
    [HideInInspector] public float second;
    [HideInInspector] public float time;

    
    float timelimit = 0;
    float missionTime = 0;

    public Text score, missionTimeLimit;
    public GameObject hp;

    public Mission mission;
    public StartMission stm;
    public GasMission gsm;
    public ExtinguisherMission exm;

    private bool[] flags = new bool[100]; // 같은 조건(Mission.isMissionOn = true)에서 각 미션을 한번만 실행하기 위한 일회용 플래그

    public GameObject MissionTimePanel;

    public AudioClip gameOverSound;


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

        second = 0f;
        // min = 8; //게임 전체시간
        min = 4; //게임 전체시간
        time = min * 60;

    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (time < 0) { return; }
        if (time >= 0)
        {
            second -= Time.deltaTime;
            time -= Time.deltaTime;
            timer.text = string.Format("{0:D2} : {1:D2}", min, (int)second);
            if (second < 0)
            {
                min -= 1;
                second += 60;
            }
        }
        if (min < 0)
        {
            second = 0f;
        }
        if (time <= 0 || hp.GetComponent<Slider>().value <= 0)
            GameOver();

        ////첫미션
        //if (time < 400f)
        //{
        //    if (flags[0]) { this.mission = new StartMission(); timelimit = 120f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[0] = false; }
        //    MissionStart(mission, 1);

        //}
        ////두번째 미션
        //if (time < 250f)
        //{
        //    if (flags[2]) { this.mission = new GasMission(); timelimit = 40f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[2] = false; }
        //    MissionStart(mission, 3);
        //}
        //if (time < 130f)
        //{

        //    if (flags[4]) { this.mission = new ExtinguisherMission(); timelimit = 120f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[4] = false; }  //한번만 실행
        //    MissionStart(mission, 5);

        //}
        

        //첫미션
        if (time > 115f && time < 240f)
        {
            if (flags[0]) { this.mission = this.stm = new StartMission(); timelimit = 120f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[0] = false; }
            MissionStart(stm, 1);

        }
        //두번째 미션
        if (time > 70f && time < 115f)
        {
            if (flags[2]) { this.mission = this.gsm = new GasMission(); timelimit = 40f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[2] = false; }
            MissionStart(gsm, 3);
        }
        if (time < 70f)
        {

            if (flags[4]) { this.mission = this.exm = new ExtinguisherMission(); timelimit = 30f; missionTime = timelimit; MissionTimePanel.SetActive(true); flags[4] = false; }  //한번만 실행
            MissionStart(exm, 5);

        }

    }

    public void MissionStart(Mission mission, int flagIndex)
    {
        mission.MissionEvent();
        missionTime -= Time.deltaTime; //매프레임마다 실행
        if (missionTime < 0) missionTime = 0;
        missionTimeLimit.text = missionTime.ToString("N3");  //매프레임마다 실행
        if (missionTime <= 0) { if (flags[flagIndex]) { mission.OnMissionFailed(); MissionTimePanel.SetActive(false); flags[flagIndex] = false; } } // 한번만 실행
        if (!Mission.isMissionOn) MissionTimePanel.SetActive(false);
    }

    public void GameOver()
    {
        SoundManger.instance.PlaySingle(gameOverSound);
        GameObject.Find("InGameCanvas").transform.Find("GameOverPanel").gameObject.SetActive(true);
        
    }

    public void ReStart()
    {
        Debug.Log("리스타트");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMain()
    {
        //SceneManager.LoadScene("title");
    }

    





}
