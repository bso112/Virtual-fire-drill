using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public Quiz[] quizes;
    bool[] array = new bool[4];
    protected GameManager gm;
    protected InGameUIControl uiControl;
    public GameObject QuizPanel, QuizStart, extinguisher, alarm;
    public Text dialog;
    protected Text scoreText;
    public int score;
    public List<string> missionDialog = new List<string>();
    public GameObject QuestionMark;
    //원래 있던 코드

    //아래로는 성재형 코드 추가

    public Text QuizText;

    public Text QuizButton1, QuizButton2, QuizButton3, QuizButton4;

    ArrayList list = new ArrayList();
    public ArrayList clones = new ArrayList();
    public Dictionary<string, string> dictionary = new Dictionary<string, string>();
    public static QuizManager instance = null;
    public int answerCount = 0;
    public string Stage { get; set; }
    public int Level { get; set; }
    public bool isTutorial = false;
    public int random;

    //QuestuinScript안의 코드
    GameObject panel;
    bool test = false;

    public int count;
    public int QuizCount;
    [HideInInspector] public static bool isMissionOn, isMissonSucced = false; // 미션이 끝났다,성공했다 / 미션이 끝났다,실패했다/ 를 구분하기 위함
    [HideInInspector] public GameObject clickedItem; //아이템일 수도, 슬롯일 수도 있다.

    // Start is called before the first frame update


    private void Awake()
    {
        if (!instance)
        {
            instance = this;

            quizes = new Quiz[4];
            for (int i = 0; i < quizes.Length; i++)
            {
                quizes[i] = new Quiz();
            }

            Debug.Log("Level = " + Level);

            quizes[0].Question = "화재 상황시 올바르지 않은 방법은?";
            quizes[0].AnswerNumber = 3;
            quizes[0].Answers = new string[] { "낮은 자세로 유도등, 유도표지를 따라 대피합니다.", "방문을 열기 전 문 손잡이가 뜨거우면 다른 길을 찾습니다.",
                "옷에 불이 붙었을 때에는 눈과 입을 가리고 바닥에서 뒹굽니다.", "화재시에는 엘리베이터를 타고 신속하게 대피합니다." };

            quizes[1].Question = "소화기 사용시 유의사항 중 틀린 것은?";
            quizes[1].AnswerNumber = 1;
            quizes[1].Answers = new string[] { "너무 가까이 접근하여 화상을 입지 않도록 주의", "바람을 마주 보고 호스를 불쪽으로 향함", "방사된 가스는 마시지 말고 즉시 환기", "지하공간이나 창문이 없는 곳에서 사용하지 않기" };

            quizes[2].Question = "전기화재에 대처법 중 아닌 것은?";
            quizes[2].AnswerNumber = 2;
            quizes[2].Answers = new string[] { "두꺼비 집을 내린다", "소화기를 사용한다", "콘센트를 뺀다", "젖은 담요, 수건 등으로 불이 난 곳을 완전히 덮는다." };

            quizes[3].Question = "무슨 문제를 낼까아아~~";
            quizes[3].AnswerNumber = 3;
            quizes[3].Answers = new string[] { "워우예~", "예스~~~", "눈누 난나~", "빰빰!!" };


        }
    }
    void Start()
    {
        scoreText = GameManager.GetInstance().score;
        dialog = InGameUIControl.GetInstance().dialog;
        uiControl = InGameUIControl.GetInstance();
        gm = GameManager.GetInstance();
    }

    Touch tempTouches;

    int n = 0;

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Update");
        if (!Input.GetMouseButtonDown(0)) { return; }


        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    tempTouches = Input.GetTouch(0);
        //}

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //메인카메라 태그 설정해야 nullponit exception 안난다.
        //Ray touchRay = Camera.main.ScreenPointToRay(tempTouches.position);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(mouseRay, out hit)) //touchRay로 바꿔야 함
        {
            //인스펙터에서 각 아이템과 아이템이미지에 콜라이더가 있는지, 태그가 잘 되있는지, 이름이 잘 되있는지 확인!
            //아이템을 클릭했을 때
            if (hit.collider.tag == "Quiz")
            {
                Debug.Log("ActivateSelectionPanel");
                clickedItem = hit.collider.gameObject;
                //QuizStart.SetActive(true);

                //Invoke("Quiz", 2);


                Debug.Log("퀴즈 실행");

                QuizText.text = GetCurrentQuestion();
                QuizPanel.SetActive(true);
                panel = GameObject.Find("QuizPanel");

                Invoke("CreateQuiz", 2);

                hit.collider.tag = "UsedQuiz";
            }
        }
    }
    private void Quiz()
    {

    }
    private void MakeQuiz()
    {
        QuizText = GameObject.Find("Question").GetComponent<Text>();
    }

    private void CreateQuiz()
    {
        bool b = true;
        while (b)
        {
            random = Random.Range(0, 3);
            if (!array[random])
            {
                array[random] = true;
                QuizText.text = quizes[random].Question;
                Debug.Log("zzzzzz");
                b = false;

                QuizButton1.text = quizes[random].Answers[0];
                QuizButton2.text = quizes[random].Answers[1];
                QuizButton3.text = quizes[random].Answers[2];
                QuizButton4.text = quizes[random].Answers[3];

            }
        }
    }

    public string GetCurrentQuestion()
    {
        string txt = "";

        {
            txt = "질문에 해당하는 것을 모두 고르세요.";
        }
        return txt;
    }
    public string[] GetAnswerArray()
    {
        return quizes[Level].Answers;
    }

    public string GetRandomAnswer()
    {
        string[] arr = quizes[Level].Answers;
        int random = Random.Range(0, arr.Length);
        Debug.Log("Random Asnwer = " + arr[random]);
        return arr[random];
    }



}