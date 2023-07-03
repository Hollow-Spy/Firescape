using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger6 : MonoBehaviour
{
    
    public GameObject trigger6object,triggerblock;
 
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            triggerblock.SetActive(true);
            Destroy(trigger6object);
            Destroy(gameObject);
        }
    }
}
