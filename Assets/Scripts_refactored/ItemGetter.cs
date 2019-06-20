using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGetter : MonoBehaviour
{
    //클릭아이템이 없으면 빈 게임오브젝트를 반환한다.(클릭아이템기반으로 이벤트를 발생시키는게 과연 좋을까?)
    private static GameObject clickeditem;
    public static GameObject Getclickeditem
    {
        get { if (clickeditem == null) return new GameObject(); return clickeditem; }
    }


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
        if (Input.GetMouseButton(0))
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount <= 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        if(Physics.Raycast(ray, out RaycastHit hit))
        {   
            //언제 클릭아이템을 넣을지 정해줘야한다.
            clickeditem = hit.transform.gameObject;
            OnClickEvent();
        }



    }

   
}
