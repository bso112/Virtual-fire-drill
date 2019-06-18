using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSetter : MonoBehaviour
{
    //OnClickEvents에 연결

    /// <summary>
    /// InfoPanel에 아이템 정보 셋팅
    /// </summary>
    public void SetItemInfo()
    {
        
        Text text = gameObject.GetComponent<Text>();
        text.text = ItemGetter.clickeditem.GetComponent<Item>().itemInfo.itemInfo;
        
    }

    /// <summary>
    /// GameCompletedPanel에 플레이어정보 셋팅
    /// </summary>
    public void SetPlayerInfo()
    {
        GameManager gm = GameManager.GetInstance();
        int score = (int)(gm.time);
        GameObject gameCompletedPanel = gameObject;

        gameCompletedPanel.transform.Find("UserName").GetComponent<Text>().text = "이재상";
        gameCompletedPanel.transform.Find("LastTime").GetComponent<Text>().text = score.ToString() + "초";

        foreach (var a in Mission.succedMissionNames)
        {
            gameCompletedPanel.transform.Find("SuccessMission").GetComponent<Text>().text += a + ", ";
        }

        foreach (var a in Mission.failedMissionNames)
        {
            gameCompletedPanel.transform.Find("FailedMission").GetComponent<Text>().text += a + ", ";
        }

        gameCompletedPanel.transform.Find("Score").GetComponent<Text>().text = Mission.score.ToString();

        gameCompletedPanel.transform.Find("Grade").GetComponent<Text>().text = new Ranking().CalcRank(Mission.score);
    }
    


}
