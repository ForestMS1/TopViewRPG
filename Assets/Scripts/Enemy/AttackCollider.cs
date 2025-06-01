using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (enemy.State == Enemy.EnemyState.Chase && coll.gameObject.CompareTag("Player"))
        {
            enemy.State = Enemy.EnemyState.Attack;
            if(enemy.CanAttack())
                enemy.DoAttackHit<Player>();
            enemy.State = Enemy.EnemyState.Chase;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (enemy.State == Enemy.EnemyState.Chase && coll.gameObject.CompareTag("Player"))
        {
            enemy.State = Enemy.EnemyState.Attack;
            if(enemy.CanAttack())
                enemy.DoAttackHit<Player>();
            enemy.State = Enemy.EnemyState.Chase;
        }
    }
}
