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
    public GameObject selectionPanel, itemSelectionPanel, dialogPanel, infoPanel, SimpleMassagePanel, MissonPanel, VideoWindow, slot1, slot2,
        extinguisher, sand, alarm, multitap, gasValve, towel, oliStove, waterBucketFull, WaterBucket, elevator,
        extinguisherMisson;

    public Text dialog, consol, info, simpleMassage;


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

    //터치 Touch tempTouches;
    // Update is called once per frame
    void Update()
    {
        //게임오브젝트에 광선을 쏴서 식별한다.(매 프레임마다 마우스클릭이 있는지 검사한다)
        if (!Input.GetMouseButtonDown(0)) { return; }
        //터치 if(Input.touchCount<=0) { return; }
        //이 밑부터는 마우스 클릭했을때 한번만 실행한다.

        // 터치 for (int i = 0; i < Input.touchCount; i++)
        //{
        //    tempTouches = Input.GetTouch(0);
        //}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //메인카메라 태그 설정해야 nullponit exception 안난다.
        //터치 Ray touchRay = Camera.main.ScreenPointToRay(tempTouches.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        //터치 if(Physics.Raycast(touchRay, out hit))
        {
            //인스펙터에서 각 아이템과 아이템이미지에 콜라이더가 있는지, 태그가 잘 되있는지, 이름이 잘 되있는지 확인!

            //아이템을 클릭했을 때
            if (hit.collider.tag == "item")
            {
                Debug.Log("ActivateSelectionPanel");
                selectionPanel.SetActive(true);
                clickedItem = hit.collider.gameObject; //클릭된 아이템을 전역변수에 넣어준다.

                //클릭된 게임 오브젝트트를 검사, 아이템의 정보를 인포패널에 적어둠
                //인스턴시에이트를 AR코어가 자동적으로 해주기 때문에 게임오브젝트 형식으로 비교할 수 없음. 어쩔 수 없이 이름으로 비교(프리팹과 인스턴스의 차이)
                if (clickedItem.name == extinguisher.name)
                {
                    info.text = "소화기다.";
                }
                else if (clickedItem.name == sand.name)
                {
                    info.text = "모래다.";
                }
                else if (clickedItem.name == alarm.name)
                {
                    info.text = "비상벨이다";
                }
                else if (clickedItem.name == multitap.name)
                {
                    info.text = "멀티탭이다";
                }
                else if (clickedItem.name == gasValve.name)
                {
                    info.text = "가스밸브다";
                }
                else if (clickedItem.name == oliStove.name)
                {
                    info.text = "오일스토브다.";
                }
                else if (clickedItem.name == towel.name)
                {
                    info.text = "수건이다.";
                }

            }
            //미션태그를 가진 오브젝트면
            if (hit.collider.tag == "mission")
            {
                Debug.Log("ActivateMissonPanel");
                clickedItem = hit.collider.gameObject;
                MissonPanel.SetActive(true);




            }

        }


        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        // m_PointerEventData.position = Input.mousePosition;
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            //클릭된 UI가 슬롯이면
            if (result.gameObject.tag == "slot" && result.gameObject.GetComponent<Image>().sprite != null)
            {
                Debug.Log("ActiveitemSelectioPanel");
                itemSelectionPanel.SetActive(true); //아니면 아이템셀렉션패널을 연다
                clickedItem = result.gameObject; //클릭된 UI를 전역변수로 넘겨준다.

                //클릭된 슬롯의 이미지를 검사해 케이스마다 아이템 정보창의 컨텐츠를 셋팅한다.
                if (result.gameObject.GetComponent<Image>().sprite == GetItemImg(extinguisher))
                {
                    info.text = "소화기다.";
                }
                else if (result.gameObject.GetComponent<Image>().sprite == GetItemImg(sand))
                {
                    info.text = "모래다.";
                }
                else if (result.gameObject.GetComponent<Image>().sprite == GetItemImg(towel))
                {
                    info.text = "수건이다.";
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


    public void Pick()
    {
        Debug.Log("클릭된 아이템 이름 = " + clickedItem.name);


        if (clickedItem.GetComponent<Item>().itemType != Item.Type.Equipment) { dialog.text = "습득할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; }
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
        if (item.GetComponent<Item>().defaultImg == null) Debug.Log("아이템 이미지가 없습니다");

        return item.GetComponent<Item>().defaultImg;
    }


    public void DiscardSlot() //슬롯을 비운다
    {
        if (clickedItem != null && clickedItem.GetComponent<Image>() != null && clickedItem.GetComponent<Image>().sprite != null)
        {
            clickedItem.GetComponent<Image>().color = new Color32(0, 0, 0, 150);
            clickedItem.GetComponent<Image>().sprite = null;
        }
    }


    //아이템 사용버튼을 누르면 실행
    public void UseItem()
    {
        //아이템 타입이 Misc이면 
        Debug.Log("사용하는 아이템:" + clickedItem.name);
        if (clickedItem.GetComponent<Item>() != null && clickedItem.GetComponent<Item>().itemType == Item.Type.Misc) { dialog.text = "사용할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; }
        clickedItem.GetComponent<Item>().state = Item.Usage.USING;
        //아이템을 습득하지 않고 바로 사용할 경우
        if (clickedItem != null)
        {
            if (clickedItem.name == extinguisher.name)
            {
                Debug.Log("소화기를 사용합니다");
                GameObject extinguisher = GameObject.Find(this.extinguisher.name);
                extinguisher.GetComponent<Shooter>().ActivateShooter();

            }
            if (clickedItem.name == alarm.name)
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find(this.alarm.name);
                alarm.GetComponent<AudioSource>().Play();
            }
            if (clickedItem.name == sand.name)
            {
                Debug.Log("모래를 사용합니다");
                GameObject sand = GameObject.Find(this.sand.name);
                sand.GetComponent<Shooter>().ActivateShooter();
            }
            if (clickedItem.name == towel.name)
            {
                Debug.Log("타올을 사용합니다.");
                Debug.Log("아직 구현이 안되었습니다.");
            }
            if (clickedItem.name == waterBucketFull.name)
            {
                GameObject waterBucketFull = GameObject.Find(this.waterBucketFull.name);
                waterBucketFull.GetComponent<Shooter>().ActivateShooter();

            }
            if (clickedItem.name == gasValve.name)
            {
                Debug.Log("가스밸브를 사용합니다");


                IEnumerator SmoothRotation(GameObject obj, Quaternion target, float speed)
                {
                    float time = 0;
                    time += Time.deltaTime;
                    //2초간
                    while (time < 2)
                    {
                        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, target, speed);
                        yield return null;
                    }

                }
                StartCoroutine(SmoothRotation(clickedItem.transform.Find("handle").gameObject, Quaternion.Euler(0, 0, 40), 0.5f));
                clickedItem.GetComponent<Item>().state = Item.Usage.USED;


                Mission mission = GameManager.GetInstance().mission;
                //스타트미션에서 랜덤으로 붙은 불이 꺼졌을때
                if (StartMission.fire == null)
                {
                    dialog.text = mission.missionDialog[0];
                    dialogPanel.SetActive(true);
                    mission.ScoreCheack();
                    Mission.isMissonSucced = true; Mission.isMissionOn = false;
                }
                else //미션이 진행 중이 아닌데 가스밸브를 잠갔을 경우
                {
                    dialog.text = "잘했어요! 가스밸브는 까먹지 말고 잠가야해요!";
                    dialogPanel.SetActive(true);
                    GameManager.GetInstance().score.text = (int.Parse(GameManager.GetInstance().score.text) + 20).ToString();

                }
            }
            if (clickedItem.name == elevator.name)
            {
                
                elevator.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                VideoWindow.SetActive(true);
                elevator.GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += VideoOver;
          
                void VideoOver(UnityEngine.Video.VideoPlayer vp)
                {
                    GameManager.GetInstance().hp.GetComponent<Slider>().value -= 10f;
                    dialog.text = "화재시 엘리베이터를 사용하면 위험합니다!";
                    dialogPanel.SetActive(true);
                    VideoWindow.SetActive(false);
                }


            }


        }
        //아이템을 습득하고나서 사용할 경우
        if (clickedItem != null && clickedItem.GetComponent<Image>() != null && clickedItem.GetComponent<Image>().sprite != null)
        {
            Debug.Log("슬롯 이미지 이름:" + clickedItem.GetComponent<Image>().sprite.name);
            //슬롯에 있는 이미지가 (아이템이름)이미지면
            if (clickedItem.GetComponent<Image>().sprite == GetItemImg(extinguisher))
            {
                Debug.Log("소화기를 사용합니다");
                //Shooter shooter = extinguisher.GetComponent<Shooter>(); 프리펩 자체에 접근했다. 게임상의 프리펩 인스턴스와는 다르기 때문에 실행이 안된다. 실행하려면 프리펩 인스턴스에 접근해야한다.
                GameObject extinguisher = GameObject.Find(this.extinguisher.name);
                extinguisher.GetComponent<Shooter>().ActivateShooter();
            }
            if (clickedItem.GetComponent<Image>().sprite == GetItemImg(alarm))
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find(this.alarm.name);
                alarm.GetComponent<AudioSource>().Play();
            }
            if (clickedItem.GetComponent<Image>().sprite == GetItemImg(sand))
            {
                GameObject sand = GameObject.Find(this.sand.name);
                sand.GetComponent<Shooter>().ActivateShooter();
            }
            if (clickedItem.GetComponent<Image>().sprite == GetItemImg(towel))
            {
                //towel이라는 이름은 여러 오브젝트 인스턴스가 가지고있다. 따라서 겟콤포넌트 할때 주의(모델 임포트할때 자식이 딸려오는 모델임)
                GameObject towel = GameObject.Find(this.towel.name);

            }
        }
    }
}
