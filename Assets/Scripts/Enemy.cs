using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    WayPoints destroyScore;
    SceneLoader sceneLoader;
    [Header("Enemy")]
     [SerializeField] float health = 100;
    
     [SerializeField] GameObject  enemyDestroyVFXPrefab;
     [SerializeField] AudioClip enemyDestroySound;

    [Header("Enemy Bullet")]
    [SerializeField] float fireRate;
    [SerializeField] float minimumTime = 0.2f;
    [SerializeField] float maximumTime = 3f;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] float bulletSpeed = 10f;

    [SerializeField] float rotationSpeed;
   [SerializeField] AudioClip enemyBulletAudioClip;
    

    
    
	// Use this for initialization
	void Start () 
  {
       sceneLoader = FindObjectOfType<SceneLoader>();
       fireRate = UnityEngine.Random.Range(minimumTime, maximumTime);
       StartCoroutine(Shoot());
       
       
	}

	// Update is called once per frame
	void Update () {
		
       // CountDownTimerToShoot();
      
    
	}

  /*  private void CountDownTimerToShoot()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0f)
        {
            Fire();
            fireRate = Random.Range(minimumTime, maximumTime);
        }
    } */

   /*  private void Fire()
    {
        GameObject laser = Instantiate(  enemyBullet,transform.position,Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
        Destroy(laser,2f);
    } */

    IEnumerator Shoot()
    {
      while(true)
      {
        yield return new WaitForSeconds(fireRate);
        GameObject laser = Instantiate(enemyBullet,transform.position,Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyBulletAudioClip,laser.transform.position);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-bulletSpeed);
        laser.AddComponent<RotateBullet>().rotationSpeed = rotationSpeed;
         Destroy(laser,5f);
        fireRate = UnityEngine.Random.Range(minimumTime,maximumTime);
       
       
      }
    }
   

  private void OnTriggerEnter2D(Collider2D other)
    {
        LaserDamage laserDamage = other.gameObject.GetComponent<LaserDamage>();
        if(!laserDamage){return;}
        
        HitMethod(laserDamage);
        
    }

    private void HitMethod(LaserDamage laserDamage)
    {
        health -= laserDamage.Damage();
         laserDamage.DestroyBulletWhenHit();
        if (health <= 0)
        {
            Enemydestory();
           
        }
    }
     void Enemydestory()
    {
        Destroy(gameObject);
        FindObjectOfType<SceneLoader>().IncreaseScore();
        AudioSource.PlayClipAtPoint(enemyDestroySound,this.transform.position);
        GameObject instaniate = Instantiate(enemyDestroyVFXPrefab,transform.position,Quaternion.identity);
        Destroy(instaniate,0.2f);
    }
   
     public class RotateBullet : MonoBehaviour
    {
        public float rotationSpeed = 90f;

        void Update()
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
