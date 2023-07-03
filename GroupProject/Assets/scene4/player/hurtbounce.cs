using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtbounce : MonoBehaviour
{
    private knockdown knockdownstate;              //getting knockdown script from player
    private playercontrol playercontroller;       // gettting player controller from exactly
    public GameObject shoty,deathsfx,gameoverscreen;    //shoty, death sound effect and gameover screen
    private BoxCollider2D playercollider;              // collider for bounciness etc
    private Rigidbody2D body;               
    public float knockback;                               //strengh of knockback/ how long it takes to get up
     public GameObject punchsfx;              //sfx for when boucning off walls
    private bool bouncing;            //bouncing state 
    private Animator animations;                 //animations for getting up
    private int secondchance=3;                // second chances, aka how many times you can get hit before death
    public Sprite[] deathframes;            //death frames/animation
    private SpriteRenderer playersprite;          // player's sprite, duh
    public bool immune = true,dead=false;            //imune state right after getting knocked, dead state
    public GameObject axe;
    bool recoverimune; // imune right after getting up
    


    private void Start()
    {
       
        playersprite = GetComponent<SpriteRenderer>();
        knockdownstate = GetComponent<knockdown>();
        playercollider = GetComponent<BoxCollider2D>();
        playercontroller = GetComponent<playercontrol>();
        body = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        bouncing = false;
        playercollider.sharedMaterial.bounciness = 0;
   

    }

  public  IEnumerator imunity()
    {
        recoverimune = true;
        for(int a = 0;a<8;a++)
        {
            if(playersprite.color.a == 0)
            {
                playersprite.color = new Color(1, 1, 1, 1);
            }
            else
            {
                playersprite.color = new Color(1, 1, 1, 0);
            }
            yield return new WaitForSeconds(0.25f);
        }
     
        recoverimune = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (  bouncing == true && collision.rigidbody.CompareTag("ground") )      // if player collides with another rigid body he will play the punch sfx
        {
            Instantiate(punchsfx, transform.position, Quaternion.identity);
        }
    }

    IEnumerator deathsequence()                             
    {
        knockdownstate.enabled = false;                          //player will no longer be knocked state, the script will be disabled
        Instantiate(deathsfx, transform.position, Quaternion.identity);      //play death sound
        for(int i = 0; i < 6; i++)             // go through all frames of dying
        {
            playersprite.sprite = deathframes[i];
            yield return new WaitForSeconds(0.1f);
        }
      
        yield return new WaitForSeconds(0.5f);      //wait a lill
        Instantiate(gameoverscreen, transform.position, Quaternion.identity);         // instantiate gameoverscreen
        Destroy(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>());    //destrpy camera controll
        Destroy(gameObject);               //destroy this
        
    }

    public void bounce(Vector2 damagepos)         //bounce, aka the knockback after getting hit, takes 1 arguement which is the position of the source of damage
    {
        if(recoverimune == false)
        {

        
        if(knockdownstate.enabled == true && immune == false) //if we're knockdown and immunity is expired, death comes
        {
            dead = true;
            
            StartCoroutine(deathsequence());
        }

    if(bouncing == true) //if we're still bouncing but get hit, reduce a second chance, when chances ran out knockdown will be forced
        {
            secondchance--;
            if(secondchance <= 0)
            {
                knockdownstate.enabled = true;
             
            }
        }
    if(bouncing == false && dead == false)    // if we werent bouncing nor dead
        {

            immune = true;     // set immune so no instant deaths
        animations.enabled = false;       // no animationns
        bouncing = true;         //bouncing state is set to true
        Instantiate(punchsfx, transform.position, Quaternion.identity);   //punch sfx
        body.constraints= RigidbodyConstraints2D.None;       // remove constrains so player can rotate in z axis
        body.AddTorque(.001f);    // add torque to add a lil bit of spinage, the rotation not the vegetable
        playercontroller.enabled = false;      // no controlls for playboy

            try
            {
                shoty.SetActive(false);
              
            }
            catch
            {
            
                    axe.SetActive(false);                
            
            }
           //no gun
        playercollider.sharedMaterial.bounciness = 1;       //set bounciness to 1 so he bounces
        body.AddForce(new Vector2 (-damagepos.x + .2f, damagepos.y + .2f) * knockback);     // add force in the opposite direction
        StartCoroutine(waitstop());        //now we wait for player to stop moving
        }

        }

    }
    IEnumerator waitstop()
    {
        float timeout=0;   
        while(body.velocity.magnitude > 0 && timeout < 2) //if the ´velocity of the player is rly low or 4 seconds have passed
        {
           
            timeout += Time.deltaTime;
            yield return null;
        }
        transform.rotation = new Quaternion(0, 0, 0,0);  //set player straight
        bouncing = false;        //no longer boucning
        secondchance = 3;       //give him 3 second chances
        knockdownstate.enabled = true;      //knockdown script will now be enabled
        yield return new WaitForSeconds(1);        //wait a second and remove imunity
        immune = false;
    }
}
