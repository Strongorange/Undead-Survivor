using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 정적으로 사용하겠다는 키워드. 바로 메모리에 얹어버림.
    public PoolManager pool;
    public PlayerController player;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
