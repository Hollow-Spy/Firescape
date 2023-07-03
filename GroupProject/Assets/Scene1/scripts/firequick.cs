using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firequick : MonoBehaviour
{
    public float distance, speed,increasingspeed,timeforincrease;
    public GameObject player,gameoverscreen;
    public IEnumerator start()
    {
       //when this function is called it will make fire rise until it reaches the distance, and it will increase speed through out time
        StartCoroutine(speeding());
        while (transform.position.y < distance)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.1f);

        }
    }
    IEnumerator speeding() // every so often increases speed
    {
        while(transform.position.y < distance)
        {
            yield return new WaitForSeconds(timeforincrease);
            speed += increasingspeed;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //reaches player, it stops moving and show gameover screen
        {
            player.SetActive(false);
            StopCoroutine(start());
            gameoverscreen.SetActive(true);
        }

    }

}
