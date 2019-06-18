using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemInfoCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/ItemInfo")]
    public static void CreateAssets()
    {

        CustomAssetUtility.CreateAsset<ItemInfo>();
    }


}
