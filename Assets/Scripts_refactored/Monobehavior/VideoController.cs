using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class VideoController : MonoBehaviour
{
    //비디오가 붙은 게임오브젝트에 붙임 ex) 엘리베이터
    private GameObject videoWindow;

    private void Start()
    {
        videoWindow = ObjectFactory.Instance.VideoWindow;
        
    }
    //엘리베이터 사용. 엘리베이터선택패널의 예 버튼에 연결
    public void PlayVideo()
    {
        SoundManger.instance.musicSource.volume = .1f;
        VideoPlayer videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Play();
        videoWindow.SetActive(true);
        videoPlayer.loopPointReached += OnVideoOver;

    }


    /// <summary>
    /// 비디오가 끝났을 때의 이벤트
    /// </summary>
    /// <param name="vp"></param>
    public void OnVideoOver(VideoPlayer vp)
    {
        vp.Stop();
        videoWindow.SetActive(false);
    }


}
