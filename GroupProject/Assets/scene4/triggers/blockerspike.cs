using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockerspike : MonoBehaviour
{
    private bool once = true;
    public GameObject spikesfx;

    //simple script for the player blockers, these spikes have a radius and whenever player walks into them it will go up w sound effect
    //also provided function to go down and disable itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true)
        {
            once = false;
            Instantiate(spikesfx, transform.position, Quaternion.identity);
            StartCoroutine(Rise()); 
        }
    }

    IEnumerator Rise()
    {
        for(int i =0; i < 20; i++)
        {
            transform.parent.Translate(0, 0.037f, 0);
            yield return null;
        }
      
    }

    public IEnumerator Lowerspike()
    {
        Instantiate(spikesfx, transform.position, Quaternion.identity);
        for (int i = 0; i < 20; i++)
        {
            transform.parent.Translate(0, -0.037f, 0);
            yield return null;
        }
        this.enabled = false;

    }
}
