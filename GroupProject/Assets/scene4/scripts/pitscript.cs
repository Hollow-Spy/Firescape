using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitscript : MonoBehaviour
{
    public GameObject fallsfx,gameoverscreen;
    //pit script, anything that is suppose to make the player fall, play the fall sfx and enable the gameover screen
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(fallsfx, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            yield return new WaitForSeconds(2f);
            Instantiate(gameoverscreen, transform.position, Quaternion.identity);
        }
    }
}
