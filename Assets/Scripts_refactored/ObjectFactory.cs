using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectFactory : MonoBehaviour
{
    //나중에 오브젝트 풀로 대체
    public static ObjectFactory Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    public GameObject DialogPanel, SelectionPanel, Inventory, InfoPanel, PlayerStatePanel, VideoWindow, elevatorSelectionPanel, MissionTimePanel,
        GameOverPanel, GameComplitedPanel;

    public GameObject extinguisher, towel, multiTap, oilStove, waterBucket, alarm, cat, door, elevator, sand, gasValve;

    [HideInInspector] public List<GameObject> uiPrefabs;
    [HideInInspector] public List<GameObject> itemPrefabs;



    // Start is called before the first frame update
    void Start()
    {
        uiPrefabs = new List<GameObject> {DialogPanel, SelectionPanel, Inventory, InfoPanel, PlayerStatePanel, VideoWindow, elevatorSelectionPanel, MissionTimePanel,
        GameOverPanel, GameComplitedPanel};

        itemPrefabs = new List<GameObject> { extinguisher, towel, multiTap, oilStove, waterBucket, alarm, cat, door, elevator, sand, gasValve };


    }
    
}
