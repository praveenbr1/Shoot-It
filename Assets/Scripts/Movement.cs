using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
  CameraShake cameraShake;
  SceneLoader sceneLoader;
  
   [Header("Player")]
  [Range(0f,500f)]  [SerializeField] float playerHealth;
    [SerializeField] float shipSpeed = 1f;
    [SerializeField] TextMeshProUGUI playerHealthText;
     [SerializeField] float collisionDamge;
      
    [SerializeField] AudioClip playerDestroySound;
   // [SerializeField] float cameaShakeDuration;
   // [SerializeField] float cameraShakeMagnitude;
    
    float yMinimum;
    float yMaximum;
    
    float xminimum;
   
    [SerializeField] float xLeftSide;
    [SerializeField] float xRightSide;

    [SerializeField] int yMinimumPadding = 1;
    [SerializeField] int yMaximumPadding = -10;
   
    [Header("Player bullet")]
    [SerializeField] GameObject playerBullets;
    [SerializeField] int bulletspeed;
    //bool canShoot = true;
    [SerializeField] float fireRateTimeDelay = 1;
    [SerializeField] AudioClip bulletSound;

    [SerializeField] float fireRateTime;
    float nextFire;

    Coroutine bullet_Fire_Repeat;
  
    

    //float nextShot;
    
    
    
    Vector3 upDirection;
    Vector3 rightDirection;

    
   private void Awake() 
   {
    
    
    cameraShake = Camera.main.GetComponent<CameraShake>();
   }
    void Start()
    {
       
       Camera camera = Camera.main;
       yMinimum = camera.ViewportToWorldPoint(new Vector3(0,0,0)).y + yMinimumPadding;
       yMaximum = camera.ViewportToWorldPoint(new Vector3(0,1f,0)).y + yMaximumPadding;
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
        movement();
        ShootingBullets();
        
        
       
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        ShipsCollision(other);
        LaserDamage laserDamage = other.gameObject.GetComponent<LaserDamage>();
         if (!laserDamage) { return; }
         ShakeIT();
        HitMethod(laserDamage);
        
        
    }

    
    private void ShipsCollision(Collider2D other)
    {
           if (other.tag =="EnemyShip")
        {
            playerHealth -= collisionDamge;
            Math.Max(0f,playerHealth);
            playerHealthText.text = playerHealth.ToString();
            cameraShake.Play();
            if (playerHealth <= 0)
            {   
                cameraShake.Stop();
                FindObjectOfType<MusicSingleton>().StopMusic();
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(playerDestroySound, this.transform.position);
                FindObjectOfType<SceneLoader>().GameOver();
                return; 

            }
        }
    }
    private void ShakeIT()
    {   if(cameraShake != null)
        cameraShake.Play();
    }

    private void HitMethod(LaserDamage laserDamage)
    {
        playerHealth -= laserDamage.Damage();
        Math.Max(0,playerHealth);
        playerHealthText.text = playerHealth.ToString();
        laserDamage.DestroyBulletWhenHit();
        if (playerHealth <= 0)
        {
            cameraShake.Stop();
            FindObjectOfType<MusicSingleton>().StopMusic();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(playerDestroySound,this.transform.position);
            FindObjectOfType<SceneLoader>().GameOver();
            
            
            
        }
        
    }
     
   public void Please()
   {
     playerHealth = 500f;
     playerHealthText.text = playerHealth.ToString();

   }
    public float GetPlayerHealth()
    {
        return playerHealth;
    }
   
    
    
     private void ShootingBullets()
    {
   // if(Time.time > nextShot)
    {
     //  canShoot = true;

    }
         bool mouseButtonPressd = false;
        
        if(Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRateTime;
           mouseButtonPressd = true;
           
        }

        else if(Input.GetMouseButtonUp(0) )
        {
            StopCoroutine(bullet_Fire_Repeat);
            mouseButtonPressd = false;
        }
        if(mouseButtonPressd )
        {
            
            bullet_Fire_Repeat = StartCoroutine(BulletStreak());
        }
        

        


    }
    IEnumerator BulletStreak()
    { 
        while(true)
       {   GameObject laser =  Instantiate(playerBullets,transform.position,Quaternion.identity) as GameObject;
           laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletspeed);
           AudioSource.PlayClipAtPoint(bulletSound,laser.transform.position);
           //nextShot = Time.time + fireRate;
           Destroy(laser,2f);
           yield return new WaitForSeconds(fireRateTimeDelay);

           //canShoot = false;
           
       }  
    }
   

    private void movement()
    {
        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S)))
        {
            upDirection =  Vector2.up * Time.deltaTime * shipSpeed;
            float newYPositon = Mathf.Clamp(upDirection.y + transform.position.y,yMinimum,yMaximum);
            transform.position =  new Vector3(transform.position.x,newYPositon);
            
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            upDirection =   Vector2.up * Time.deltaTime * shipSpeed;
            float newYposition =Mathf.Clamp(transform.position.y-upDirection.y,yMinimum,yMaximum);
            transform.position = new Vector3(transform.position.x,newYposition);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rightDirection = Vector2.right * Time.deltaTime * shipSpeed;
            transform.position += rightDirection;
           // ScreenWrap();
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rightDirection = Vector2.right * Time.deltaTime * shipSpeed;
            transform.position -= rightDirection;
           // ScreenWrap();
        }
    }

    private void ScreenWrap()
    {
        float currentPosition_In_X =  transform.position.x;

        if(currentPosition_In_X < xLeftSide)
        {
            transform.position = new Vector3(xRightSide,transform.position.y,0);
        }
        else if(currentPosition_In_X > xRightSide)
        {
            transform.position = new Vector3(xLeftSide,transform.position.y,0);
        }
    }
}


