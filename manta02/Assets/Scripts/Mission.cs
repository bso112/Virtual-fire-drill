using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Mission

{
    protected GameManager gm = GameManager.GetInstance();
    protected InGameUIControl uiControl = InGameUIControl.GetInstance();
    protected GameObject dialogPanel = InGameUIControl.GetInstance().dialogPanel;
    protected Text dialog = InGameUIControl.GetInstance().dialog;
    protected Text scoreText = InGameUIControl.GetInstance().scoreText;
    public int score;
    protected string missionName;
    protected List<string> missionDialog = new List<string>();

    [HideInInspector] public bool isMissionOn, isMissonSucced = false;

    public abstract void MissionEvent(float time); //인자로 미션의 제한시간을 받는다.

    public abstract IEnumerator MissionRoutine(float time);

    //인게임유아이컨트롤에서 체크하면 업데이트에서 체크하거나 전역불리언값을 하나더 만들어야하거나 미션객체가 싱글톤이어야하거나 하는 문제발생. 걍 여기서 체크한다.
    public void ScoreCheack()
    {
        if (isMissonSucced) { Debug.Log("점수" + score); score += int.Parse(scoreText.text); scoreText.text = score.ToString(); }
    }
}

public class StartMission : Mission
{

    public Shooter shooter;
    int ran = Random.Range(0, 2);
    GameObject gasValve, multiTap, oilStove, fire;
    GameObject[] startItems;



    public StartMission()
    {
        missionName = "스타트미션";
        score = 200;
        missionDialog.Add("성공이에요!");
        missionDialog.Add("실패에요!");
        missionDialog.Add("어디선가 불이 났습니다! 화재원을 찾아보세요!");
        dialog.text = missionDialog[2];
        dialogPanel.SetActive(true);
        //가스밸브, 멀티탭, 오일스토브 인스턴스를 가져온다.
        gasValve = GameObject.Find(uiControl.gasValve.name);
        multiTap = GameObject.Find(uiControl.multitap.name);
        oilStove = GameObject.Find(uiControl.oliStove.name);
        startItems = new GameObject[] { gasValve, multiTap, oilStove };
        //랜덤 오브젝트의 자식인 불 오브젝트를 가져와서 활성화시킨다.(처음에 비활성화 상태임)
        fire = startItems[ran].transform.Find("fire").gameObject;
        fire.SetActive(true);

        


    }    

    public override void MissionEvent(float time)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator MissionRoutine(float time)
    {

        float timeSnapshot;
        timeSnapshot = gm.time;

        while (isMissionOn)
        {

            Debug.Log("미션시작");
            Debug.Log("미션이 활성화 됬나?" + isMissionOn);

            if (gm.time > timeSnapshot + time)
            {

                Debug.Log("타임아웃");
                dialog.text = missionDialog[1];
                dialogPanel.SetActive(true);
                isMissonSucced = false;
                isMissionOn = false;
            }

            yield return null;

            if (!this.shooter)
            {   
                //클릭된 아이템이 슈터를 가지고 있을 경우
                if (uiControl.clickedItem.GetComponent<Shooter>())
                {   
                    //슈터의 레퍼런스를 저장한다.
                    this.shooter = GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>();
                }

                //만약 슈터가 붙은 게임오브젝트가 ~~면 식으로 상황을 전개해나간다.
            }

            //랜덤오브젝트에 따라, 또 사용하는 소화오브젝트에 따라 상황이 달라진다. 정리를 더 해보자.

            //랜덤 오브젝트가 가스 밸브이고 불이 꺼지면
            if (startItems[ran] == gasValve && fire.GetComponent<Fire>().isExtinguished)
            {
                missionDialog.Add("잘했어요! 이제 가스밸브를 잠가주세요"); dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
                //가스밸브가 잠기면 성공

            }
            //랜덤 오브젝트가 멀티탭이고 불이 꺼지면
            else if (startItems[ran] == multiTap && fire.GetComponent<Fire>().isExtinguished)
            {
                missionDialog.Add("물을 사용하면 감전됩니다!"); dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
            }
            //랜덤 오브젝트가 오일스토브고 물을 사용하면
            else if (startItems[ran] == oilStove && this.shooter.state == Shooter.Usage.USING)
            {
                missionDialog.Add("물을 사용하면 불이 더 커집니다!"); dialog.text = missionDialog[3]; dialogPanel.SetActive(true);
            }

            //성공 실패 로직(점수획득, ismissionOn을 false로 만들기)
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
    public override void MissionEvent(float time)
    {
    }





    //코루틴으로 안쓰고 while로 하면 무한루프 걸려버림.. 코루틴으로써도 넘 자주 실행되는데
    public override IEnumerator MissionRoutine(float time) //인자로 미션제한시간을 받는다.
    {

        float timeSnapshot;
        timeSnapshot = gm.time;
        while (isMissionOn)
        {

            Debug.Log("미션시작");
            Debug.Log("미션이 활성화 됬나?" + isMissionOn);

            if (gm.time > timeSnapshot + time)
            {

                Debug.Log("타임아웃");
                dialog.text = missionDialog[1];
                dialogPanel.SetActive(true);
                isMissonSucced = false;
                isMissionOn = false;
            }
            //사용자가 소화아이템을 사용하는지 프레임마다 체크
            yield return null;

            if (!this.shooter)
            {
               
                //클릭된 아이템이 슈터를 가지고 있을 경우
                if (uiControl.clickedItem.GetComponent<Shooter>())
                {
                    //슈터의 레퍼런스를 저장한다.
                    this.shooter = GameObject.Find(uiControl.clickedItem.name).GetComponent<Shooter>();
                }
                continue; //슈터에 객체가 할당이 안되면 건너뛴다. 
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

