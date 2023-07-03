using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boucyaxescript : MonoBehaviour
{
    Rigidbody2D body;
  
    public float speed;
    public GameObject sparkles, hitsfx;
   public int collisions=15;
    mainboss bossscript;
    private CircleCollider2D hitbox;
    float startingspeed;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        float x = 1;
        float y = 1;
        bossscript = GameObject.FindGameObjectWithTag("boss").GetComponent<mainboss>();
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("boss").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>(), true);
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>(), true);
        hitbox = GetComponent<CircleCollider2D>();
        body.velocity = new Vector2(x * speed, y * speed);
        startingspeed = body.velocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("ground")) //if hits ground, reduce collision number and make particles and sound
        {

            collisions--;
           
            Instantiate(hitsfx, transform.position, Quaternion.identity);
            if (collision.collider.transform.position.x < transform.position.x)
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(180, -90, -90));
            }
            else
            {
                Instantiate(sparkles, transform.position, Quaternion.Euler(0, -90, -90));

            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //if collides with player hurt him
        {
            collision.GetComponent<hurtbounce>().bounce(transform.position);
        }
    }

    // Update is called once per frame
    void Update() // if he hits the max number of collision hits return to boss
    {
        if(body.velocity.magnitude < startingspeed)
        {
            collisions = 0;
        }
        if(collisions <= 0)
        {
            body.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position,bossscript.axepoint.position ,speed * Time.deltaTime);
            if(hitbox.bounds.Contains(bossscript.axepoint.position))          //as soon as his hitbox contains axepos, delete and tell the boss he got the axe
            {
                GameObject.FindGameObjectWithTag("boss").GetComponent<mainboss>().recievedaxe();
                Destroy(gameObject);
            }
        }
    }
}
