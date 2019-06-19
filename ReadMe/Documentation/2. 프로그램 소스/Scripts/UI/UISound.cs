using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UISound : MonoBehaviour
{   

    //ui클릭했을때 클릭사운드 내기 위한 스크립트.

    SoundManger soundMgr;

    private void Start()
    {
        soundMgr = SoundManger.instance;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) soundMgr.PlaySingle(soundMgr.clickSound);

#else
        if (Input.touchCount > 0) soundMgr.PlaySingle(soundMgr.clickSound);
     
#endif


    }
}
