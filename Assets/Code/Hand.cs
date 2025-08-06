using Unity.Mathematics;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public SpriteRenderer handSprite;

    public bool isLeft;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.25f, 0);
    Vector3 rightPos2 = new Vector3(-0.15f, -0.25f, 0);

    Quaternion leftRot = Quaternion.Euler(0, 0, -30);
    Quaternion leftRot2 = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; //0: 해당 오브젝트의 스프라이트, 1: 플레이어 스프라이트
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft) // 근접무기
        {
            transform.localRotation = isReverse ? leftRot2 : leftRot;
            handSprite.flipY = isReverse;
            handSprite.sortingOrder = isReverse ? 0 : 6;
        }
        else // 원거리무기
        {
            transform.localPosition = isReverse ? rightPos2 : rightPos;
            handSprite.flipX = isReverse;
            handSprite.sortingOrder = isReverse ? 6 : 0;
        }
    }
}
