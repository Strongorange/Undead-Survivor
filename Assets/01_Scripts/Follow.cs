using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // UI는 월드 좌표계, 다른 게임 오브젝트들은 스크린 좌표계
        rect.position = Camera.main.WorldToScreenPoint( // 월드 좌표계를 스크린 좌표계로 변환
            GameManager.instance.player.transform.position
        );
    }
}
