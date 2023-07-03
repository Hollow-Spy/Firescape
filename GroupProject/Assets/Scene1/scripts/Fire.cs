using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
  
    public float distance;
    public float speed;
   
    public groundscript ground;
    public GameObject playerbody;
    public Rigidbody2D body2;
    public CameraControl camerascript;
    public Animator animator;
    public weapon shoty;
   // public Collider2D collisions;
    private AudioSource musicsource;
    public GameObject gameoverscreen,fallingsfx;
  
    public IEnumerator start() //first fire script, simple go up until it reaches the distance
    {
        while(transform.position.y < distance)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.1f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //if it touches the player it will pause music and will start the fallig animation with sfx
    {
      
        
        if(collision.CompareTag("Player"))
        {
            try
            {
                musicsource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
                musicsource.Pause();
            }
            catch
            {

            }
            
            ground.enabled = false;
            animator.enabled = false;
           // collisions.enabled = false;
            shoty.enabled = false;
            body2.gravityScale = 0.3f;

            Instantiate(fallingsfx, transform.position, Quaternion.identity); //sound effect and gravity change
            StartCoroutine(falling());
            Destroy(camerascript, 1.4f);
           
        }
        
      
    }

    IEnumerator falling() //rotation ienumerator
    {
        float i=0;
        while(i<400)
        { 
        playerbody.transform.rotation = Quaternion.Euler(0,0,i);
            i += 2f;
            yield return new WaitForSeconds(0.001f);
        }
        gameoverscreen.SetActive(true);
    }

}
