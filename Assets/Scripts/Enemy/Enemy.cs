using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{

    [SerializeField] 
    protected NavMeshAgent agent;
    [SerializeField] 
    protected Transform target;
    protected Rigidbody rb;
    protected SphereCollider attackCollider;
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    protected EnemyState state;
    public EnemyState State
    {
        get => state;
        set => state = value;
    }
    
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
        if(state == EnemyState.Chase && !target.GetComponent<Player>().IsDead)
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

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        if(!IsDead)
            state = EnemyState.Chase;
    }

    public EnemyState GetState()
    {
        return state;
    }
}
