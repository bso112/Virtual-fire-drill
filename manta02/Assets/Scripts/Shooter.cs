using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{   
    //이 스크립트는 비활성상태여야 한다.
    //소화기 뿌리기, 모래 뿌리기를 위한 스크립트. 소화기, 모래 아이템이미지 게임오브젝트에 붙인다.

    public GameObject effect;
    private int mouseCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(JetRoutine(effect));
            
        }
    }

    IEnumerator JetRoutine(GameObject _effect) 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject effect = Instantiate(_effect, ray.origin, Quaternion.identity);

        effect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(effect.GetComponent<ParticleSystem>().main.duration);
        Destroy(effect);
        //한번 소화기 혹은 모래를 사용했으니 
        mouseCounter++;
        if (mouseCounter > 5) // 다섯번 사용하면
        {
            gameObject.GetComponent<Shooter>().enabled = false;
            mouseCounter = 0;
            Debug.Log(gameObject.name + "을(를) 전부 소모하였습니다.");
            InGameUIControl uiCtrl = InGameUIControl.GetInstance();
            uiCtrl.DiscardSlot();  //다쓰면 슬롯을 비움
            StopCoroutine(JetRoutine(effect)); //멈춰줘야 하나?
            

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
