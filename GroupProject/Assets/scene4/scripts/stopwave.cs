using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwave : MonoBehaviour
{
    public wavescript wave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //simply stops the wave, and then kills it seconds after
        {
            wave.speed = 0;
            wave.StopAllCoroutines();
            StartCoroutine(killwave());
        }
    }
    
      
    
     IEnumerator killwave()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(wave.gameObject);
    }
}
