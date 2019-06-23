using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ItemGetter : MonoBehaviour
{
    //클릭아이템이 없으면 빈 게임오브젝트를 반환한다.(클릭아이템기반으로 이벤트를 발생시키는게 과연 좋을까?)
    public static GameObject clickeditem { get; private set; }
    


    [Header("Here are click events! Drag anything here!")]
    
    public UnityEvent OnClick;

    

    public void OnClickEvent()
    {
        if (OnClick != null)
            OnClick.Invoke();

    }




    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (!Input.GetMouseButtonDown(0))
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount <= 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        if (Physics.Raycast(ray, out RaycastHit hit))
        {   
            if(!CanvasRaycaster.IsPointerOverUIObject())
            {
                Debug.Log("레이캐스트 된 객체" + hit.transform.name);
                if (hit.transform.tag == "item")
                {
                    clickeditem = hit.transform.gameObject;
                    ObjectFactory.Instance.SelectionPanel.SetActive(true);
                }


                OnClickEvent();
            }
            
        }

    }

   
}
