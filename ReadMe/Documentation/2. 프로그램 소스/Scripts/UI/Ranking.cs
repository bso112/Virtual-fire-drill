using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public GameObject pageGroup;
    [HideInInspector] public Transform[] pages;
    [HideInInspector] public Transform[] players;

    private DB_data tmp; //내림차순 정렬에서 스왑에 사용될 임시 데이터

 
    //6000이 최대
    public string CalcRank(int score)
    {
        if (score > 10000)
        {
            return "A";
        }
        else if (score > 5000)
        {
            return "B";
        }
        else if (score > 3000)
        {
            return "C";
        }
        else if (score > 2000)
        {
            return "D";
        }
        else
        {
            return "F";
        }

    }


    public void DescendData(ref DB_data[] datas)
    {
        //데이터를 스코어 순으로 내림차순 정렬한다. (버블정렬아님)

        for (int i = 0; i < datas.Length - 1; i++)
        {
            for (int j = i + 1; j < datas.Length; j++)
            {
                if (int.Parse(datas[i].Score) < int.Parse(datas[j].Score))
                {
                    tmp = datas[i];
                    datas[i] = datas[j];
                    datas[j] = tmp;
                }
            }

        }
    }

    public void SetValueToUI(DB_data[] datas)
    {

        DescendData(ref datas);

        pages = new Transform[pageGroup.transform.childCount];





        for (int i = 0; i < pageGroup.transform.childCount; i++)
        {
            pages[i] = pageGroup.transform.GetChild(i);
        }

        players = new Transform[pages.Length * pages[0].childCount +1 ];


        int count = 0;

        for (int j = 0; j < pages.Length; j++)
        {
            for (int i = 0; i < pages[j].childCount; i++)
            {
                if(pages[j].GetChild(i).tag == "playerData")
                    players[count++] = pages[j].GetChild(i);
            }
        }
        Debug.Log("player lenght" + players.Length);
        Debug.Log("data.leght" + datas.Length);



        //players[0]에는 11번 플레이어가 들어간다.
        for (int i = 0; i < datas.Length; i++)
        {
            Debug.Log(players[i].name);
            Debug.Log(players[i].transform.GetChild(1).name);
            players[i].transform.GetChild(1).GetComponent<Text>().text = datas[i].Users;
            players[i].transform.GetChild(2).GetComponent<Text>().text = datas[i].Timeleft;
            players[i].transform.GetChild(3).GetComponent<Text>().text = datas[i].today;
            players[i].transform.GetChild(4).GetComponent<Text>().text = datas[i].Score;
            players[i].transform.GetChild(5).GetComponent<Text>().text = CalcRank(int.Parse(datas[i].Score));

        }


    }


}
