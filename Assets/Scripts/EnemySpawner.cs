using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] List<WayPoints> wayPoints;
    [SerializeField] int waveNumber = 0;
    [SerializeField] bool loopWaves = false;
    IEnumerator Start()
    {
       do{
          yield return StartCoroutine(SpawnEnemyWaves());
        }
        while (loopWaves);
    }

    IEnumerator SpawnEnemyWaves()
    {
        for(int waveIndex = waveNumber; waveIndex < wayPoints.Count; waveIndex++)
        {
        WayPoints currentWave = wayPoints[waveIndex];
        yield return StartCoroutine(SpawnEnemiesInThisWave(currentWave));
        }
    }
    IEnumerator SpawnEnemiesInThisWave(WayPoints wayPoint)
    {
        for(int enemyCount = 0; enemyCount < wayPoint.Numberofenemies(); enemyCount++)
        {
           var newEnemy = Instantiate(wayPoint.EnemyPrefab(),wayPoint.WayPoint()[0].transform.position,Quaternion.identity);
          newEnemy.GetComponent<WayPointsEnemies>().SetWaveCongi(wayPoint);
            yield return new WaitForSeconds(wayPoint.EnemySpawnTime());
        }
    }
}
