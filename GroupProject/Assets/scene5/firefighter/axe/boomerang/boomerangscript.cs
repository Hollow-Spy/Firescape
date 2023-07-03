using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerangscript : MonoBehaviour
{
    Rigidbody2D body;
    CircleCollider2D hitbox;
    public float speed;
    bool returning,slowdown;
    public GameObject sparkles, hitsound;
    mainboss bossscript;
   
    Transform playerpos;
    float x, y, z; // will take a picture of the player's position before axe starts moving


    void Start()
    {

         playerpos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        x = playerpos.position.x;
        y = playerpos.position.y;
        z = playerpos.position.z;
    
        bossscript = GameObject.FindGameObjectWithTag("boss").GetComponent<mainboss>();
        
        body = gameObject.GetComponent<Rigidbody2D>();
       
        hitbox = gameObject.GetComponent<CircleCollider2D>();  
        Physics2D.IgnoreCollision(hitbox, GameObject.FindGameObjectWithTag("boss").GetComponent<BoxCollider2D>()); //ignore bosse's hitbox

    }
    IEnumerator wait() //move a lil bit foward after hitting the picutred position before returning
    {
        slowdown = true;  
        body.velocity = new Vector2(speed / 3 * -1,-0.1f);
        yield return new WaitForSeconds(0.7f);
        returning = true;
        body.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if(returning == false && slowdown == false) //while not returning keep moving towards the saved player pos
        {
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(x,y) , speed * Time.deltaTime);
            if(hitbox.bounds.Contains(new Vector3(x,y,transform.position.z))) //if it reaches that, start slowing down
            {
                StartCoroutine(wait());
            }
        }
        else
        {
            if(returning == true) // if returning is actually true, move towards the boss, if he hits him, tell boss axe is back and destroy this
            {
                transform.position = Vector3.MoveTowards(transform.position, bossscript.axepoint.position, speed * Time.deltaTime);

                if (hitbox.bounds.Contains(bossscript.axepoint.position))
                {
                    bossscript.axeback = true;
                    Destroy(gameObject);
                }
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //enter on player tho and cause sum damage
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<hurtbounce>().bounce(transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("ground")) //if it hits ground, change speed so it's inverted, set return to true, do sounds and sparks
        {

            speed = speed * -1;
            returning = true;
            Instantiate(hitsound, transform.position, Quaternion.identity);
            if(collision.collider.transform.position.x < transform.position.x)
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(180, -90, -90));
            }
            else
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(0, -90, -90));

            }
           
          
        }
    }
}
