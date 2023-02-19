using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave")]
public class WayPoints : ScriptableObject
{
   [SerializeField] GameObject enemyPrefab;
   [SerializeField] GameObject pathPrefab;
   
   [SerializeField] float enemySpeed;
   

   [SerializeField] float enemySpawnTime;
   [SerializeField] int numberOfEnemeies;

    

   public GameObject EnemyPrefab() {return enemyPrefab;}

   public List<Transform> WayPoint()
    {
        List<Transform> wayPoints = new List<Transform>();
        foreach( Transform child in pathPrefab.transform)
        {
            wayPoints.Add(child);
        }
        return wayPoints;
        
    }
   public float EnemySpeed() {return enemySpeed;}

   

   public float EnemySpawnTime() {return enemySpawnTime;}
   public int Numberofenemies() { return numberOfEnemeies;}
   

   
   

}
