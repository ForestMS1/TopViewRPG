using UnityEngine;
using UnityEngine.AI;

public class Slime : Enemy
{
    [SerializeField] 
    private SlimeData slimeData;
    
    void Awake()
    {
        Init();

        state = EnemyState.Idle;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        attackCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    void Init()
    {
        if (slimeData == null)
        {
            Debug.Log("SlimeData가 연결되지 않았습니다.");
            return;
        }
        MaxHP = slimeData.MaxHP;
        HP = MaxHP;
        MaxMP = slimeData.MaxMP;
        MP = MaxMP;
        ATK = slimeData.ATK;
        DEF = slimeData.DEF;
        EXP = slimeData.EXP;
        IsDead = false;
        attackRange = slimeData.attackRange;
        attackCoolTime = slimeData.attackCoolTime;
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

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}
