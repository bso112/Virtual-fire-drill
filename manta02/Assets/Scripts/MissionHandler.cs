using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionHandler : MonoBehaviour
{


    public void StartMissionRoutine(Mission mission, float timeLimit)
    {
        Debug.Log("zzzz");
        mission.isMissionOn = true;
        StartCoroutine(mission.MissionRoutine(timeLimit));
    }


}
