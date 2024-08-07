using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property
        type = data.itemType;
        rate = data.damages[0];

        ApplyGear(); // 장비가 새롭게 추가될때 ApplyGear 적용
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear(); // 장비가 레벨업할때 ApplyGear 적용
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Debug.Log("RateUP 동작!");
        // 부모에서 무기 찾아오기
        // transform.parent === player, 부모에서 자식들의 Weapons를 찾는다.
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        Debug.Log("정보");
        Debug.Log(weapons);
        Debug.Log(weapons.Length);

        foreach (Weapon weapon in weapons)
        {
            Debug.Log("Weapons", weapon);
            switch (weapon.id)
            {
                case 0: // 근접 무기
                    Debug.Log("근접 찾음!!!");
                    weapon.speed = 150 + (150 * rate);
                    break;
                default: // 원거리 무기
                    weapon.speed = 0.5f * (1f - rate); // 이 값이 작아지면 원거리 무기의 연사 속도가 빨라진다.
                    break;
            }
        }
    }

    void SpeedUp()
    {
        Debug.Log("신발 속도 상승!");
        float speed = 5; // 플레이어 기본 이동 속도;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
