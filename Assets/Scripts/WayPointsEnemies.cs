using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsEnemies : MonoBehaviour
{
   WayPoints serializeWayPoints;
     List<Transform> wayPoints;
   

    int wayPointIndex = 0;
    float targetSpeed;
    Vector3 targetPoint;

    private void Start() {
        wayPoints = serializeWayPoints.WayPoint();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

     private void Update()
    {
        Move();
    }
    
   public void SetWaveCongi(WayPoints wayPoint)
   {
          this.serializeWayPoints = wayPoint;
   }
    private void Move()
    {
        if (wayPointIndex < wayPoints.Count)
        {
            targetPoint = wayPoints[wayPointIndex].transform.position;
            targetSpeed = serializeWayPoints.EnemySpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, targetSpeed);
            if (transform.position == targetPoint)
            {
                wayPointIndex++;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
