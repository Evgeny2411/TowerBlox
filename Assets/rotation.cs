using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rotation : MonoBehaviour
{

    // Start is called before the first frame update

    private bool playing;
    Vector3 startAngle;   //Reference to the object's original angle values

    float rotationSpeed = 1f;  //Speed variable used to control the animation

    float rotationOffset = 30f; //Rotate by 30 units

    public static float finalAngle;  //Keeping track of final angle to keep code cleaner

    void Start()
    {
        startAngle = transform.eulerAngles;  // Get the start position
    }

    void Update()
    {
        playing = GameController.playing;
        if (playing == true)
        {
            finalAngle = startAngle.z + (float)Math.Sin(Environment.TickCount * rotationSpeed * 0.001) * rotationOffset;  //Calculate animation angle
            transform.eulerAngles = new Vector3(startAngle.x, startAngle.y, finalAngle); //Apply new angle to object 
        }
    }
}
