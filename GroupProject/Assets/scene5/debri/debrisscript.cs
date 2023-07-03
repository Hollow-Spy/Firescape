using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debrisscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //´just hurt the player when he hits him
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<hurtbounce>().bounce(transform.position);

        }
    }
}
