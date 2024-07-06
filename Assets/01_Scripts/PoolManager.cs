using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    // 풀을 담당하는 리스트들
    List<GameObject>[] pools;

    void Awake() {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Lenth; index++){
            pools[index] = new List<GameObject>();
        }
    } 

    public GameObject Get(int index){
        GameObject select = null;

        // 선택한 풀의 놀고 있는 (비활성화 된) 게임오브젝트 접근
            

        foreach(GameObject item in pools[index]){
            if(!item.activeSelf) {
                // 발견하면 select 변수에 할당
                select = item;
                select.setActive(true);
                break;
            }
        }
        
        // 비활성화된 게임오브젝트가 없다면

        
            
        if(!select){
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        
        return select;
    }

}

