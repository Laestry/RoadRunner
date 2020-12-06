using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float speed;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
    }
}
