using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionController : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        // 2024-08-20 아래 로직은 문제가 있어 사용하지 않음
        // Vector3 playerDir = GameManager.instance.player.inputVec;



        switch (transform.tag)
        {
            case "Ground":
                // 기존 Player Input Vector 값을 기준으로 맵의 이동 방향을 결정하던 로직에 문제가 있어
                // 플레이어와 맵의 거리 차이로 맵의 이동 방향 설정
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

                    transform.Translate(ran + dist * 2);
                }
                break;
        }
    }
}
