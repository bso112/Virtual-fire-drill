using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



public class first_connet : MonoBehaviour
{
    public static first_connet Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    public enum eDBQuery { INSERT_UPDATE, RANK1, RANK2, RANK3, SELECTALL };
    public enum eSuccessed { FAIL, SUCCESS }    //DB와의 연결이 잘 되었는지

    private GameManager gm;



    private int dataCount = 0;
    private DB_data[] playerDatas;
    public DB_data[] GetPlayerData()
    {
        return playerDatas;
    }

    [Serializable]
    public class Data
    {
        public string username;
        public int cleartime;
        public int score;
        //public string mission;
    };


    public eDBQuery dbQuery;
    public Data datas;

    private string server = "https://guide94.cafe24.com/graduationwork/1.php";
    public string GetServer() { return server; }


    public void UpdataDatas(string username, int cleartime, int score)
    {
        datas.username = username;
        datas.cleartime = cleartime;
        datas.score = score;
    }


    private void Start()
    {
        //playerDatas = new DB_data[] { new DB_data("byeon", "11", "54", "20180215"), new DB_data("kim", "20", "20", "20180215") };
        StartCoroutine(this.LoadDB(server));
    }

    public void _SetValueToUI()
    {
        gameObject.GetComponent<Ranking>().SetValueToUI(playerDatas);
    }

    public IEnumerator LoadDB(string _url)
    {
        WWWForm tForm = new WWWForm(); //php로 전송할 데이터를 같이 포함시킨다. 
                                       //php에선 $_POST["db_host"]; 의 형태로 받는다. 
                                       //tForm.AddField("id", "TEST");
        tForm.AddField("DBQuery", (int)dbQuery);

        switch (dbQuery)
        {
            case eDBQuery.INSERT_UPDATE:
                tForm.AddField("users", datas.username);
                tForm.AddField("timeleft", datas.cleartime);
                tForm.AddField("score", datas.score);
                //tForm.AddField("mission", datas.mission);
                break;

            case eDBQuery.RANK1:
                break;
            case eDBQuery.RANK2:
                break;
            case eDBQuery.RANK3:
                break;

            case eDBQuery.SELECTALL:
                break;
        }

        WWW www = new WWW(_url, tForm); //php를 호출
        yield return www;   //응답이 돌아올때까지 대기한다. 이 대기시간 때문에 코루틴으로 진행한다.


        //Debug.Log(www.text);


        if (www.error != null)  //php 접근 자체에 문제가 있으면 www.error가 발생한다.
        {
            Debug.Log("www에러" + www.error);
        }
        else
        {


            Debug.Log(www.text);

            string result = www.text;

            ArrayList list = new ArrayList();

            JObject jo = JObject.Parse(result);

            //Debug.Log(jo);

            //SELECT * 나오는값
            //RANK 값
            //나중에 디비쿼리 추가할수도있으니 케이스문으로 관리하는게좋ㅇ륻ㅅ
            JArray ja = JArray.Parse(jo["result"].ToString());
            foreach (JObject jobj in ja)
            {
                DB_data date_slice = new DB_data();
                date_slice.Users = jobj["users"].ToString();
                date_slice.Timeleft = jobj["timeleft"].ToString();
                date_slice.Score = jobj["score"].ToString();
                date_slice.today = jobj["today"].ToString();
                date_slice.mission = jobj["mission"].ToString();

                list.Add(date_slice);
            }

            playerDatas = new DB_data[list.Count];
            //플레이어 데이터에 대입하며 반복돌때 필요한 인덱스
            int index = 0; 

            switch (dbQuery)
            {
                case eDBQuery.RANK1:
                    foreach (DB_data u in list)
                    {

                    }
                    break;
                case eDBQuery.RANK2:
                    foreach (DB_data u in list)
                    {
                    }
                    break;
                case eDBQuery.RANK3:
                    foreach (DB_data u in list)
                    {
                    }
                    break;
                case eDBQuery.SELECTALL:
                    foreach (DB_data u in list)
                    {
                        Debug.Log(u.Users);
                        playerDatas[index] = u;
                        index++;
                    }
                    break;
            }

            //foreach (DB_data u in list)
            //{
            //    print("user = " + u.Users + " / timeleft = " + u.Timeleft + " / score = " + u.Score + " / today = " + u.today + " / mission = " + u.mission);
            //}


        }
    }



}
