using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public Animator animations;
    public  float jumpforce,jumptime,jumptimecounter; //variable for jump height
    public bool onground,isjumping,boostable;      //check if he's on ground
    [SerializeField] float speed = 0; //player speed
    public Rigidbody2D body; // player's body
    SpriteRenderer sprite;  // player image render
    public GameObject charactersize,dustparticles,landdustparticles,jumpsfx; // player scale variable
    bool lookingright = true; // is player looking right
    public bool aircontroller = true; //able to controll movement midair
    private gamemaster gamemastercontroll; //variable for checkpoints, never destroys on reload
    public bool bufferjump; //how soon can player jump again
  
    private void Start()
    {
        body = GetComponent<Rigidbody2D>(); //getting the properties from the player's body
        
        jumptime += PlayerPrefs.GetFloat("jumpheight"); // if banana was bought increase jump time resulting in a possible higher jump
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);

        sprite = GetComponent<SpriteRenderer>();
        try
        {
            gamemastercontroll = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
       
        if( gamemastercontroll.checkpoint != Vector3.zero)
        {

            transform.position = gamemastercontroll.checkpoint;
        }
        }
        catch { }
    }

    private void FixedUpdate() //constantly calling move
    {
        move();
     
    }

    private void move()
    {
        float movement = Input.GetAxisRaw("Horizontal");                                        //player movement
        if (movement == -1 && lookingright == true)
        {
            flip();
        }
        else
        {
            if (movement == 1 && lookingright == false)
            {
                flip();
            }

        }
        if(aircontroller == true) //being able to switch between no air controll and air controll giving player controll mid air unless he uses shoty
        {
            body.velocity = new Vector2(movement * speed, body.velocity.y);
            animations.SetFloat("speed", Mathf.Abs(movement));
        }
          //sets animations based on player speed
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boostable = true;
        }
       
        
        //jump function
        if ((Input.GetKeyDown(KeyCode.Space)  || bufferjump == true) && onground == true)
        {
            bufferjump = false; //if we're jump disable buffer quickly while rising
            body.velocity = Vector2.up * jumpforce;   //add the velocity

            Instantiate(dustparticles,new Vector3(transform.position.x, transform.position.y - 0.013f,transform.position.z), Quaternion.identity); //particles
            Instantiate(jumpsfx, transform.position, Quaternion.identity); //sound
            animations.SetBool("jumping", true); //set anim to jump
            onground = false; //no longer on ground
            jumptimecounter = jumptime; //jump height controller is just to jump time
            isjumping = true; // is jumping is set to true and make it squish
            StartCoroutine(jumpsquish()); 
        }
        if (Input.GetKey(KeyCode.Space) && isjumping == true && aircontroller == true) //height controll
        {

            if(jumptimecounter > 0)
            {
                boostable = true;
                body.velocity = Vector2.up * jumpforce;
                jumptimecounter -= Time.deltaTime;
               
            }
            else
            {
                StartCoroutine(jumppeakgravity());
                isjumping = false;
             
            }

            

            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isjumping = false;
        }
    }
    IEnumerator jumppeakgravity() //the jump peak gravity changer
    {
        body.gravityScale = body.gravityScale / 2;
        yield return new WaitForSeconds(.2f);
        body.gravityScale = body.gravityScale * 2;
    }


    private IEnumerator jumpsquish()   //advanced jump squish function, eye candy :) 
    {
        Vector3 aux;
        for (int i = 0; i < 9; i++)
        {
            aux = charactersize.transform.localScale;
            aux.x -= 0.025f;
            charactersize.transform.localScale = aux;
            yield return new WaitForSeconds(0.02f);

        }
        for (int i = 0; i < 9; i++)
        {
            aux = charactersize.transform.localScale;
            aux.x += 0.025f;
            charactersize.transform.localScale = aux;
            yield return new WaitForSeconds(0.02f);

        }
        charactersize.transform.localScale = new Vector3(1, 1, 1);
    }
    
    void flip()      //flip the character
    {
        lookingright = !lookingright;
        sprite.flipX = !sprite.flipX;
    }
}
