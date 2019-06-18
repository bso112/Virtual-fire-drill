using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGetter : MonoBehaviour
{
    public static GameObject clickeditem { private set { clickeditem = value; }  get { return clickeditem; } }

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
