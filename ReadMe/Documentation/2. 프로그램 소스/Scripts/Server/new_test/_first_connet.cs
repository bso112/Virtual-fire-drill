using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class _first_connet : MonoBehaviour
{
    public enum eDBQuery { INSERT_UPDATE, RANK1, RANK2, RANK3, SELECTALL };
    public enum eSuccessed { FAIL, SUCCESS }    //DB와의 연결이 잘 되었는지

    public Text[] test;
    public int inum = 1;

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

    string server = "http://guide94.cafe24.com/graduationwork/1.php";

    void Start()
    {
        StartCoroutine(this.LoadDB(server));
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
            //Debug.Log(www.text);

            string result = www.text;

            ArrayList list = new ArrayList();

            JObject jo = JObject.Parse(result);

            Debug.Log(jo);

            //SELECT * 나오는값
            //RANK 값
            //나중에 디비쿼리 추가할수도있으니 케이스문으로 관리하는게좋ㅇ륻ㅅ
            JArray ja = JArray.Parse(jo["result"].ToString());
            foreach (JObject jobj in ja)
            {
                DB_data date_slice = new DB_data();
                date_slice.Users = jobj["users"].ToString();
                date_slice.Timeleft = jobj["timeleft"].ToString();
                date_slice.today = jobj["today"].ToString();
                date_slice.Score = jobj["score"].ToString();



                list.Add(date_slice);
            }



            switch (dbQuery)
            {
                case eDBQuery.RANK1:
                    foreach (DB_data u in list)
                    {
                        test[inum * 4 - 4].text = u.Users;
                        test[inum * 4 - 3].text = u.Timeleft;
                        test[inum * 4 - 2].text = u.today;
                        test[inum * 4 - 1].text = u.Score;
                        inum++;

                    }
                    break;
                case eDBQuery.RANK2:
                    foreach (DB_data u in list)
                    {
                        test[inum * 4 - 4].text = u.Users;
                        test[inum * 4 - 3].text = u.Timeleft;
                        test[inum * 4 - 2].text = u.today;
                        test[inum * 4 - 1].text = u.Score;
                        inum++;
                    }
                    break;
                case eDBQuery.RANK3:
                    foreach (DB_data u in list)
                    {
                        test[inum * 4 - 4].text = u.Users;
                        test[inum * 4 - 3].text = u.Timeleft;
                        test[inum * 4 - 2].text = u.today;
                        test[inum * 4 - 1].text = u.Score;
                        inum++;
                    }
                    break;
                case eDBQuery.SELECTALL:
                    foreach (DB_data u in list)
                    {
                        //print("user = " + u.Users + " / timeleft = " + u.Timeleft + " / score = " + u.Score + " / today = " + u.today );
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
