using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    [SerializeField] float damage;
   
   
      public float Damage()
    {
        return damage;
    }
  


   public void DestroyBulletWhenHit()
   {
      Destroy(gameObject);
   }
private void OnTriggerEnter2D(Collider2D other) 
{
       if(other.tag == "Bullet")
    {
        Destroy(other.gameObject);
        DestroyBulletWhenHit();
    }
}
}
