using Unity.Mathematics;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public SpriteRenderer handSprite;

    public bool isLeft;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPos2 = new Vector3(-0.15f, -0.15f, 0);

    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
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
            // 목표 회전값을 선택 (플레이어가 뒤집혔는지에 따라)
            Quaternion targetRot = isReverse ? leftRot2 : leftRot;
            // 회전값을 직접 대입 (진동 방지)
            transform.localRotation = targetRot;
    
            // 플레이어가 뒤집혔으면 손 스프라이트도 Y축 반전
            handSprite.flipY = isReverse;
            // 손의 렌더링 순서 설정 (뒤집힘 여부에 따라)
            handSprite.sortingOrder = isReverse ? 0 : 6;
        }
        else // 원거리무기
        {
            // 목표 위치값을 선택 (플레이어가 뒤집혔는지에 따라)
            Vector3 targetPos = isReverse ? rightPos2 : rightPos;
            // 위치값을 직접 대입 (진동 방지)
            transform.localPosition = targetPos;
    
            // 플레이어가 뒤집혔으면 손 스프라이트도 X축 반전
            handSprite.flipX = isReverse;
            // 손의 렌더링 순서 설정 (뒤집힘 여부에 따라)
            handSprite.sortingOrder = isReverse ? 6 : 0;
        }
    }
}
