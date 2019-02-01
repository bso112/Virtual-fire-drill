using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUIControl : MonoBehaviour
{

    //인게임 UI를 총괄하는 스크립트

    private InGameUIControl(){}
    private static InGameUIControl instance;

    public GameObject selectionPanel, itemSelectionPanel, dialogPanel, infoPanel, slot1, slot2, 
        extinguisher, sand, alarm, multitap, gasnozzle, towel;
   
    public Text dialog, consol, info;
    public Sprite extinguisherImg, sandImg, alarmImg, multitapImg, towelImg;
    bool isMissonEnd, isGameEnd;

    [HideInInspector] public GameObject clickedItem;

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
        //게임이 시작되면
        dialog.text = "어디선가 불이 났습니다! 원인을 찾아보세요.";
        dialogPanel.SetActive(true);
        isMissonEnd = isGameEnd = false;
        slots = new List<Slot>();
        slots.Add(this.slot1.GetComponent<Slot>());
        slots.Add(this.slot2.GetComponent<Slot>());

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

    }

    // Update is called once per frame
    void Update()
    {   
        //게임오브젝트에 광선을 쏴서 식별한다.(매 프레임마다 마우스클릭이 있는지 검사한다)
        if (!Input.GetMouseButtonDown(0))
        { return; }
        //이 밑부터는 마우스 클릭했을때 한번만 실행한다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //메인카메라 태그 설정해야 nullponit exception 안난다.
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {   
            //인스펙터에서 각 아이템과 아이템이미지에 콜라이더가 있는지, 태그가 잘 되있는지, 이름이 잘 되있는지 확인!

            //아이템을 클릭했을 때
            if(hit.collider.tag == "item")
            {
                Debug.Log("ActivateSelectionPanel");
                selectionPanel.SetActive(true);
                clickedItem = hit.collider.gameObject; //클릭된 아이템을 전역변수에 넣어준다.

                //클릭된 게임 오브젝트트를 검사, 아이템의 정보를 인포패널에 적어둠

                if(clickedItem.name == extinguisher.name)
                {
                    info.text = "소화기다.";
                }
                else if(clickedItem.name == sand.name)
                {
                    info.text = "모래다.";
                }
                else if(clickedItem.name == alarm.name)
                {
                    info.text = "비상벨이다";
                }
                else if(clickedItem.name == multitap.name)
                {
                    info.text = "멀티탭이다";
                }
                
            }
           
            //클릭된 게임오브젝트의 태그가 mission이거나, 미션이 끝났거나, 게임이 끝났으면(아직 구현 안함)
            if(hit.collider.tag == "mission" || isMissonEnd || isGameEnd)
            {
                Debug.Log("ActiveDialogPanel");
                dialogPanel.SetActive(true);
            }
        }

        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "slot" && result.gameObject.GetComponent<Image>().sprite != null) //클릭된 UI가 슬롯이면
            {
                Debug.Log("ActiveitemSelectioPanel");
                itemSelectionPanel.SetActive(true); //아니면 아이템셀렉션패널을 연다
                clickedItem = result.gameObject; //클릭된 UI를 전역변수로 넘겨준다.
                
                //클릭된 슬롯의 이미지를 검사해 케이스마다 아이템 정보창의 컨텐츠를 셋팅한다.
                if (result.gameObject.GetComponent<Image>().sprite == extinguisherImg)
                {
                    info.text = "소화기다.";
                }
                else if (result.gameObject.GetComponent<Image>().sprite == sandImg)
                {
                    info.text = "모래다.";
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
        Debug.Log("클릭된 아이템 이름 = "+clickedItem.name);


        if (clickedItem.GetComponent<Item>().itemType != Item.Type.Equipment) { dialog.text = "습득할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; }
        for(int i =0; i<slots.Count; i++)
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
                Debug.Log(i+"번째 슬롯이 꽉참");
            }

        }
    }


    Sprite GetItemImg(GameObject item)
    {
        return item.GetComponent<Item>().defaultImg;
    }


    public void DiscardSlot() //슬롯을 비운다
    {
        if (clickedItem != null && clickedItem.GetComponent<Image>() != null && clickedItem.GetComponent<Image>().sprite != null)
        {
            clickedItem.GetComponent<Image>().color = new Color32(0,0,0,150);
            clickedItem.GetComponent<Image>().sprite = null;
        }
    }

    public void UseItem()
    {
        //아이템 타입이 Misc이면 
        if (clickedItem.GetComponent<Item>().itemType == Item.Type.Misc) { dialog.text = "사용할 수 없는 아이템입니다"; dialogPanel.SetActive(true); return; } 
        Debug.Log("사용하는 아이템:"+clickedItem.name);
        //아이템을 습득하지 않고 바로 사용할 경우
        if (clickedItem != null)
        {
            if (clickedItem.name == extinguisher.name)
            {
                Debug.Log("소화기를 사용합니다");
                GameObject extinguisher = GameObject.Find("extinguisher");
                extinguisher.GetComponent<Shooter>().ActivateShooter();

            }
            if (clickedItem.name == alarm.name)
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find("alarm");
                alarm.GetComponent<AudioSource>().Play();
            }
            if (clickedItem.name == sand.name)
            {
                GameObject sand = GameObject.Find("sand");
                sand.GetComponent<Shooter>().ActivateShooter();
            }
            
            
        }
        //아이템을 습득하고나서 사용할 경우
        if (clickedItem != null && clickedItem.GetComponent<Image>() != null && clickedItem.GetComponent<Image>().sprite != null)
        {
            Debug.Log("슬롯 이미지 이름:" + clickedItem.GetComponent<Image>().sprite.name);
            if (clickedItem.GetComponent<Image>().sprite == extinguisherImg)
            {
                Debug.Log("소화기를 사용합니다");
                //Shooter shooter = extinguisher.GetComponent<Shooter>(); 프리펩 자체에 접근했다. 게임상의 프리펩 인스턴스와는 다르기 때문에 실행이 안된다. 실행하려면 프리펩 인스턴스에 접근해야한다.
                GameObject extinguisher = GameObject.Find("extinguisher"); //이름으로 하지말까..하지만 방법이..??
                extinguisher.GetComponent<Shooter>().ActivateShooter();
            }
            if(clickedItem.GetComponent<Image>().sprite == alarmImg)
            {
                Debug.Log("알람이 울립니다");
                GameObject alarm = GameObject.Find("alarm"); 
                alarm.GetComponent<AudioSource>().Play();
            }
            if(clickedItem.GetComponent<Image>().sprite == sandImg)
            {

            }
        }
    }
}
