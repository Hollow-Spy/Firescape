using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrang : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    CircleCollider2D hitbox;
    bool returning;
    public GameObject sparkles, hitsound;
    bool touched;
    float originalspeed;
    Transform player;
     axeweapon weapon;
    float timeout;
    void Start()
    {
        originalspeed = speed;        //pre record original speed so when he comes back it goes same speed
        weapon = GameObject.FindGameObjectWithTag("axe").GetComponent<axeweapon>();
        body = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitbox = gameObject.GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(hitbox, GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>());
       
        StartCoroutine(traveltime());  //initiate travel
    }
   
    IEnumerator traveltime()
    {
        yield return new WaitForSeconds(0.15f); //wait a lil bit 
        if(returning == false) //if he's not returning yet aka didnt hit ground, meaning he's still going foward, divide velocity by 3
        {
            speed = speed / 3;

        }
        yield return new WaitForSeconds(0.1f); //wait a split second
        if(returning == false) //if its still not returning, set speed back and start returning
        {
            speed = speed * 3;
            returning = true;
           StartCoroutine(returnaxe());
        }
    }
        IEnumerator returnaxe() //returning, tracks player down and it goes towards him, as soon as he hits him he gets his axeback
        {
        
        float difference=1; 
        while (touched == false && difference >= 0.0001f) // while touched is false AND difference is bigger than 0.00001 meaning the axe is still far or hasnt touched the player
        {
             difference = Vector3.Distance(transform.position, player.position); //calculate difference of distance between player and axe
               

            speed = originalspeed; // speed will be set to original speed
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //move towards player

            timeout += Time.deltaTime; //time out, in case axe gets stuck when time out is up, instantly recall axe
            if(timeout > 1.2f)
            {
                touched = true;
                difference = 0;
            }
        }
        weapon.axeback();  // when finally axe gets back, tell the weapon the axe is back
        Destroy(gameObject); //destroy this object

        }
    private void Update()
    {
        if(returning == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); //while returning is false, keep moving foward

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && returning == true) // if he collides with player and returning is set to true, touched is true which means the axe will shoty dissapear
        {
            touched = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //if he collides with the ground
    {

        if (collision.collider.CompareTag("ground")) //returning is set to true meaning he'll start going back to player
        {

          
            returning = true;
            
            Instantiate(hitsound, transform.position, Quaternion.identity); //hit sound is played
            if (collision.collider.transform.position.x < transform.position.x) //do sparkles
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(180, -90, -90));
            }
            else
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(0, -90, -90));

            }
            StopCoroutine(traveltime()); //stop the travel time courtine
            StartCoroutine(returnaxe()); //start the return axe one
             
        }
       
    }

}
