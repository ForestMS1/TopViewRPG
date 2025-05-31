using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{

    [SerializeField] 
    private NavMeshAgent agent;

    [SerializeField] 
    private Transform target;

    private Rigidbody rb;
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
        
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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
}
