using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavescript : MonoBehaviour
{
    public GameObject gameoverscreen,deathsfx;
    GameObject player;
    public float distance, speed, increasingspeed, timeforincrease;
   public void startmoving() //when this function is called start the wave couroutine
    {

        StartCoroutine(start());
    }

   
     IEnumerator start() //enable speeding for wave, and start moving it from left to right
    {

        StartCoroutine(speeding());
        while (transform.position.x < distance)
        {
            transform.Translate(speed * Time.deltaTime,0 , 0);
            yield return new WaitForSeconds(0.05f);

        }
    }
    IEnumerator speeding() //every so seconds as long as we're not at the destination increase the wave speed
    {
        while (transform.position.x < distance)
        {
            yield return new WaitForSeconds(timeforincrease);
            speed += increasingspeed;
        }

    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision) //player enters the hitbox and he dies playing a funny water drop sfx 
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(deathsfx, transform.position, Quaternion.identity);
         
            collision.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            StopCoroutine(start());
            Instantiate(gameoverscreen, transform.position, Quaternion.identity);
        }

    }

}

