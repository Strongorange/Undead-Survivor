using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔 범위
    public LayerMask targetLayer; // 스캔할 레이어
    public RaycastHit2D[] targets; // 레이캐스트 해서 맞은 객체 배열
    public Transform nearestTarget;

    void FixedUpdate()
    {
        // 원형 형태의 광선 발사
        targets = Physics2D.CircleCastAll(
            transform.position, // 캐스팅 시작 위치
            scanRange, // 원의 반지름
            Vector2.zero, // 캐스팅 방향 (원이니까 없음)
            0, // 캐스팅 길이 (원이니까 없음)
            targetLayer // 대상 레이어
        );
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; // 시작 기준점, 아래의 foreach 돌면서 이 값을 업데이트해서 점점 작아지고 나중에는 가장 가까운 요소 1개가 선택됨

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position; // 플레이어 위치
            Vector3 targetPos = target.transform.position; // 현재 순회중의 타겟의 위치
            float curDiff = Vector3.Distance(myPos, targetPos); // 플레이어 위치와 현재 타겟의 위치 차이

            if (curDiff < diff) // 현재 아이템과 거리 차이가 diff 기준보다 가까우면
            {
                diff = curDiff; // diff 기준을 현재 차이로 변경
                result = target.transform; // 가장 가까운 타겟을 현재 타겟으로 변경
            }
        }

        return result;
    }
}
