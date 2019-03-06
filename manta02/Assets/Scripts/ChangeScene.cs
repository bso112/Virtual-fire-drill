using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneChange1()
    {
        SceneManager.LoadScene("RastScene");
    }
    public void SceneChange2()
    {
        SceneManager.LoadScene("");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
