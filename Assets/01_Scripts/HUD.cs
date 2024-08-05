using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health
    }

    public InfoType type;
    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float nextExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / nextExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level); // 이 부분 공부
                break;
            case InfoType.Kill:
                Debug.Log(GameManager.instance.kill);
                myText.text = string.Format("{0:F0}", GameManager.instance.kill); // 이 부분 공부
                break;
            case InfoType.Time:
                break;
            case InfoType.Health:
                break;
        }
    }
}
