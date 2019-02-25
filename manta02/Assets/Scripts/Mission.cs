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
    protected Text scoreText = GameManager.GetInstance().score;
    protected GameObject hp = GameManager.GetInstance().hp;
    public int score;
    protected string missionName;
    public List<string> missionDialog = new List<string>();

    [HideInInspector] public static bool isMissionOn, isMissonSucced = false;

    public float timeSnapshot;

    public Mission()
    {
        Debug.Log("미션생성자 실행");
        timeSnapshot = gm.min * 60 + gm.second;
    }
    public abstract void MissionEvent();

    //성공로직 마지막에 실행되어야한다.
    public void ScoreCheack()
    {
        Debug.Log("스코어체크");
        if (isMissonSucced) { Debug.Log("점수" + score); score += int.Parse(scoreText.text); scoreText.text = score.ToString(); }
    }
}

public class TestMission : Mission
{
    public TestMission()
    {
        missionDialog.Add("(테스트)성공입니당~");
        missionDialog.Add("(테스트)실패입니당~");
    }
    public override void MissionEvent()
    {
        if (isMissionOn) Debug.Log("테스트 미션");
    }
}

public class StartMission : Mission
{

    public Shooter shooter;
    static int ran = Random.Range(0, 3);
    private GameObject gasValve, multiTap, oilStove;
    public static GameObject fire;
    GameObject[] startItems;
    static bool isDialogActivated;



    public StartMission()
    {
        missionName = "스타트미션";
        score = 50;
        missionDialog.Add("성공이에요!");
        missionDialog.Add("실패에요!");
        missionDialog.Add("어디선가 불이 났습니다! 화재원을 찾아보세요!");
        missionDialog.Add("");
        if(dialog.text != missionDialog[2]) { dialog.text = missionDialog[2]; dialogPanel.SetActive(true); }
        isDialogActivated = true;


        //가스밸브, 멀티탭, 오일스토브 인스턴스를 가져온다.
        gasValve = GameObject.Find(uiControl.gasValve.name);
        multiTap = GameObject.Find(uiControl.multitap.name);
        oilStove = GameObject.Find(uiControl.oliStove.name);
        startItems = new GameObject[] { gasValve, multiTap, oilStove };
        //랜덤 오브젝트의 자식인 불 오브젝트가 있으면
        if (startItems[ran].transform.Find("fire"))
        {
            fire = startItems[ran].transform.Find("fire").gameObject;
            fire.SetActive(true);
        }




    }


    //미션이 언제 실행되는지 업데이트에서 조건을 확인하지 않고, 조건을 하드코딩하거나 버튼 클릭등의 이벤트로 구체적으로 제공한다.
    public override void MissionEvent()
    {   
        //매 클릭마다 실행
        if (!Input.GetMouseButtonDown(0)) { return; }
        //isMissionOn이 true면
        if (isMissionOn)
        {
            //슈터가 없고 클릭아이템이 있으면
            if (!shooter && uiControl.clickedItem)
            {
                //슈터를 할당
                this.shooter = GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>();
            }
            if (!shooter) { return; }


            //세가지 오브젝트에 랜덤으로 붙은 불을 클릭하면(불을 끄는 시점과 불을 클릭하는 시점이 달라 문제가 생겼었다.)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && hit.collider.gameObject == fire)
            {
                Debug.Log("레이캐스트된 객체:" + hit.collider.name);
                Debug.Log("불이꺼짐");

                //가스밸브는 불을 끈다음 밸브를 잠궈야(밸브-사용하기버튼 눌러야) 성공이다.
                if (startItems[ran] == gasValve)
                {
                    if (missionDialog[3] != "잘했어요!이제 가스밸브를 잠가주세요")
                        missionDialog[3] = "잘했어요! 이제 가스밸브를 잠가주세요"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    //밸브를 돌리면 성공(동기화 문제 때문에 InGameUiControl의 useitem에 구현함)

                }
                else if (startItems[ran] == multiTap && shooter.effect.name == "waterEffect")
                {
                    if (missionDialog[3] != "불은 껐지만 감전되어 체력이 감소했다!")
                        missionDialog[3] = "불은 껐지만 감전되어 체력이 감소했다!"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    hp.GetComponent<Slider>().value -= 10f;
                    isMissonSucced = true; isMissionOn = false;
                    ScoreCheack();
                }
                else if (startItems[ran] == oilStove && shooter.effect.name == "waterEffect")
                {
                    if (missionDialog[3] != "물을 사용하면 불이 더 커집니다!")
                        missionDialog[3] = "물을 사용하면 불이 더 커집니다!"; dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                    //불이 커진다.
                    ParticleSystem ps = fire.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startSize = 2.5f;
                }
                else if (fire == null)
                {
                    dialog.text = missionDialog[0];
                    dialogPanel.SetActive(true);
                    ScoreCheack();
                    isMissonSucced = true; isMissionOn = false;
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

        missionName = "불끄기미션";
        score = 100;
        //미션 다이어로그의 0은 성공메세지, 1은 실패메세지다. 본 메세지는 2부터 시작한다.
        missionDialog.Add("성공이에요!");
        missionDialog.Add("실패에요!");
        missionDialog.Add("불을 끄세요!");
        missionDialog.Add("잘했어요 조금 더 꺼볼까요?");
        dialog.text = missionDialog[2];


    }

    bool isDialogActivated = false; // 다이어로그 패널 active를 한번만 실행하기 위해 




    public override void MissionEvent()
    {

        float timeSnapshot;
        timeSnapshot = gm.second;
        while (isMissionOn)
        {

            Debug.Log("미션시작");
            Debug.Log("미션이 활성화 됬나?" + isMissionOn);

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

            // 슈터를 사용해서 미션을 진행한다.

            Debug.Log("끈 불의 수:" + shooter.extinguishedCount);

            //setActive 다이어로그가 한번만 실행되게 함.
            if (shooter.extinguishedCount != 5 || shooter.extinguishedCount != 10)
            {
                isDialogActivated = false;
            }

            if (shooter.extinguishedCount == 5)
            {
                if (!isDialogActivated)
                {
                    Debug.Log("다섯개의 불을 끔");
                    dialog.text = missionDialog[3];
                    dialogPanel.SetActive(true);
                    isDialogActivated = true;
                }
            }
            if (shooter.extinguishedCount == 10)
            {
                if (!isDialogActivated)
                {
                    Debug.Log("열개의 불을 끔");
                    dialog.text = missionDialog[0];
                    dialogPanel.SetActive(true);
                    isMissonSucced = true;
                    isMissionOn = false;
                    isDialogActivated = true;
                    ScoreCheack();

                }
            }

        }
    }



}




