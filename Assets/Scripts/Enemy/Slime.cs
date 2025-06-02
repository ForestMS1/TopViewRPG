using UnityEngine;
using UnityEngine.AI;

public class Slime : Enemy
{
    private Animator animator;
    
    void Awake()
    {
        MaxHP = 100f;
        HP = MaxHP;
        MaxMP = 50f;
        MP = MaxMP;
        ATK = 10f;
        DEF = 3f;
        EXP = 3f;
        IsDead = false;

        state = EnemyState.Idle;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        attackCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(state == EnemyState.Chase && !target.GetComponent<Player>().IsDead)
            agent.SetDestination(target.position);
    }
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        animator.SetTrigger("Damage");
    }
}
