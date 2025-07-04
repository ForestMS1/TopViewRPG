using System.Collections;
using UnityEngine;

public class UltimateSkill : Skill
{
    [SerializeField] private float radius = 5f;         // 공격 범위
    [SerializeField] private int damage = 50;           // 공격력
    [SerializeField] private LayerMask enemyLayer;      // 적 레이어
    [SerializeField] private GameObject effectPrefab;   // 이펙트 프리팹 (선택사항)
    [SerializeField] private PlayerFSM playerFSM;

    public override void Activate(Creature actor)
    {
        if (!CanUse(actor))
        {
            Debug.Log("스킬을 사용할 수 없습니다.");
            return;
        }

        base.Activate(actor); // 쿨타임 시작
        
        playerFSM.ChangeState(PlayerFSM.PlayerState.UltimateSkill);
        // 1. 이펙트 출력
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, actor.transform.position, Quaternion.identity);
        }

        // 2. 범위 내 적 탐색
        Collider[] hitEnemies = Physics.OverlapSphere(actor.transform.position, radius, enemyLayer);

        // 3. 데미지 적용
        foreach (var enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Enemy target))
            {
                target.OnDamage(damage);
            }
        }

        Debug.Log($"궁극기 사용! {hitEnemies.Length}명의 적에게 {damage} 데미지를 입힘.");
    }

    // 디버그용 범위 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}