using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShooter : MonoBehaviour
{
    // 감지할 게임 오브젝트 레이어
    public LayerMask whatIsTarget;

    // 물 줄기를 그릴 라인 렌더러
    private LineRenderer waterLineRenderer;
    private AudioSource waterAudio;

    public Transform waterShootPos;



    private void Awake()
    {
        waterLineRenderer = GetComponent<LineRenderer>();
        waterAudio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        waterLineRenderer.enabled = false;

        if (Input.GetMouseButton(0))
        {
            Shot();
        }
        else
        {
            waterAudio.Stop();
        }
    }

    void Shot()
    {
        waterLineRenderer.enabled = true;
        waterAudio.Play();

        // 화면상의 한점을 찍으면, 카메라에서 화면 상의 한점을 향해 발사되는 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        waterLineRenderer.SetPosition(0, waterShootPos.position);


        // 광선을 쏴서 충돌체가 감지됬다면
        if (Physics.Raycast(ray, out hit, 50f, whatIsTarget))
        {

            // 상대방 게임 오브젝트를 비활성화
            hit.collider.gameObject.SetActive(false);

            // 끝점을 충돌 위치
            waterLineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            waterLineRenderer.SetPosition(1, ray.origin + ray.direction * 10f);
        }
    }
}