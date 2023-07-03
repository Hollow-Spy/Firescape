using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikescript : MonoBehaviour
{
    public GameObject bloodsfx,gameoverscreen,blackscreen;
    private GameObject player;
    //simple spike script, player hits the spike, plays gruesome death sfx and a while later death screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);
            Instantiate(blackscreen, transform.position, Quaternion.identity);
            Instantiate(bloodsfx, transform.position, Quaternion.identity);
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.9f);
        Instantiate(gameoverscreen, transform.position, Quaternion.identity);
    }
}
