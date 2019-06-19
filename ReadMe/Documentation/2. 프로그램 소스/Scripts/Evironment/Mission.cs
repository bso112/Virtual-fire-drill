using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Mission

{
    protected GameManager gm = GameManager.GetInstance();
    protected InGameUIControl uiControl = InGameUIControl.GetInstance();
    public GameObject dialogPanel = InGameUIControl.GetInstance().dialogPanel;
    public Text dialog = InGameUIControl.GetInstance().dialog;
    protected static Text scoreText = GameManager.GetInstance().score;
    protected GameObject hp = GameManager.GetInstance().hp;
    public static int score;
    public static string missionName = "";
    public List<string> missionDialog = new List<string>();
    static public int ComplishedMissionCount = 0;

    public static List<string> succedMissionNames = new List<string>();
    public static List<string> failedMissionNames = new List<string>();
    

    protected Dictionary<string, GameObject> items = ItemManager.items; 

    [HideInInspector] public static bool isMissionOn, isMissonSucced = false; // 미션이 끝났다,성공했다 / 미션이 끝났다,실패했다/ 를 구분하기 위함

    public float timeSnapshot;

    public Mission()
    {
        OnMissionStart();
        timeSnapshot = gm.min * 60 + gm.second;
        missionDialog.Add("성공이에요!"); //미션다이어로그의 0번째는 성공메시지, 1번째는 실패메세지임.
        missionDialog.Add("실패에요!");
    }

    public abstract void MissionEvent(); //게임매니저에서 업데이트마다 실행


    public static void AddScore(int add)
    {
        int result = 0;
        SoundManger.instance.PlaySingle(SoundManger.instance.sucSound);
        result = int.Parse(scoreText.text) + add;
        scoreText.text = result.ToString();
        score = result;
    }

    public void OnMissionStart()
    {
        SoundManger.instance.PlaySingle(SoundManger.instance.MissionStartSound);
    }

    //성공로직 마지막에 실행되어야한다.
    public void OnMissionSucceded()
    {
        succedMissionNames.Add(missionName);
        ComplishedMissionCount++;
        dialog.text = missionDialog[0];
        dialogPanel.SetActive(true);
        isMissonSucced = true;
        isMissionOn = false;
        if (isMissonSucced) { score += int.Parse(scoreText.text); scoreText.text = score.ToString(); }
        SoundManger.instance.PlaySingle(SoundManger.instance.sucSound);

    }

    public void OnMissionFailed()
    {
        if (isMissionOn)
        {
            failedMissionNames.Add(missionName);
            SoundManger.instance.PlaySingle(SoundManger.instance.failSound);
            dialog.text = missionDialog[1];
            dialogPanel.SetActive(true);
            isMissonSucced = false;
            isMissionOn = false;

        }

    }



  
}




public class GasMission : Mission
{
    public GasMission()
    {
        Mission.missionName = "유독가스 대피";
        score = 1000;
        //미션 다이어로그의 0은 성공메세지, 1은 실패메세지다. 본 메세지는 2부터 시작한다.
        missionDialog.Add("물에 젖은 수건을 찾아보세요!!");
        missionDialog.Add("물수건 덕분에 유독가스를 막을 수 있어요!");
        dialog.text = missionDialog[2];
        dialogPanel.SetActive(true);
        isMissionOn = true;

    }

    float tmpTime = 0f;



    public override void MissionEvent()
    {
        if (isMissionOn)
        {
            tmpTime += Time.deltaTime;
            if (hp.GetComponent<Slider>().value >= 0f)
            {
                if (tmpTime > 0.3f) { hp.GetComponent<Slider>().value -= 1f; tmpTime = 0f; }
                if (!uiControl.clickedItem) return;
                if (uiControl.clickedItem.name == items["towel"].name && uiControl.clickedItem.GetComponent<Item>().state == Item.Usage.USED)
                {
                    OnMissionSucceded();
                    return;
                }

            }
        }

    }
}




public class StartMission : Mission
{

    public Shooter shooter;
    static int ran = Random.Range(0, 3);
    public static GameObject fire;
    string[] startItems;
    Touch tempTouches;
    public string ranObj;




    public StartMission()
    {
        Mission.missionName = "화재원 찾기";
        score = 1000;
        missionDialog.Add("어디선가 타는 냄새가 난다.. 주변을 둘러보자");
        missionDialog.Add("화재원을 찾았다! 서둘러 끄자!");
        if (dialog.text != missionDialog[2]) { dialog.text = missionDialog[2]; dialogPanel.SetActive(true); }
        isMissionOn = true;


        //가스밸브, 멀티탭, 오일스토브 인스턴스를 가져온다.
        startItems = new string[]{ "gasValve", "oilStove", "multitap"};

        ranObj = startItems[ran];
        

    }

