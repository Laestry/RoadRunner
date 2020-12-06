using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moving : MonoBehaviour
{
    [SerializeField]
    [Range(5, 15)]
    private int forceAmount;
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * forceAmount);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * forceAmount);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward * forceAmount);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back * forceAmount);
    }
}
