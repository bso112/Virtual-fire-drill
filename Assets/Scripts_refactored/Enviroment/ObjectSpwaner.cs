using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpwaner : MonoBehaviour
{
    public float spawnMergin;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> items = ObjectFactory.Instance.itemPrefabs;

        foreach (var item in items)
        {

            Instantiate(item , new Vector3(spawnMergin, 0, 0), Quaternion.identity).name = item.name;
            spawnMergin *= 0.6f;

        }
    }


}