    //랜덤 오브젝트에 불을 붙인다.
    public void SetFire()
    {
        if (ranObj != null)
        {
            fire =  GameObject.Find(ranObj).transform.Find("fire").gameObject;
            fire.SetActive(true);
        }
    }

    //다이어로그를 팝업한다.
    public void ShowDialog()
    {
        dialog.text = missionDialog[3];
        dialogPanel.SetActive(true);
    }


    //미션이 언제 실행되는지 업데이트에서 조건을 확인하지 않고, 조건을 하드코딩하거나 버튼 클릭등의 이벤트로 구체적으로 제공한다.
    public override void MissionEvent()
    {
        //매 클릭마다 실행
#if UNITY_EDITOR
        if (!Input.GetMouseButtonDown(0)) { return; }
#else
        if (Input.touchCount <= 0) { return; }
        for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouches = Input.GetTouch(0);
            }
#endif
        //isMissionOn이 true면
        if (isMissionOn)
        {
            //슈터가 없고 슈터가 붙은 클릭아이템이 있으면
            if (!shooter && uiControl.clickedItem && GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>())
            {

                //슈터를 가져온다.
                this.shooter = GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>();
            }
            if (!shooter) { return; }

            //불을 끄면
#if UNITY_EDITOR
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && hit.collider.gameObject.name == "fire")
            {
#else
                if (Physics.Raycast(Camera.main.ScreenPointToRay(tempTouches.position), out RaycastHit hit) && hit.collider.gameObject.name == "fire")
            { 
#endif

                //가스밸브는 불을 끈다음 밸브를 잠궈야(밸브-사용하기버튼 눌러야) 성공이다.
                if (ranObj == "gasValve")
                {
                    if (missionDialog[3] != "잘했어요!이제 가스밸브를 잠가주세요")
                        missionDialog[3] = "잘했어요! 이제 가스밸브를 잠가주세요"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    //밸브를 돌리면 성공(동기화 문제 때문에 InGameUiControl의 useitem에 구현함)

                }
                else if (ranObj == "multitap" && shooter.effect.name == "waterEffect")
                {
                    if (missionDialog[3] != "불은 껐지만 감전되어 체력이 감소했다!")
                        missionDialog[3] = "불은 껐지만 감전되어 체력이 감소했다!"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    hp.GetComponent<Slider>().value -= 10f;
                    OnMissionSucceded();


                }
                else if (ranObj == "oilStove" && shooter.effect.name == "waterEffect")
                {
                    if (missionDialog[3] != "물을 사용하면 불이 더 커집니다!")
                        missionDialog[3] = "물을 사용하면 불이 더 커집니다!"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    //불이 커진다.
                    ParticleSystem ps = fire.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startSize = 6f;
                }
                else
                {
                    OnMissionSucceded();

                }



            }

        }
    }

}

public class ExtinguisherMission : Mission
{
    //소화기, 모래로 불을 끌 수 있다.
    public Shooter shooter;


    //이 생성자는 미션습득 버튼이 클릭된 시점에서 한번만 실행된다.
    public ExtinguisherMission()
    {
        
        Mission.missionName = "불끄기";
        score = 1000;
        missionDialog.Add("불이 거세졌어요! 제한시간 내에 불을 끄세요!");
        missionDialog.Add("잘했어요 조금 더 꺼볼까요?");
        dialog.text = missionDialog[2];
        dialogPanel.SetActive(true);
        isMissionOn = true;


    }

    bool isDialogActivated = false; // 다이어로그 패널 active를 한번만 실행하기 위해 




    public override void MissionEvent()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButtonDown(0)) { return; }
#else
        if (Input.touchCount <= 0) { return; }
#endif


        if (isMissionOn)
        {


            //사용자가 소화아이템을 사용하는지 프레임마다 체크

            if (uiControl.clickedItem && !shooter)
            {

                //클릭된 아이템이 슈터를 가지고 있을 경우
                if (uiControl.clickedItem.GetComponent<Shooter>())
                {
                    //슈터의 레퍼런스를 저장한다.
                    this.shooter = GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>();
                }

            }
            if (!shooter) return;
            // 슈터를 사용해서 미션을 진행한다.



            //setActive 다이어로그가 한번만 실행되게 함.
            if (shooter.extinguishedCount != 5 || shooter.extinguishedCount != 10)
            {
                isDialogActivated = false;
            }

            if (shooter.extinguishedCount == 5)
            {
                if (!isDialogActivated)
                {
                    dialog.text = missionDialog[3];
                    dialogPanel.SetActive(true);
                    isDialogActivated = true;
                }
            }
            if (shooter.extinguishedCount == 10)
            {
                if (!isDialogActivated)
                {
                    OnMissionSucceded();
                    dialog.text += "\n 모든 미션을 완료했어요! 이제 문을 찾아 나가세요!";
                    isDialogActivated = true;
                    

                }
            }

        }
    }



}




