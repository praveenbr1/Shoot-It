using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int Score = 0;
   [SerializeField] int enemyDamageScore = 5;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highScore;

    [SerializeField] TextMeshProUGUI _highScore;
    
    [SerializeField] AudioSource buttonSounds;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject mainMenuButton;
    int currentSceneIndex = 0;
    

    
    [SerializeField] GameObject playerPrefab;
    /* Here we create a sepearate gameObjcet and we attach our audioSource Component to it
     then we add our audio to it after we take the gameObject and we add it in the above serializeField */
    private void Start()
     {
      highScore.text =  PlayerPrefs.GetInt("HighScore",0).ToString();
    }
    private void OnEnable() 
    {
        retryButton.SetActive(false);
        mainMenuButton.SetActive(false);
        highScore.enabled = false;
        _highScore.enabled = false;
    }
    public void loadScene()
    { 
       
       Time.timeScale = 1f;
       buttonSounds.Play();
       mainMenuButton.SetActive(false);
       retryButton.SetActive(false);
       
       StartCoroutine (Wait());
    }

    public void RetryButton()
    {
        FindObjectOfType<MusicSingleton>().PlayMusic();
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetScore();
        mainMenuButton.SetActive(false);
        DestroyAllGameObjects();
        Time.timeScale = 1f;
        Instantiate(playerPrefab, new Vector3(0f,-7f,0f),Quaternion.identity);
        retryButton.SetActive(false);
        highScore.enabled = false;
        _highScore.enabled = false;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.8f);
         currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        OnEnable();
    }

    public void Mainmenu()
    {   
        FindObjectOfType<MusicSingleton>().PlayMusic();
        retryButton.SetActive(false);
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
        
     }
     public void DestroyAllGameObjects()
   {
     WayPointsEnemies[] wayPoints = (FindObjectsOfType<WayPointsEnemies>());
 
     for (int i = 0; i < wayPoints.Length; i++)
     {
         Destroy(wayPoints[i].gameObject);
     }
     
     LaserDamage[] laserDamages = (FindObjectsOfType<LaserDamage>());
     {
       for(int i = 0 ; i < laserDamages.Length ; i++)
       {
           Destroy(laserDamages[i].gameObject);
       }
     }

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        retryButton.SetActive(true);
        mainMenuButton.SetActive(true);
         highScore.enabled = true;
        _highScore.enabled = true;
    }
    public  void IncreaseScore()
    {
     Score +=  enemyDamageScore;
     score.text = Score.ToString();
     if(Score > PlayerPrefs.GetInt("HighScore",0))
     {
        PlayerPrefs.SetInt("HighScore",Score);
        highScore.text = Score.ToString();
     }

    }

    public void ResetScore()
    {
        Score = 0;
        score.text = Score.ToString();
        
    }
   
   public void CloseGame()
   {
    Application.Quit();
   }
}