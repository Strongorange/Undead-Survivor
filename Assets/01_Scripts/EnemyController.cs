using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public float speed;
    bool isLive = true;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 플레이어랑 충돌하면 충돌로 인해 속도가 생겨 이동 속도가 변하는 것을 방지
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
