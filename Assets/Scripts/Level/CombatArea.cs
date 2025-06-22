using UnityEngine;
using System.Collections.Generic;

public class CombatArea : MonoBehaviour
{
    private GameObject[] monsters;
    private bool isIn = false;
    
    void Start()
    {
        FindMonsterSpawns( );
    }

    private void FindMonsterSpawns( )
    {
        BoxCollider box = GetComponent<BoxCollider>( );
        Vector3 center = box.bounds.center;
        Vector3 halfExtents = box.bounds.extents;
        Quaternion rotation = transform.rotation;

        Collider[] hits = Physics.OverlapBox(center, halfExtents, rotation);

        List<EnemySpawnPoint> foundTargets = new();

        foreach (var col in hits)
        {
            EnemySpawnPoint target = col.GetComponent<EnemySpawnPoint>();
            if( target != null )
            {
                foundTargets.Add(target);
                target.gameObject.SetActive( false );
            }
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if( !isIn )
            isIn = true;
        else
            return;
        
        if( other.CompareTag( "Player" ) )
        {
            foreach( GameObject mob in monsters )
            {
                mob.SetActive( true );
                mob.GetComponent<EnemySpawnPoint>(  ).Spawn(  );
            }
        }
    }
}