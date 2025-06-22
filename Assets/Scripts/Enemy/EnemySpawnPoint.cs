using System.Collections;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void Spawn( )
    {
        StartCoroutine( SpawnCount( ) );
    }

    private IEnumerator SpawnCount( )
    {
        yield return new WaitForSeconds( 3f );
        Instantiate( enemyPrefab, transform.position, Quaternion.identity );
        Destroy( gameObject );
    }
}