using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DbChecker : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    private AudioSource _audio;
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();
    }

    //업데이트에서 실행
    public bool Dbcheck(float db)
    {
        loudness = GetAveragedVolume() * sensitivity;
      
        if (loudness > db)
        {
            Debug.Log("소리가난다");
            GetComponent<AudioSource>().enabled = false;
            return true;
        }
        return false;
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}
