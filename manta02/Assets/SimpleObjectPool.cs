using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FireObjects
{
    [SerializeField]
    public GameObject flag;
    [SerializeField]
    public List<GameObject> firelist = new List<GameObject>();
}

// A very simple object pooling class
public class SimpleObjectPool : MonoBehaviour
{
    // the prefab that this object pool returns instances of
    
    public GameObject prefab;
    
    public List<GameObject> firelist = new List<GameObject>();

    [SerializeField]
    // collection of currently inactive instances of the prefab
    private Stack<GameObject> inactiveInstances = new Stack<GameObject>();
    [SerializeField]
    private Stack<GameObject> fireinactiveInstances = new Stack<GameObject>();

    // Returns an instance of the prefab
    public GameObject GetObject(bool isFlag)
    {
        GameObject spawnedGameObject;

        // if there is an inactive instance of the prefab ready to return, return that
        if (inactiveInstances.Count > 0 && isFlag == true)
        {
            // remove the instance from teh collection of inactive instances
            spawnedGameObject = inactiveInstances.Pop();
        }
        else if( fireinactiveInstances.Count > 0 && isFlag == false)
        {
            spawnedGameObject = fireinactiveInstances.Pop();
        }
        // otherwise, create a new instance
        else
        {
            if( isFlag == true )
              spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
            else
                spawnedGameObject = (GameObject)GameObject.Instantiate( firelist[Random.Range(0, firelist.Count)] ); 

            // add the PooledObject component to the prefab so we know it came from this pool
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        // enable the instance
        spawnedGameObject.SetActive(true);

        // return a reference to the instance
        return spawnedGameObject;
    }

    // Return an instance of the prefab to the pool
    public void ReturnObject(GameObject toReturn , bool isFlag)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        // if the instance came from this pool, return it to the pool
        if (pooledObject != null && pooledObject.pool == this)
        {
            // disable the instance
            toReturn.SetActive(false);

            // add the instance to the collection of inactive instances
            if (isFlag == true)
                inactiveInstances.Push(toReturn);
            else
                fireinactiveInstances.Push(toReturn);
        }
        // otherwise, just destroy it
        else
        {
            Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
    }
}

// a component that simply identifies the pool that a GameObject came from
public class PooledObject : MonoBehaviour
{
    public SimpleObjectPool pool;
}