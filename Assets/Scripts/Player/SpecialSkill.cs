using System.Collections;
using UnityEngine;

public class SpecialSkill : Skill
{
    [SerializeField] private float dashDistance = 1f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float invincibleTime = 0.5f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackAngle = 60f;
    [SerializeField] private int damage = 30;
    [SerializeField]
    private PlayerFSM playerFSM;

    private bool isInvincible = false;
    private Coroutine skillCoroutine;
    public SkillData.SkillType Type => SkillData.SkillType.Special;
    public override void Activate(Creature actor)
    {
        if (!CanUse(gameObject.GetComponent<Creature>())) return;
        CurrentCool = MaxCoolDown;
        
        Debug.Log("SpecialSkill Activate!");
        
        if (skillCoroutine != null)
            StopCoroutine(skillCoroutine);
        skillCoroutine = StartCoroutine(SpecialRoutine(actor));
    }

    private IEnumerator SpecialRoutine(Creature actor)
    {
        // 무적 상태 부여
        isInvincible = true;
        actor.SetInvincible(true);

        // 살짝 전진
        Vector3 start = actor.transform.position;
        Vector3 end = start + actor.transform.forward * dashDistance;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            float t = elapsed / dashDuration;
            actor.transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        actor.transform.position = end;
        playerFSM.ChangeState(PlayerFSM.PlayerState.SpecialSkill);
        // 공격 판정
        DoAttack(actor);

        // 무적 해제
        yield return new WaitForSeconds(invincibleTime - dashDuration);
        isInvincible = false;
        actor.SetInvincible(false);
    }

    private void DoAttack(Creature actor)
    {
        Collider[] hits = Physics.OverlapSphere(actor.transform.position + actor.transform.forward * (attackRange / 2), attackRange);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                Vector3 dirToEnemy = (enemy.transform.position - actor.transform.position).normalized;
                float angle = Vector3.Angle(actor.transform.forward, dirToEnemy);
                if (angle < attackAngle / 2f)
                {
                    enemy.OnDamage(damage);
                }
            }
        }
    }
}