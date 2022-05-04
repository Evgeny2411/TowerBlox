using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectFalling : MonoBehaviour
{
    public static int lives = 3;
    public static bool roofLose;
    private static int misses = 0;
    // фиксирует промахи игрока
 
    void Update()
    {
        var diff = fallingTower.miss - misses;
        if(diff > 0)
        {
            lives = lives - diff;
            misses += diff;
            if(lives < 0)
            {
                lives = 0;
            }
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "dropping")
        {
            lives = lives - 1;
            if (lives < 0) { lives = 0; }
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "roof")
        {
            roofLose = true;
        }
    }

}
