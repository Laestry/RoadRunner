﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    [Serializable]
    private class Positions
    {
        [SerializeField]
        public float xMin, xMax, yMin, yMax, zMin, zMax;
    }
    
    [Serializable]
    private class SpawnPositions
    {
        [SerializeField]
        public GameObject p1, p2, p3, p4;
    }
    [SerializeField]
    private GameObject hazard;
    [SerializeField]
    private float startWait, spawnWait, waveWait;
    [SerializeField]
    private int hazardCount;
    
    [SerializeField] 
    private Positions position;
    [SerializeField]
    private SpawnPositions sp;
    [SerializeField] 
    private GameObject player;
    [SerializeField] 
    private int speed;
        
    private bool gameStarted;
    private Vector3 p;
    private Vector3 spawnValues;
    private int[] x = new int[4];
    private Vector3[] arr;
    private Quaternion[] arrRot;
    private int i1;

  

    
    void Start()
    {
        arr = new[]
        {
            sp.p1.transform.position, sp.p2.transform.position, sp.p3.transform.position, sp.p4.transform.position
        };
        arrRot = new[]
        {
            sp.p1.transform.rotation, sp.p2.transform.rotation, sp.p3.transform.rotation, sp.p4.transform.rotation
        };
        
        StartCoroutine(SpawnWaves());
    }
    
    void Update()
    {
        p = player.transform.position;
        
        if (!gameStarted)
        {
            player.transform.position = new Vector3(
                Mathf.Clamp(p.x, position.xMin, position.xMax), 
                Mathf.Clamp(p.y, position.yMin, position.yMax), 
                Mathf.Clamp(p.z, position.zMin, position.zMax));
        }

        if (p.x > -9)
        {
            gameStarted = true;
            player.GetComponent<Limiter>().enabled = true;
        }
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            GenerateRandomNumbers();
            for (int i = 0; i < hazardCount; i++)
            {
                var gb = Instantiate(hazard, GeneratePosition(), arrRot[ x[i1]-1 ]);
                if(x[i1] == 1 || x[i1] == 2 )
                    gb.GetComponent<Rigidbody>().velocity = Vector3.back * (speed * UnityEngine.Random.Range(1, 3));
                else
                    gb.GetComponent<Rigidbody>().velocity = Vector3.forward * (speed * UnityEngine.Random.Range(1, 3) * 0.75f);

                ++i1;
                yield return new WaitForSeconds (spawnWait);
            }
            if(i1==hazardCount)
                i1=0;
            yield return new WaitForSeconds (waveWait);
        }
    }
    
    private void GenerateRandomNumbers()
    {
        Array.Clear(x,0, x.Length);
            
        for (int i = 0; i < x.Length; i++)
        {
            int next;                
            while (true)
            {
                next = UnityEngine.Random.Range(1, 5);
                if (!((IList) x).Contains(next)) 
                    break;                    
            }
            x[i] = next;
        }
    }

    private Vector3 GeneratePosition() 
    {
        Vector3 spawnPosition = new Vector3(arr[ x[i1]-1 ].x, arr[ x[i1]-1 ].y, arr[ x[i1]-1 ].z);
        
        return spawnPosition;
    }
}
