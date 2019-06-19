using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUIControl : MonoBehaviour
{

    //인게임 UI를 총괄하는 스크립트
    //패널은 비활성화, 그 자식들은 활성화되있어야함.


    private InGameUIControl() { }
    private static InGameUIControl instance;
    public GameObject selectionPanel, itemSelectionPanel, dialogPanel, infoPanel, SimpleMassagePanel, elevatorPanel, VideoWindow, slot1, slot2, gameCompletedPanel;

    private Dictionary<string, GameObject> items;

    public Text dialog, consol, info, simpleMassage;

    public AudioClip clickSound;









    [HideInInspector] public GameObject clickedItem; //아이템일 수도, 슬롯일 수도 있다.

    List<Slot> slots;



    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public static InGameUIControl GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType(typeof(InGameUIControl)) as InGameUIControl;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }
        return instance;
    }



    // Start is called before the first frame update
    void Start()
    {
        items = ItemManager.items;
        //인벤토리 초기화
        slots = new List<Slot>();
        slots.Add(this.slot1.GetComponent<Slot>());
        slots.Add(this.slot2.GetComponent<Slot>());
        //UI레이캐스트 초기화
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();


    }

    Touch tempTouches;
    // Update is called once per frame



    void Update()
    {

#if UNITY_EDITOR
        if (!Input.GetMouseButtonDown(0)) { return; }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //아이템을 클릭했을 때
            if (hit.collider.tag == "item")
            {
                SoundManger.instance.PlaySingle(clickSound);
                Debug.Log("ActivateSelectionPanel");
                selectionPanel.SetActive(true);
                clickedItem = hit.collider.gameObject; //클릭된 아이템을 전역변수에 넣어준다.
                Debug.Log("클릭아이템 이름: " + clickedItem.name);

                //클릭된 게임 오브젝트트를 검사, 아이템의 정보를 인포패널에 적어둠
                //인스턴시에이트를 AR코어가 자동적으로 해주기 때문에 게임오브젝트 형식으로 비교할 수 없음. 어쩔 수 없이 이름으로 비교(프리팹과 인스턴스의 차이)

                foreach (var item in items)
                {
                    if (clickedItem.name == "extinghisher")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";
                       
                    }
                    else if (clickedItem.name == "alarm")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";

                    }
                    else if(clickedItem.name == "towel")
                    {
                        info.text = "젖은 수건이다. \n가스로부터 호흡기를 보호할 수 있을 것 같다.";

                    }
                    else if(clickedItem.name == "oilStove")
                    {
                        info.text = "석유난로다.\n 불이 났는데.. 석유는 위험하지 않을까?";

                    }
                    else if(clickedItem.name == "gasValve")
                    {
                        info.text = "가스밸브다.\n 밸브가 열려있다.";

                    }
                    else if(clickedItem.name == "multitap")
                    {
                        info.text = "멀티탭이다.\n그대로 내버려둬도 괜찮을까?";

                    }
                    else if(clickedItem.name == "elevator")
                    {
                        info.text = "엘리베이터다.\n 아래층으로 빠르게 내려갈 수 있을 것 같다.";
                       
                    }
                    else if(clickedItem.name == "cat")
                    {
                        info.text = "고양이다.\n 열기와 가스 때문에 괴로워하고 있다. 구해주자.";

                    }
                    else if(clickedItem.name == "waterBucket")
                    {
                        info.text = "물양동이다.\n 물을 뿌려 불을 끌 수 있을 것 같다.";

                    }
                    else if(clickedItem.name == "sand")
                    {
                        info.text = "고양이 화장실이다.\n 모래로 불을 끌 수 있다고 들은 것 같은데..";

                    }
                    else if (clickedItem.name == "door")
                    {
                        info.text = "문이다.\n얼른 열고 밖으로 나가자.";

                    }


                }


            }
        }
