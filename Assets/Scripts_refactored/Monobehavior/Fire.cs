using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{   
    
    public bool isExtinguished;
    [HideInInspector] public float time;

    private void Start()
    {
        //성능을 위해 이 스크립트는 소화기분무액에 충돌이 일어날때까지 비활성상태이다. 
        this.enabled = false;
    }

    void Update()
    {
        if(isExtinguished == true)
        {

            time += Time.deltaTime;
            if (time > gameObject.GetComponent<ParticleSystem>().main.duration)
            { //불의 지속시간만큼 기다리고 스스로를 파괴한다.
                Destroy(gameObject);
            }
        }
    }
}
