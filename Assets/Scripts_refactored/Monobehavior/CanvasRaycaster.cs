using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 이 컴포넌트가 붙은 캔버스의 자식 중, 그래픽 레이캐스트가 붙은 UI를 레이캐스트한다.
/// </summary>
public class CanvasRaycaster : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    /// <summary>
    /// 클릭된 슬롯
    /// </summary>
    public static refactored.Slot slot;

    /// <summary>
    /// 레이캐스트된 UI다.
    /// </summary>
    public static GameObject result { get; private set; }


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
        if (!Input.GetMouseButtonDown(0)) return;
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        // m_PointerEventData.position = Input.mousePosition;
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            CanvasRaycaster.result = result.gameObject;
            Debug.Log("그래픽 레이캐스트 된 객체" + result.gameObject.name);

            if(result.gameObject.tag == "slot")
            {
                slot = result.gameObject.GetComponent<refactored.Slot>();
            }
        }
    }

    /// <summary>
    /// 마우스포인터가 UI엘레멘트 위에 있는가?
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
