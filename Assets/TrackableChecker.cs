using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class TrackableChecker : MonoBehaviour
{
    private List<DetectedPlane> currentDetectedPlanes = new List<DetectedPlane>();
    private List<GameObject> createdFlags = new List<GameObject>();

    public GameObject[] firePrefabs;
    public int minFirePerPlane = 2;
    public int maxFirePerPlane = 8;

    void Update()
    {
        Session.GetTrackables<DetectedPlane>(currentDetectedPlanes,TrackableQueryFilter.New);


        foreach(var plane in currentDetectedPlanes)
        {
            GameObject firePrefabToSpawn = firePrefabs[Random.Range(0, firePrefabs.Length)];

            int count = Random.Range(minFirePerPlane, maxFirePerPlane + 1);

            for (int i =0; i < count; i++)
            {
                // 감지된 표면 중심에서 앞쪽으로 (중심에서 앞/뒤쪽 모서리까지의 길이) 사이에서 랜덤 길이만큼 이동한 위치
                // 감지된 표면 중심에서 오른쪽으로 (중심에서 오른쪽/왼쪽 모서리까지의 길이) 사이에서 랜덤 길이만큼 이동한 위치

                Vector3 randomSpawnPos
                    = plane.CenterPose.position
                    + plane.CenterPose.forward * Random.Range(-plane.ExtentZ, plane.ExtentZ)
                    + plane.CenterPose.right * Random.Range(-plane.ExtentX, plane.ExtentX);

                Instantiate(firePrefabToSpawn, randomSpawnPos, plane.CenterPose.rotation);
            }
        }
    }
}