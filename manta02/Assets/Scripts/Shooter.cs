using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //이 스크립트는 비활성상태여야 한다.
    //소화기 뿌리기, 모래 뿌리기를 위한 스크립트. 소화기, 모래 아이템이미지 게임오브젝트에 붙인다.

    //Shooter.eable은 사용하기 버튼을 클릭한 시점에서 true고 Usage는 소화기를 실제로 마우스클릭으로 사용했을때 Using이 됨
    [HideInInspector] public enum Usage //아이템을 사용하고 있는 상태인가?
    {
        IDLE,
        USING,
    }

    public Usage state = Usage.IDLE;
    public GameObject effect;
    public int limitJetCount = 15; //소화기 하나로 분출할 수 있는 횟수
    private int mouseCounter;
    [HideInInspector] public int extinguishedCount = 0;
    



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {StartCoroutine(JetRoutine(effect));}
        //터치if (Input.touchCount > 0) { StartCoroutine(JetRoutine(effect)); }
    }

    Touch tempTouches;

    IEnumerator JetRoutine(GameObject _effect) 
    {
        state = Usage.USING;
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    tempTouches = Input.GetTouch(0);
        //}
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //터치 Ray touchRay = Camera.main.ScreenPointToRay(tempTouches.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        //터치 if(Physics.Raycast(touchRay, out hit))
            {
            //부딪힌게 불이면 끈다.
            if(hit.collider.tag == "fire")
            {
                Debug.Log("불의 이름:" + hit.collider.name);
                //왜 hit.collider.gameObject.GetComponent<ParticleSystem>().colorOverLifetime.enable = true 처럼 바로 접근하면 안되고 차근차근 접근하면 되는거지?
                //물을 쓰고, 기름에 의한 화재일 경우 끄지 못한다.
                if(this.effect.name == "water" && hit.collider.gameObject.transform.parent.gameObject.name == "oilStove") { Debug.Log("불을 끄지 못합니다"); yield break; }
                ParticleSystem ps =  hit.collider.gameObject.GetComponent<ParticleSystem>();
                Fire fire = hit.collider.gameObject.GetComponent<Fire>();
                fire.enabled = true;
                var col = ps.colorOverLifetime;
                col.enabled = true;
                var psMain = ps.main;
                psMain.loop = false;
                fire.isExtinguished = true; //불을 껐다는걸 불에게 알린다.
                extinguishedCount++; //끈 불의 갯수 세기(미션스크립트에 넘겨주기 위함)
                if(extinguishedCount > 10) { Debug.Log("끈 불의 수가 10개가 넘음"); extinguishedCount = 0; }
                

                

            }
        }
        GameObject effect = Instantiate(_effect, ray.origin, Quaternion.identity);
        //터치 GameObject effect = Instantiate(_effect, touchRay.origin, Quaternion.identity);

        effect.GetComponent<ParticleSystem>().Play();
        //파티클시스템(소화기분말)의 수명만큼 기다리다가 파괴한다.
        yield return new WaitForSeconds(effect.GetComponent<ParticleSystem>().main.duration); 
        Destroy(effect); 
        //한번 소화기 혹은 모래를 사용했으니 카운터를 올린다.
        mouseCounter++;
        if (mouseCounter > limitJetCount) // 사용횟수제한만큼 사용하면
        {
            gameObject.GetComponent<Shooter>().enabled = false;
            mouseCounter = 0;
            Debug.Log(gameObject.name + "을(를) 전부 소모하였습니다.");
            InGameUIControl uiCtrl = InGameUIControl.GetInstance();
            uiCtrl.DiscardSlot();  //다쓰면 슬롯을 비움
            state = Usage.IDLE;
            

        }
        yield return null;

        
    }

    public void ActivateShooter() //이 스크립트를 활성화하기. 사용하기 버튼에 연결해야함.
    {
        gameObject.GetComponent<Shooter>().enabled = true;
        //버튼을 클릭했을때 gameObject는 버튼이아닌, 이 스크립트가 붙어있는 게임오브젝트다. extinguisherImg, sandImg
        Debug.Log("슈터가 붙어있는 게임오브젝트 이름 : " + gameObject.name);
        if(gameObject.GetComponent<Shooter>() == null)
        {
            Debug.Log("슈터가 안붙어있습니다");
        }
        
    }
   
}
