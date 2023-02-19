using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        SetUpSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void SetUpSingleton()
    {
      int number = FindObjectsOfType<MusicSingleton>().Length;
       if(number > 1)
       {
          Destroy(gameObject);
       }
       else
       { 
            DontDestroyOnLoad(gameObject);    
       }
      
    }

public void PlayMusic()
{
    audioSource.Play();
}
  public void StopMusic()
  {
    audioSource.Stop();
  }

}
