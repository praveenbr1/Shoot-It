using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
     [SerializeField] float cameraShakeDuration = 1f;
    [SerializeField] float cameraShakeMagnitude = 0.5f;
     
     Coroutine coroutine;
    Vector3 camerInitialPosition;

    void Start()
    {
        camerInitialPosition = transform.position;
    }

    public void Play()
    {
       coroutine = StartCoroutine(Shake());
    }

    public void Stop()
    {
      StopAllCoroutines();
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0;
        while(elapsedTime < cameraShakeDuration)
        {
            transform.position = camerInitialPosition + Random.insideUnitSphere * cameraShakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = camerInitialPosition;
    }


}
