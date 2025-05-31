using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{

    [SerializeField] 
    private NavMeshAgent agent;
    [SerializeField] 
    private Transform target;
    private Rigidbody rb;
    private SphereCollider attackCollider;
    private enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    private EnemyState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }

    void Update()
    {
        if(state == EnemyState.Chase)
            agent.SetDestination(target.position);
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void FreezeVelocity()
    {
        rb.linearVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (state == EnemyState.Chase && coll.gameObject.CompareTag("Player"))
        {
            state = EnemyState.Attack;
            if(CanAttack())
                DoAttackHit<Player>();
            state = EnemyState.Chase;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (state == EnemyState.Chase && coll.gameObject.CompareTag("Player"))
        {
            state = EnemyState.Attack;
            if(CanAttack())
                DoAttackHit<Player>();
            state = EnemyState.Chase;
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if(!IsDead)
            state = EnemyState.Chase;
    }
}
