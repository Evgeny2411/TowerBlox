using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingTower : MonoBehaviour
{
    public static int miss = 0;
    // фиксирует падения на пол

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "recentBlock")
        {
            miss = miss + 1;
            Destroy(collision.gameObject);
        }
    }
}
