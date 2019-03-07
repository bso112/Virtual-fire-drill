using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguish : MonoBehaviour

    //안써두댐
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        Debug.Log("불의 이름: " + ps.gameObject.name);
        var col = ps.colorOverLifetime;
        col.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
                
    }
}
