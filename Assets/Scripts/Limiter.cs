using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Limiter : MonoBehaviour
{
    [SerializeField]
    private float xMin, xMax, yMin, yMax, zMin, zMax;
    private Vector3 p;

    private void Start()
    {
        
    }

    void Update()
    {
        p = transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(p.x, xMin, xMax), 
            Mathf.Clamp(p.y, yMin, yMax), 
            Mathf.Clamp(p.z, zMin, zMax));
    }
}
