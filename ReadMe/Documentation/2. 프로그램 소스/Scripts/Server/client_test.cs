using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class client_test : MonoBehaviour
{
    public enum eSuccessed { FAIL, SUCCESS }    //DB와의 연결이 잘 되었는지
    public enum eDBQuery { INSERT_UPDATE, DELETE, SELECT }  //입력한 데이터를 어떤 용도로 DB에 전달할 것인지

    //[System.Serializable]
    //public class Ranking
    //{
    //    public int no;      //고유번호
    //    public string name; //이름
    //    public int score;   //점수
    //}

    //public eDBQuery dbQuery;
   // public Ranking rank;
    //eSuccessed successed;

    string phpUrl = "http://dg-test.iptime.org/php_test/Server_test.php";    //php 주소

    // Use this for initialization
    void Awake()
    {
        StartCoroutine(LoadFromPhp());  //바로 코루틴을 시작
    }

    IEnumerator LoadFromPhp()
    {
        string load;
        //Ranking[] infos;
        WWWForm urlForm = new WWWForm();
        urlForm.AddField("db_host", "localhost");   //php로 전송할 데이터를 같이 포함시킨다. php에선 $_POST["db_host"]; 의 형태로 받는다. 
        urlForm.AddField("db_user", "root");
        urlForm.AddField("db_passwd", "qwerqwer1");
        urlForm.AddField("db_name", "unitydbtest ");
        //urlForm.AddField("DBQuery", (int)dbQuery);

        WWW www = new WWW(phpUrl, urlForm); //php를 호출한다.

        yield return www;   //응답이 돌아올때까지 대기한다. 이 대기시간 때문에 코루틴으로 진행한다.
    }
}
