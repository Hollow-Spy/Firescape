using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointsetter : MonoBehaviour
{
    private gamemaster checkpointset;

    //marks the checkpoint to whatever trigger this is attached to, nice
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            checkpointset = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
            checkpointset.checkpoint = transform.position;
        }
    }
}
