using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0) // 음수 값은 근접 무기를 의미, 양수면 원거리 무기를 의미
        {
            rigid.velocity = dir * 15; // 날아가는 속도 결정
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || per == -100)
        {
            return;
        }

        per--;

        if (per < 0)
        {
            rigid.velocity = Vector2.zero; // 나중에 다시 true 되었을때를 대비해서 미리 속도를 0으로
            gameObject.SetActive(false);
        }
    }
}
