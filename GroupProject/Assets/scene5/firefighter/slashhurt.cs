using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashhurt : MonoBehaviour
{

    //script for all moves that involve slashes for the boss, uses simple hitbox and rotation according to boss rotation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int inv;
        if(transform.rotation.eulerAngles.y == 180)
        {
            inv = -1;
        }
        else
        {
            inv = 1;
        }
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<hurtbounce>().bounce( new Vector2(transform.position.x * inv,transform.position.y));
        }
    }
}
