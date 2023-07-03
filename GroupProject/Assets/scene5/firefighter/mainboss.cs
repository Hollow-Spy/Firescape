using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class mainboss : MonoBehaviour
{
    public bool agressive;       // second phase bool
    public GameObject boomerangeaxe, bouncyaxe, swing1, swing2,axe,blocker,debris,punch,kick,throweffect; //references to attacks and sound effects
    public Transform axepoint; // axe point, where the axes will spawn
    public Animator animatorr; 
    public bool axeback;
    Transform playerpos;
    bool grabbed;
    int invthrow;
    public GameObject playerweapon;
    public float bosshealth;
    public healhbarscript healthbar;
    public battetrigger cutscenetriggger;
    bool nodamage = false;
    camerashake camerashaker;
    Camera camerazoom;
    public bool killable=false;
    public GameObject boomerangsfx,bouncysfx,swing1sfx,swing2sfx,punchsfx,chargeimpactsfx,gameend;
    bool dead;
    
   
    public void playsound(string audio) //this function handles sound effects for various attacks
    {
       
        switch(audio)
        {
           
            case "boomerang":
                Instantiate(boomerangsfx,transform.position,Quaternion.identity);
                break;
            case "bouncy":
                Instantiate(bouncysfx, transform.position, Quaternion.identity);

                break;
            case "swingone":
                Instantiate(swing1sfx, transform.position, Quaternion.identity);

                break;
            case "swingtwo":
                Instantiate(swing2sfx, transform.position, Quaternion.identity);
                break;
            case "punch":
                Instantiate(punchsfx, transform.position, Quaternion.identity);
                break;
            case "chargeimpact":
                Instantiate(chargeimpactsfx, transform.position, Quaternion.identity);

                break;
        }
       
    }
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /////////belllow 0 DEATH!!
        if (bosshealth <= 0 && dead ==false)
        {
            dead = true;
            animatorr.SetTrigger("death");

            cutscenetriggger.StartCoroutine(cutscenetriggger.deathphase());
            
        }
        ////////////// anger trigger//////////////// bellow 50% he angy
        if (bosshealth <= .5 && animatorr.GetBool("anger") == false && nodamage == false)
        {
            nodamage = true;
            animatorr.ResetTrigger("axethrow");
            animatorr.ResetTrigger("boomerang");

            animatorr.SetTrigger("point");
            
            cutscenetriggger.StartCoroutine(cutscenetriggger.secondphase());
        }
        ////bullet damage/////////
        if (collision.collider.CompareTag("bullet") && nodamage == false && animatorr.GetBool("anger") == false)
        {
            //0.005f;
            bosshealth -= 0.009f;
            healthbar.StartCoroutine(healthbar.changehealth(0.009f));
        }
        ////taking damage for axe HERE ///////////////
        if (collision.collider.CompareTag("flyaxe"))
        {
            bosshealth -= 0.009f;
            healthbar.StartCoroutine(healthbar.changehealth(0.009f));
            if(killable == true && dead == true)
            {
                gameend.SetActive(true);
                Destroy(collision.gameObject);
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Stop();
              

            }
        }
        if (collision.collider.CompareTag("axeslash"))     // damage for axe slash
        {
            bosshealth -= 0.012f;
            healthbar.StartCoroutine(healthbar.changehealth(0.012f));
        }

        if (animatorr.GetBool("charge") == true) // if charge is true
        {
            if (collision.collider.CompareTag("Player") && playerpos.position.y < transform.position.y + 0.448f) //and collides with player, and player is bellow the head height
            {
                int inv;
                if(transform.eulerAngles.y == 180)    //check if boss is inverted
                {
                    inv = -1;
                }
                else
                {
                    inv = 1;
                }
                //call bounce function depending on inverted direction
                playerpos.GetComponent<hurtbounce>().bounce(new Vector2(axepoint.position.x * inv,axepoint.position.y) );

                animatorr.SetBool("charge", false); //no longer charging

                animatorr.SetTrigger("chargehit");         //play hit animation
            }
            if (collision.collider.CompareTag("ground"))  // else, if he hits a wall
            {
               
                animatorr.SetBool("charge", false);              //do the same but play earthqquake instead
                animatorr.SetTrigger("chargehit");
                StartCoroutine(earthquake());

            }
        }
       

    }

   

    IEnumerator earthquake() //earth quake, spawn fire from ceiling and shake camera
    {
        camerashaker.StartCoroutine(camerashaker.Shake(0.5f, 0.1f));
        for(int i = Random.Range(4,5); i > 0; i-- )
        {
            Instantiate(debris, new Vector2(Random.Range(30.23f, 31.842f),0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.1f ,0.3f));
        }

        camerazoom.orthographicSize = 0.6216953f;
        camerazoom.transform.position = new Vector3(30.93f, -0.98f, -3f); //reset camera
    }



    public void pointsfx()
    {
        cutscenetriggger.begincutscene();      //point sfx, initiate second phase cutscene
    }

    private void Start()
    {
        camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animatorr = gameObject.GetComponent<Animator>();
        camerashaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();
        playerpos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))    //if player walks into range, set range to true
        {
            animatorr.SetBool("range", true);


        }
        if(collision.CompareTag("flyaxe"))             //if axe comes into range, set block to true
        {
            animatorr.SetBool("block", true);
        }
    }
    public void spawnblock()             // spawn block
    {
        Instantiate(blocker, axepoint.position, Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D collision) //if player leaves range, both axe attack reset and range is false
    {
        if(collision.CompareTag("Player"))
            {
            animatorr.ResetTrigger("axerange1");
            animatorr.ResetTrigger("axerange2");
            animatorr.SetBool("range", false);
        }
      

    }

     public void grabbing() //grabbing!!!!
    {
        axepoint.position = new Vector3(axepoint.transform.position.x, axepoint.transform.position.y, 0); //make sure axe position is on the 0 z axis
        float difference = Vector3.Distance(playerpos.position, axepoint.position);    //distance between player and axe position
        
        if ((difference <= 0.12))  //if difference is small
        {
            if(transform.rotation.eulerAngles.y == 180)  //check bosses rotation
            {
                invthrow = -1;
            }
            else
            {
                invthrow = 1;
            }

            animatorr.SetBool("grabbed", true); //enable grabbed anim
            GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>().enabled = false; //disable player controll
            try
            {
                playerweapon.SetActive(false); //disable shotgun/axe
            }
            catch
            {
                axe.SetActive(false);           
            }
          
            
            StartCoroutine(throwplayer()); //thow player

        }
       
      
    }
    public void thrown() //the moment he gets thrown, increase knockback and throw player 
    {
        animatorr.SetBool("grabbed",false);
        hurtbounce hurtscript = GameObject.FindGameObjectWithTag("Player").GetComponent<hurtbounce>();
        hurtscript.knockback = 0.035f;
        hurtscript.bounce(new Vector2(axepoint.position.x *invthrow,axepoint.position.y));
        hurtscript.knockback = 0.01f;
    }

    IEnumerator throwplayer() 
    {

        while(animatorr.GetBool("grabbed")  == true)   //set animator grabbed to true, and keep player position to axepoint position, aka hand
        {
            playerpos.position = axepoint.position;
            yield return null;
        }
    }

   public void swing(int i) // spawns the slash or punches animations
    {
        float flipajust;         //ajust the swing slash according to rotation
        if(transform.eulerAngles.y == 180)
        {
            flipajust = 0.15f;
        }
        else
        {
            flipajust = -0.15f;     

        }

        switch (i) // decide which damage slash to spawn
            {
            case 1:
               
                Instantiate(swing1, new Vector3( axepoint.position.x + flipajust, axepoint.position.y, transform.position.z), Quaternion.Euler(Quaternion.identity.x, gameObject.transform.eulerAngles.y,Quaternion.identity.z) );
                break;
            case 2:
                Instantiate(swing2, new Vector3(axepoint.position.x + flipajust *1.2f, axepoint.position.y, transform.position.z), Quaternion.Euler(Quaternion.identity.x, gameObject.transform.eulerAngles.y, Quaternion.identity.z));

                break;

            case 3:
                Instantiate(punch, new Vector3(axepoint.position.x + flipajust * 1.2f, axepoint.position.y, transform.position.z), Quaternion.Euler(Quaternion.identity.x, gameObject.transform.eulerAngles.y, Quaternion.identity.z));

                break;
            case 4:
                Instantiate(kick, new Vector3(axepoint.position.x + flipajust * 1.2f, axepoint.position.y, transform.position.z), Quaternion.Euler(Quaternion.identity.x, gameObject.transform.eulerAngles.y, Quaternion.identity.z));

                break;
            case 5:
                Instantiate(throweffect, new Vector3(axepoint.position.x + flipajust * 1.2f, axepoint.position.y, transform.position.z), Quaternion.Euler(Quaternion.identity.x, gameObject.transform.eulerAngles.y, Quaternion.identity.z));

                break;
        }


    }
    public void boomerangatack() // boomerang attack, aka back and forth
    {
     
        Transform axepos;
      //instantiae a boomerang at axepoint
        axepos = Instantiate(boomerangeaxe, new Vector3(axepoint.position.x, axepoint.position.y + 0.112f, axepoint.position.z),Quaternion.identity).GetComponent<Transform>();
   //player axeless during this time
        animatorr.SetBool("axeless", true);
        //wait for axe now
        StartCoroutine(waitforaxe(axepos));
    }
    public void bounceattack() //if bouncy axe attack
    {
        Instantiate(bouncyaxe, axepoint.position, Quaternion.identity);
    }
   public void recievedaxe() //when axe is recieved
    {
        animatorr.SetTrigger("recieve");
        animatorr.SetBool("axeless",false);
    }
    IEnumerator waitforaxe(Transform axepos) //wait for axe of boomerange to return
    {
        Vector2 handpos = new Vector3(axepoint.position.x + 0.253f, axepoint.position.y);
        while (axeback == false)
        {
            yield return null;
        }
        axeback = false;
        animatorr.SetBool("axeless", false);
        animatorr.SetTrigger("recieve");
    
        
      
    }
   
}
