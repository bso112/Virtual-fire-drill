using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
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

    }


    public GameObject DialogPanel, SelectionPanel, ItemSelectionPanel, Inventory, InfoPanel, PlayerStatePanel, VideoWindow, elevatorSelectionPanel, MissionTimePanel,
        GameOverPanel, GameComplitedPanel;

    public Text Dialog;
    public Slider hp;

    public GameObject extinguisher, towel, multiTap, oilStove, waterBucket, alarm, cat, door, elevator, sand, gasValve;


    [HideInInspector] public List<GameObject> uiPrefabs;
    [HideInInspector] public List<GameObject> itemPrefabs;
    [HideInInspector] public List<string> itemNames = new List<string>();




    public void DestroyIFNotRegisterd(GameObject obj)
    {
        if (!itemNames.Contains(obj.name))
            Destroy(obj);
    }

    // Start is called before the first frame update

    private void OnEnable()
    {
        uiPrefabs = new List<GameObject> {DialogPanel, SelectionPanel, Inventory, InfoPanel, PlayerStatePanel, VideoWindow, elevatorSelectionPanel, MissionTimePanel,
        GameOverPanel, GameComplitedPanel};

        itemPrefabs = new List<GameObject> { extinguisher, towel, multiTap, oilStove, waterBucket, alarm, cat, door, elevator, sand, gasValve };

        foreach (var obj in itemPrefabs)
        {
            itemNames.Add(obj.name);
        }

    }


}
