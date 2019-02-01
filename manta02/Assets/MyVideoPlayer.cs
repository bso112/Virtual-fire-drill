using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MyVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            videoPlayer.Play();
        }
        else if(Input.GetMouseButton(1))
        {
            videoPlayer.Pause();
        }

        Debug.Log(videoPlayer.time);
    }
}
