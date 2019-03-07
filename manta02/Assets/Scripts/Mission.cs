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

    public abstract void MissionEvent(float Min); //인자로 미션의 제한시간을 받는다.

    public abstract IEnumerator MissionRoutine(float Min);
    
    //인게임유아이컨트롤에서 체크하면 업데이트에서 체크하거나 전역불리언값을 하나더 만들어야하거나 미션객체가 싱글톤이어야하거나 하는 문제발생. 걍 여기서 체크한다.
    public void ScoreCheack()
    {
        if (isMissonSucced) { Debug.Log("점수" + score); score += int.Parse(scoreText.text); scoreText.text = score.ToString(); }
    }
}

public class StartMission : Mission
{

    public Shooter shooter;

    public StartMission()
    {
        missionName = "스타트미션";
        score = 200;
        missionDialog.Add("어디선가 불이 났습니다! 화재원을 찾아보세요!");

    }    

    public override void MissionEvent(float Min)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator MissionRoutine(float Min)
    {
        throw new System.NotImplementedException();
    }
}

public class GasMission : Mission
{
    public GasMission()
    {

        missionName = "유독가스 미션";
        score = 50;
        //미션 다이어로그의 0은 성공메세지, 1은 실패메세지다. 본 메세지는 2부터 시작한다.
        missionDialog.Add("미션 성공이에요!");
        missionDialog.Add("미션 실패예요!");
        missionDialog.Add("물에 젖은 수건을 찾아보세요!!");
        missionDialog.Add("물수건 덕분에 유독가스를 막을 수 있어요!");
        dialog.text = missionDialog[2];

    }
    public override void MissionEvent(float LimitTime)
    {
    }

    public override IEnumerator MissionRoutine(float LimitTime)
    {

        Debug.Log("꺌룽랭1");
        float MinSnapshot;
        MinSnapshot = gm.Min*60 + gm.Second;
        while (isMissionOn)
        {

            Debug.Log("미션시작");
            Debug.Log("미션이 활성화 됐나?" + isMissionOn);


            if (gm.Min * 60 + gm.Second < MinSnapshot - LimitTime)
            {

                Debug.Log("타임아웃");
                dialog.text = missionDialog[1];
                dialogPanel.SetActive(true);
                isMissonSucced = false;
                isMissionOn = false;
            }

            yield return null;
            if(gm.Min * 60 + gm.Second < gm.PlayTime/2)
            {
                Debug.Log("꺌룽랭2");
                while (gm.currentHP >= 0f)
                {
                    Debug.Log("꺌룽랭3");
                    gm.currentHP -= 1f;
                    yield return new WaitForSeconds(1f);
                    if(uiControl.clickedItem.name == uiControl.towel.name)
                    {
                        break;
                        Debug.Log("꺌룽랭4");
                    }
                }
                dialog.text = missionDialog[0];
                dialogPanel.SetActive(true);
                isMissonSucced = true;
                isMissionOn = false;
                ScoreCheack();
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
    public override void MissionEvent(float LimitTime)
    {
    }

    
    //코루틴으로 안쓰고 while로 하면 무한루프 걸려버림
    public override IEnumerator MissionRoutine(float LimitTime) //인자로 미션제한시간을 받는다.
    {

        float MinSnapshot;
        MinSnapshot = gm.Min;
        while (isMissionOn)
        {

            Debug.Log("미션시작");
            Debug.Log("미션이 활성화 됐나?" + isMissionOn);

            if (gm.Min < MinSnapshot - LimitTime)
            {

                Debug.Log("타임아웃");
                dialog.text = missionDialog[1];
                dialogPanel.SetActive(true);
                isMissonSucced = false;
                isMissionOn = false;
            }

            yield return null;

            //사용자가 소화기나 모래를 사용하기를 계속 기다린다. 
            if (!this.shooter)
            {
                // 사용하면 슈터를 가져온다.
                if (uiControl.clickedItem.name == uiControl.extinguisher.name)
                {
                    this.shooter = GameObject.Find(uiControl.extinguisher.name).GetComponent<Shooter>();
                }
                else if (uiControl.clickedItem.name == uiControl.sand.name)
                {
                    this.shooter = GameObject.Find(uiControl.sand.name).GetComponent<Shooter>();
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

