using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorInstantiation : MonoBehaviour
{
    [SerializeField] private Transform mirrorSpawn;

    [SerializeField] private GameObject mirrorPlayerPrefab;

    [SerializeField] private GameObject Player;

    private GameObject mirrorPlayer;
    
    private bool hasSpawnedMirror = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasSpawnedMirror == false)
        {
            mirrorPlayer = Instantiate(mirrorPlayerPrefab, mirrorSpawn);
            mirrorPlayer.GetComponent<Rigidbody2D>().gravityScale = -Player.GetComponent<Rigidbody2D>().gravityScale;
            
            if (mirrorPlayer.GetComponent<Rigidbody2D>().gravityScale > 0.1f)
            {
                mirrorPlayer.transform.Rotate(180.0f, 0f,0f);
            }
            
            hasSpawnedMirror = true;
        }
    }
}