#else

        if (Input.touchCount <= 0) { return; }


        for (int i = 0; i < Input.touchCount; i++)
        {
            tempTouches = Input.GetTouch(0);
        }


        Ray touchRay = Camera.main.ScreenPointToRay(tempTouches.position);
        RaycastHit hit;


        if (Physics.Raycast(touchRay, out hit))
        {

            //아이템을 클릭했을 때
            if (hit.collider.tag == "item")
            {
                SoundManger.instance.PlaySingle(clickSound);
                Debug.Log("ActivateSelectionPanel");
                selectionPanel.SetActive(true);
                clickedItem = hit.collider.gameObject; //클릭된 아이템을 전역변수에 넣어준다.
                Debug.Log("클릭아이템 이름: " + clickedItem.name);

                //클릭된 게임 오브젝트트를 검사, 아이템의 정보를 인포패널에 적어둠
                //인스턴시에이트를 AR코어가 자동적으로 해주기 때문에 게임오브젝트 형식으로 비교할 수 없음. 어쩔 수 없이 이름으로 비교(프리팹과 인스턴스의 차이)
                foreach (var item in items)
                {
                    if (clickedItem.name == "extinghisher")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";
                       
                    }
                    else if (clickedItem.name == "alarm")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";

                    }
                    else if(clickedItem.name == "towel")
                    {
                        info.text = "젖은 수건이다. \n가스로부터 호흡기를 보호할 수 있을 것 같다.";

                    }
                    else if(clickedItem.name == "oilStove")
                    {
                        info.text = "석유난로다.\n 불이 났는데.. 석유는 위험하지 않을까?";

                    }
                    else if(clickedItem.name == "gasValve")
                    {
                        info.text = "가스밸브다.\n 밸브가 열려있다.";

                    }
                    else if(clickedItem.name == "multitap")
                    {
                        info.text = "멀티탭이다.\n그대로 내버려둬도 괜찮을까?";

                    }
                    else if(clickedItem.name == "elevator")
                    {
                        info.text = "엘리베이터다.\n 아래층으로 빠르게 내려갈 수 있을 것 같다.";
                       
                    }
                    else if(clickedItem.name == "cat")
                    {
                        info.text = "고양이다.\n 열기와 가스 때문에 괴로워하고 있다. 구해주자.";

                    }
                    else if(clickedItem.name == "waterBucket")
                    {
                        info.text = "물양동이다.\n 물을 뿌려 불을 끌 수 있을 것 같다.";

                    }
                    else if(clickedItem.name == "sand")
                    {
                        info.text = "고양이 화장실이다.\n 모래로 불을 끌 수 있다고 들은 것 같은데..";

                    }
                    else if (clickedItem.name == "door")
                    {
                        info.text = "문이다.\n얼른 열고 밖으로 나가자.";

                    }

                }

            }


        }
