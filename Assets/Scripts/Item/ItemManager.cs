using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    public static ItemManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {   

        foreach(var prefab in prefabs)
        {
            items.Add(prefab.name, prefab);
            Debug.Log(prefab.name);
        }


        
    }

}
