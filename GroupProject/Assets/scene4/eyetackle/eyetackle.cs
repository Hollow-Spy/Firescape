using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyetackle : MonoBehaviour
{
    public bool range = false, animating = false;
    public string state = "grounded";
    public Animator animations;
    public SpriteRenderer eyesprite;
    private Collider2D playercollision;
    public eyehitbox hitbox;
    private camerashake shake;
    private CameraControl smooth;
    public GameObject spawnsfx, attacksfx;
    public AnimationClip attackclip,spawnclip;
    ///
    /// <summary>
    /// first of all i just wanted to say i love this script, it took me probably arround 6 hours to make it, i was so mad when i was stuck in a loop, because i realized the method i was using was counting animation time and it would stack so it would give me all kinds of issues, i was on the verge of giving up and leaving it as it was but thank god i pushed through
    /// So, the idea is that once the players steps into the range, it will set range = true, call behaviour, behaviours then will act accordingly, if the eye is burried
    /// it will unburried it, if not then it's idle, and if idle and not animating, aka attacking he will attack, if player is on the left of the eye he wont flip but 
    /// if he he is he flips 
    /// behaviour is called after the end of every attack, if the player is still in the range good, that mean he'll attack again otherwise he'll just do his idle animation
    /// </summary>

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();
        smooth = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
    }
    void checkdirection() //whenever attacking flip the character
    {   
    
     
        if (playercollision.transform.position.x < transform.position.x)   
        {
            eyesprite.flipX = false;
            hitbox.attackrotate(-1);
        }
        else
        {
            eyesprite.flipX = true;
            hitbox.attackrotate(1);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //if player is in range get his components and set range to true, call in behaviour
    {
        if (collision.CompareTag("Player"))
        {
            playercollision = collision;
            
            range = true;
     
                behaviour();


        }
    }
    private void OnTriggerExit2D(Collider2D collision)   //player leaves, range false
    {
        if (collision.CompareTag("Player"))
        {
            range = false;

        }
    }


    IEnumerator waitanimation()    //this quortine handles animations and makes sure it waits them out before attacking out in behaviour again
        {
      
            animating = true;      //set animating to true 
            

            if (state == "grounded")  //if in ground, then spawn and call the hitbox to move up with it
            {
            Instantiate(spawnsfx, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            hitbox.moveup();   //function to move hitbox up
            yield return new WaitForSeconds(spawnclip.length-0.3f); //wait the lengh of the clip and set state to idle
                state = "idle";
            }
            else //else, if hes not grounded meaning he could be idle
            {
            Instantiate(attacksfx, transform.position, Quaternion.identity); //attack sound
            checkdirection();   //check direction
            yield return new WaitForSeconds(0.8f);    //wait a lil
            smooth.smoothTime = 0.1f;                   //shake camera and lessen smooth time of controll camera
             StartCoroutine( shake.Shake(.1f, .06f));
            yield return new WaitForSeconds(0.1f); 
            smooth.smoothTime = 0.3f;
            yield return new WaitForSeconds(attackclip.length-.9f);    // wait the rest of the lengh
            }
                animating = false;   // animating is set to false indicating its no longer animating and is ready to act out again
                  behaviour();  // call behaviour in order to decide what to do next
    }
  

public void behaviour()    //main behaviour script
    {
        
        if (range == false && animating == false)  //if neither the player is in range nor is the eye animating, meaning he's atacking, simply play idle
        {
            animations.Play("idle");
        }

        if (range == true && animating == false) //if range is true and animating false, we check

        {
            if(state == "grounded")   //if he's still on the ground, if yes then play spawn animation using courtine
            {
                animations.Play("spawn");
                StartCoroutine(waitanimation());
            }
            
            if(state == "idle")  //or if he has spawned and is just in idle, play attack and start courtine
            {
            
               
                animations.Play("attack");
                StartCoroutine(waitanimation());
               
            }
        }
    
    }

}