#endif


        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        // m_PointerEventData.position = Input.mousePosition;
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            SoundManger.instance.PlaySingle(clickSound);
            Debug.Log("클릿된 슬롯이름" + result.gameObject.name);
            //클릭된 UI가 슬롯이면
            if (result.gameObject.tag == "slot" && result.gameObject.GetComponent<Image>().sprite != null)
            {
                itemSelectionPanel.SetActive(true);
                clickedItem = result.gameObject; //클릭된 슬롯을 전역변수로 넘겨준다.


                //클릭된 슬롯의 이미지를 검사해 케이스마다 아이템 정보창의 컨텐츠를 셋팅한다.

                foreach (var item in items)
                {
                    if (result.gameObject.GetComponent<Image>().sprite.name.Replace("Img", "") == item.Key)
                        info.text = item.Key + "(이)다";

                    if (clickedItem.name == "extinghisher")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";

                    }
                    else if (clickedItem.name == "alarm")
                    {
                        info.text = "소화기다.\n바람을 등지고 빗자루로 쓸듯이 소화액을 분사하자";

                    }
                    else if (clickedItem.name == "towel")
                    {
                        info.text = "젖은 수건이다. \n가스로부터 호흡기를 보호할 수 있을 것 같다.";

                    }
                    else if (clickedItem.name == "oilStove")
                    {
                        info.text = "석유난로다.\n 불이 났는데.. 석유는 위험하지 않을까?";

                    }
                    else if (clickedItem.name == "gasValve")
                    {
                        info.text = "가스밸브다.\n 밸브가 열려있다.";

                    }
                    else if (clickedItem.name == "multiTap")
                    {
                        info.text = "멀티탭이다.\n그대로 내버려둬도 괜찮을까?";

                    }
                    else if (clickedItem.name == "elevator")
                    {
                        info.text = "엘리베이터다.\n 아래층으로 빠르게 내려갈 수 있을 것 같다.";

                    }
                    else if (clickedItem.name == "cat")
                    {
                        info.text = "고양이다.\n 열기와 가스 때문에 괴로워하고 있다. 구해주자.";

                    }
                    else if (clickedItem.name == "waterBucket")
                    {
                        info.text = "물양동이다.\n 물을 뿌려 불을 끌 수 있을 것 같다.";

                    }
                    else if (clickedItem.name == "sand")
                    {
                        info.text = "고양이 화장실이다.\n 모래로 불을 끌 수 있다고 들은 것 같은데..";

                    }
                    else if (clickedItem.name == "door")
                    {
                        info.text = "문이다.\n얼른 열고 밖으로 나가자.";

                    }
                }

            }
        }







    }

    // 정보보기 버튼을 누르면 infoPanel 띄우기. 정보보기 버튼에 연결해야함.
    public void ActiveInfoPanel()
    {
        Debug.Log("ActiveInfoPanel");
        infoPanel.SetActive(true);

    }

    //미션 컴플리트 판넬에 유저정보 띄우기
    void SetValueToCompleted()
    {
        GameManager gm = GameManager.GetInstance();
        int score = (int)(gm.time);
        gameCompletedPanel.transform.Find("UserName").GetComponent<Text>().text = "이재상";
        gameCompletedPanel.transform.Find("LastTime").GetComponent<Text>().text =  score.ToString() + "초";
        
        foreach(var a in Mission.succedMissionNames)
        {
            gameCompletedPanel.transform.Find("SuccessMission").GetComponent<Text>().text += a + ", ";
        }

        foreach (var a in Mission.failedMissionNames)
        {
            gameCompletedPanel.transform.Find("FailedMission").GetComponent<Text>().text += a + ", ";
        }

        gameCompletedPanel.transform.Find("Score").GetComponent<Text>().text = Mission.score.ToString();

        gameCompletedPanel.transform.Find("Grade").GetComponent<Text>().text = new Ranking().CalcRank(Mission.score);


    }


    public void Pick()
    {
        Debug.Log("집을 아이템 이름 = " + clickedItem.name);


        if (clickedItem.GetComponent<Item>().itemType != Item.Type.Equipment) { dialog.text = "습득할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; }

        if (clickedItem.tag == "item" && clickedItem.GetComponent<Item>().itemType == Item.Type.Equipment)
        {
            Renderer[] renderers = clickedItem.transform.GetComponentsInChildren<Renderer>();
            Collider[] cols = clickedItem.transform.GetComponentsInChildren<Collider>();

            foreach (var ren in renderers)
            {
                ren.enabled = false;
            }
            foreach (var col in cols)
            {
                col.enabled = false;
            }

        }


        for (int i = 0; i < slots.Count; i++)
        {
            //Slot스크립트의 isEmpty를 받아온다.(객체의 상태에 접근해 참조)
            if (slots[i].isEmpty == true) //슬롯이 비어있고, 아이템이 장착할 수 있는 타입이면
            {
                Debug.Log("슬롯이 비어있음");
                slots[i].Additem(GetItemImg(clickedItem));
                break; //하나의 슬롯만 채우도록 함
            }
            else
            {
                Debug.Log(i + "번째 슬롯이 꽉참");
            }

        }
    }


    Sprite GetItemImg(GameObject item)
    {
        Debug.Log("아이템이름:" + item.name);
        if (item.GetComponent<Item>().defaultImg == null) Debug.Log("아이템 이미지가 없습니다");

        return item.GetComponent<Item>().defaultImg;
    }


    public void DiscardSlot() //슬롯을 비운다
    {
        clickedItem.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        clickedItem.GetComponent<Image>().sprite = null;

    }


    //엘리베이터 사용. 엘리베이터선택패널의 예 버튼에 연결
    public void UseElevator()
    {
        SoundManger.instance.musicSource.volume = .1f;
        UnityEngine.Video.VideoPlayer videoPlayer; videoPlayer = GameObject.Find(items["elevator"].name).GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Play();
        VideoWindow.SetActive(true);
        videoPlayer.loopPointReached += VideoOver;

    }

    public void VideoOver(UnityEngine.Video.VideoPlayer vp)
    {

        vp.Stop();
        VideoWindow.SetActive(false);
        GameManager.GetInstance().hp.GetComponent<Slider>().value -= 10f;
        dialog.text = "화재시 엘리베이터를 사용하면 위험합니다!";
        dialogPanel.SetActive(true);

    }

    //아이템 사용버튼을 누르면 실행
    public void UseItem()
    {

        //아이템 타입이 Misc이면 
        Debug.Log("사용하는 아이템:" + clickedItem.name);
        if (clickedItem.GetComponent<Item>() != null && clickedItem.GetComponent<Item>().itemType == Item.Type.Misc) { dialog.text = "사용할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; }



        //equipment형 아이템을 숨긴다
        if (clickedItem.tag == "item" && clickedItem.GetComponent<Item>().itemType == Item.Type.Equipment && clickedItem.name != "cat")
        {
            Renderer[] renderers = clickedItem.transform.GetComponentsInChildren<Renderer>();
            Collider[] cols = clickedItem.transform.GetComponentsInChildren<Collider>();

            foreach (var ren in renderers)
            {
                ren.enabled = false;
            }
            foreach (var col in cols)
            {
                col.enabled = false;
            }

        }

        if (clickedItem.tag == "slot" && clickedItem.GetComponent<Image>().sprite.name != "cat")
        {
            DiscardSlot();
        }
        //아이템을 습득하지 않고 바로 사용할 경우

        if (clickedItem != null)
        {
            if (clickedItem.name == items["extinguisher"].name)
            {
                Debug.Log("소화기를 사용합니다");
                GameObject extinguisher = GameObject.Find(items["extinguisher"].name);
                extinguisher.GetComponent<Shooter>().ActivateShooter();

            }
            if (clickedItem.name == items["alarm"].name)
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find(items["alarm"].name);
                if (alarm.GetComponent<AudioSource>().isPlaying) { alarm.GetComponent<AudioSource>().Stop(); return; }
                alarm.GetComponent<AudioSource>().Play();



            }
            if (clickedItem.name == items["sand"].name)
            {
                Debug.Log("모래를 사용합니다");
                GameObject sand = GameObject.Find(items["sand"].name);
                sand.GetComponent<Shooter>().ActivateShooter();
            }
            if (clickedItem.name == items["towel"].name)
            {
                clickedItem.GetComponent<Item>().state = Item.Usage.USED;
                Destroy(clickedItem);


            }
            if (clickedItem.name == items["waterBucket"].name)
            {
                GameObject waterBucketFull = GameObject.Find(items["waterBucket"].name);
                waterBucketFull.GetComponent<Shooter>().ActivateShooter();

            }
            if (clickedItem.name == items["gasValve"].name)
            {
                //이미 사용됬으면 아무것도 안함
                if (clickedItem.GetComponent<Item>().state == Item.Usage.USED) return;

                Mission mission = GameManager.GetInstance().mission;
                //스타트미션에서 랜덤으로 붙은 불이 꺼졌을때
                if(GameManager.GetInstance().stm.ranObj != "towel" && Mission.missionName == "startMission")
                {
                    dialog.text = mission.missionDialog[0];
                    dialogPanel.SetActive(true);
                    mission.OnMissionSucceded();
                    Mission.isMissonSucced = true; Mission.isMissionOn = false;
                }

                clickedItem.GetComponent<Item>().state = Item.Usage.USED;
            }
            if (clickedItem.name == items["elevator"].name)
            {
                elevatorPanel.SetActive(true);

            }
            //게임 클리어 조건 체크
            if (clickedItem.name == items["door"].name)
            {
                if (Mission.ComplishedMissionCount >= 1)
                {   
                    if(slot1.GetComponent<Image>().sprite || slot2.GetComponent<Image>().sprite)
                    {
                        if (slot1.GetComponent<Image>().sprite.name == "cat" || slot2.GetComponent<Image>().sprite.name == "cat")
                        {
                            Mission.AddScore(1000);
                        }
                        
                    }
                    SetValueToCompleted();
                    gameCompletedPanel.SetActive(true);

                }
                else
                {
                    dialog.text = "아직 일러요! 더 많은 미션을 성공하고 오세요!";
                    dialogPanel.SetActive(true);
                }
            }
            if (clickedItem.name == items["cat"].name)
            {
                GameObject cat = GameObject.Find(items["cat"].name);
                cat.GetComponent<AudioSource>().Play(); //야옹

            }


        }
        //아이템을 습득하고나서 사용할 경우
        if (clickedItem != null && clickedItem.GetComponent<Image>() != null && clickedItem.GetComponent<Image>().sprite != null)
        {
            Debug.Log("슬롯 이미지 이름:" + clickedItem.GetComponent<Image>().sprite.name);
            //슬롯에 있는 이미지가 (아이템이름)이미지면
            if (clickedItem.GetComponent<Image>().sprite == GetItemImg(items["extinguisher"]))
            {
                Debug.Log("소화기를 사용합니다");
                //Shooter shooter = extinguisher.GetComponent<Shooter>(); 프리펩 자체에 접근했다. 게임상의 프리펩 인스턴스와는 다르기 때문에 실행이 안된다. 실행하려면 프리펩 인스턴스에 접근해야한다.
                GameObject extinguisher = GameObject.Find(items["extinguisher"].name);
                extinguisher.GetComponent<Shooter>().ActivateShooter();
            }
            else if (clickedItem.GetComponent<Image>().sprite == GetItemImg(items["alarm"]))
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find(items["alarm"].name);
                if (alarm.GetComponent<AudioSource>().isPlaying) { alarm.GetComponent<AudioSource>().Stop(); return; }
                alarm.GetComponent<AudioSource>().Play();
            }
            else if (clickedItem.GetComponent<Image>().sprite == GetItemImg(items["sand"]))
            {
                GameObject sand = GameObject.Find(items["sand"].name);
                sand.GetComponent<Shooter>().ActivateShooter();
            }
            else if (clickedItem.GetComponent<Image>().sprite == GetItemImg(items["towel"]))
            {
                //towel이라는 이름은 여러 오브젝트 인스턴스가 가지고있다. 따라서 겟콤포넌트 할때 주의(모델 임포트할때 자식이 딸려오는 모델임)
                GameObject towel = GameObject.Find(items["towel"].name);
                towel.GetComponent<Item>().state = Item.Usage.USED;
            }
            else if (clickedItem.GetComponent<Image>().sprite == GetItemImg(items["cat"]))
            {
                GameObject cat = GameObject.Find(items["cat"].name);
                cat.GetComponent<AudioSource>().Play();
            }
        }
    }
    private void CorrectAnswer()
    {
        QuizManager Qm = QuizManager.instance;
        AnswerScript answerScript = AnswerScript.instance;

        answerScript.correct.SetActive(false);
        Qm.QuizPanel.SetActive(false);
        Qm.Level = Qm.Level + 1;

        Mission.AddScore(500);

    }
    private void WrongAnswer()
    {
        QuizManager Qm = QuizManager.instance;
        AnswerScript answerScript = AnswerScript.instance;

        answerScript.wrong.SetActive(false);
        Qm.QuizPanel.SetActive(false);
        Qm.Level = Qm.Level + 1;
    }

    public void CheckAnswer(int a)
    {
        QuizManager Qm = QuizManager.instance;
        Debug.Log("Button CLick");
        AnswerScript answerScript = AnswerScript.instance;
        if (a == Qm.quizes[Qm.random].AnswerNumber)
        {
            //GameObject.Find("SoundCorrect").GetComponent<AudioSource>().Play();
            Debug.Log("O");
            answerScript.correct.SetActive(true);

            Invoke("CorrectAnswer", 2);
        }
        else
        {
            Debug.Log("X");
            // GameObject.Find("SoundWrong").GetComponent<AudioSource>().Play();
            answerScript.wrong.SetActive(true);

            Invoke("WrongAnswer", 2);
        }
    }
}
