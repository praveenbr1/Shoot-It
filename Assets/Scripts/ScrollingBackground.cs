using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    MeshRenderer background;
    [SerializeField] float speed = 1f;
    void Start()
    {
       background = GetComponent<MeshRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        background.material.mainTextureOffset += new Vector2(0f,speed ) *Time.deltaTime; 
    }
}
