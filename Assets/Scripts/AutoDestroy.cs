using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int secondsToDestroy;
    public GameObject explosion; 
    public float radius = 5.0F;
    public float power = 10.0F;
    
    void Start()
    {
        StartCoroutine(SpawnWaves());

    }

    private IEnumerator SpawnWaves()
    {
        yield  return new WaitForSeconds (secondsToDestroy);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity.normalized);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 4.0F);
        }
        yield  return new WaitForSeconds (0.15f);
        Destroy(gameObject);

    }
}
