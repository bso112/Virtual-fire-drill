using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatItemInfo : MonoBehaviour
{   
    [MenuItem("Assets/Create/ItemInfo")]
    public static void CreateAssets()
    {

        CustomAssetUtility.CreateAsset<ItemInfo>();
    }

}
