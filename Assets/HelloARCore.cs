using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;


public class HelloARCore : MonoBehaviour
{

    public GameObject spawnTarget;


    // Update is called once per frame
    void Update()
    {
        // 화면 잡힌것들중에서 원하는 것을 가져옴
        // Session.GetTrackables<Type>();

        // 화면 터치를 최소 한 지점이라도 해야 계속 진행
        if(Input.touchCount <= 0)
        {
            return;
        }

        Touch touchPosition = Input.GetTouch(0);

        // Frame Raycast에 의한 충돌 정보 + 기타 등등 정보를 담는 단순 컨테이너
        TrackableHit hit;
        // Frame.Raycast(스크린 x, 스크린 y, 필터링할것들, 정보를 담을 컨테이너)
        // Feature point 특정 사물
        if(Frame.Raycast(touchPosition.position.x , touchPosition.position.y, TrackableHitFlags.PlaneWithinInfinity , out hit))
        {
            // 감지된 바닥이 맞다면
            if(hit.Trackable is DetectedPlane)
            {
                Instantiate(spawnTarget, hit.Pose.position, hit.Pose.rotation);
            }
        }
    }
}
