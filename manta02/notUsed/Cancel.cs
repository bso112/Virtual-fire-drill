using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;


//쓸모없음.. CloseWindow로 간단히 처리함.
public class Cancel : MonoBehaviour 
{

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        //UI의 레이캐스트
        //public void Raycast(EventSystems.PointerEventData eventData, List<RaycastResult> resultAppendList);

        //Check if the left Mouse button is clicked
        if (Input.GetMouseButtonDown(0)) //클릭했을때 한번만 실행됨
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>(); //RaycastResult는 RaycastHit같은것.

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results); //Physics.Raycast(ray, out hit)

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
                //버튼이 아니고, 맞은게 자기자신이면(자기자신이 아닌데 지워버리니 캔버스도 지워짐)
                if (result.gameObject.GetComponent<Button>() == null && result.gameObject == gameObject)
                {
                    result.gameObject.SetActive(false);
                }

                if (result.gameObject.GetComponent<Button>() != null && result.gameObject == gameObject)
                {
                    Debug.Log("정보보기 클릭");


                    //코루틴의 yield return null 로도 먼저 실행되버림. 콜백에 대해서 알아보자. 간단하게 구현하려면 몇초기다리면 되긴하는데,,
                    //코루틴써서 0.1초를 기다려봤지만 한번 정상적으로 실행된다음에 다시 정보보기 버튼을 누르니 실행이안됨. 
                    // result.gameObject.transform.parent.gameObject.SetActive(false); //부모(판넬)를 비활성화. 이게 InGameControl의 ActiveInfoPanel보다 먼저 실행되서 ActiveInfoPanel는 호출 안됨
                    // 델리게이트를 이용한 콜백을 써도 이 로직이 update 구문 안에 있는이상 매프레임마다 실행되므로 InGameControl의 ActiveInfoPanel보다 먼저 실행될 수밖에 없다.
                    // 꼼수를 쓰자. 투명하게 해서 없어진 것 처럼 보이게 하고 몇초지나서 없애기.
                }
            }
        }
    }
    

 
  


}

