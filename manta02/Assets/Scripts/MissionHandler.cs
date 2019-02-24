using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionHandler : MonoBehaviour
{
    public void StartMission(Mission mission)
    {
        Mission.isMissionOn = true;
        mission.MissionEvent();
        
    }
    

}
