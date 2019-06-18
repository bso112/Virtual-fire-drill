using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanel : MonoBehaviour {

    public Sprite[] sprites;
    public Image min10;
    public Image min01;
    public Image sec10;
    public Image sec01;

    // Start is called before the first frame update
    void Start() {

    }

    public void SetTime(int min, int sec) {
        string m = min.ToString();
        string s = min.ToString();

        string mm10 = m.Substring(0, 1);
        string mm01 = m.Substring(1, 1);

        string ss10 = m.Substring(0, 1);
        string ss01 = m.Substring(1, 1);

        min10.sprite = sprites[int.Parse(mm10)];
        min01.sprite = sprites[int.Parse(mm01)];
        sec10.sprite = sprites[int.Parse(ss10)];
        sec01.sprite = sprites[int.Parse(ss01)];
    }

}
