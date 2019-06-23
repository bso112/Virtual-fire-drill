using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemInfo[] itemInfos = Resources.FindObjectsOfTypeAll<ItemInfo>();

        foreach(var info in itemInfos)
        {
            info.itemInfo = info.itemInfo.Replace('n', '\n');
        }
    }

  
}
