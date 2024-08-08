using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D target;
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriteRenderer;
    Animator anim;
    WaitForFixedUpdate wait;

    public RuntimeAnimatorController[] animCon;
    public float speed;
    public float health;
    public float maxHealth;
    bool isLive;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>(); // 어떤 콜라이더든 가져와진다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        isLive = true;
        coll.enabled = true; // 콜라이더 활성화
        rigid.simulated = true; // 리지드바디 물리적 활성화
        spriteRenderer.sortingOrder = 2; // 시체가 다른 오브젝트 가리지 않게 하 다시 바꾸기
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !isLive)
        {
            return;
        }

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // .. Live, Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {
            // .. Die
            isLive = false;
            coll.enabled = false; // 콜라이더 비활성화
            rigid.simulated = false; // 리지드바디 물리적 비활성화
            spriteRenderer.sortingOrder = 1; // 시체가 다른 오브젝트 가리지 않게 하기
            anim.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
