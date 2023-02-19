using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
   
    

    [SerializeField] float wrapPadding = 1.5f;
  //  public float wrapBuffer = 0.1f;
    private float xMin, xMax, yMin, yMax;
    private float objectWidth, objectHeight;

    void Start()
    {
        objectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        xMin = bottomLeft.x + objectWidth / 2 + wrapPadding;
        xMax = topRight.x - objectWidth / 2 - wrapPadding;
        yMin = bottomLeft.y + objectHeight / 2 + wrapPadding;
        yMax = topRight.y - objectHeight / 2 - wrapPadding;
    }

    void Update()
    {
        if (transform.position.x < xMin)
            transform.position = new Vector3(xMax, transform.position.y, 0);

        if (transform.position.x > xMax )
            transform.position = new Vector3(xMin, transform.position.y, 0);

        if (transform.position.y < yMin)
            transform.position = new Vector3(transform.position.x, yMax, 0);

        if (transform.position.y > yMax)
            transform.position = new Vector3(transform.position.x, yMin, 0);
    }
}

