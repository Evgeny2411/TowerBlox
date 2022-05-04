using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public GameObject crane;
    private Vector3 offset;
    // Двигает камеру за краном
    void Start()
    {
        offset = transform.position - crane.transform.position;
    }

    void LateUpdate()
    {
        transform.position = crane.transform.position + offset;
    }
}
