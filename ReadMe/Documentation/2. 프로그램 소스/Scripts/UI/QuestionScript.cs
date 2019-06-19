using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;



public class QuestionScript : MonoBehaviour
{
    QuizManager gm;
    GameObject panel;
    public Text txtQuestion;
    float timeSpan;
    float checkTime;

    // Use this for initialization

    void Start()
    {
        gm = QuizManager.instance;

        //  gm.SetQuiz();
        txtQuestion.text = gm.GetCurrentQuestion();
        panel = GameObject.Find("Panel");
        timeSpan = 0.0f;
        checkTime = 3.0f;
        GameObject.Find("SoundOpen").GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {

        timeSpan += Time.deltaTime;

        if (timeSpan > checkTime)

        {
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
            timeSpan = 0;
        }
    }
}