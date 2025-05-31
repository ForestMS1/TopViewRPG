using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{

    [SerializeField] 
    private NavMeshAgent agent;

    [SerializeField] 
    private Transform target;
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
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
