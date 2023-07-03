using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockdown : MonoBehaviour
{
    /// <summary>
    /// this script completly disables the player's controlls and makes them have to spam the space button in order to get up, very versatile, might be used in future scenes
    /// </summary>
    public SpriteRenderer sprite;
    public Sprite[] frames = new Sprite[2];
    public float knockstrengh;
    float recover;
    public GameObject space;
    GameObject objectspace;
    public GameObject weapon,recoversfx,wakeupsfx,redscreen, axe;
    public playercontrol playercontroller;
    public Animator animations;
    private Rigidbody2D body;
    private BoxCollider2D colliderplayer;
    private float oldknockstrengh;
    AudioSource  recoveraudio;
    redscript redscreenscript;
    hurtbounce hurtscript;

  
    private void Start()
    {
        hurtscript = GetComponent<hurtbounce>();
        body = GetComponent<Rigidbody2D>();
        colliderplayer = GetComponent<BoxCollider2D>();
      
    }
    //if disabled make sure player is in the RIGHt Z axis
    private void OnDisable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        hurtscript.StartCoroutine(hurtscript.imunity()); // give player 2 secs of immunity on hurt script (reference)
    }
    private void OnEnable() //when enable aka called
    {
        GameObject aux;
        aux = Instantiate(redscreen, transform.position, Quaternion.identity); // instantiate red screen and attached a variable aux to it so we can delete it later
        redscreenscript = aux.GetComponent<redscript>();     //redscreen script from instantantiated object
        oldknockstrengh = knockstrengh;   //set old knockdown to current one
        recover = 0; //set players recovery to 0
       recoveraudio =  Instantiate(recoversfx, transform.position, Quaternion.identity).GetComponent<AudioSource>(); //instantiate audio and attrribute it to varialbe

        if(PlayerPrefs.GetInt("recovery")  == 1) //if recovery power up is in play, make knock strengh 50% smaller
        {
           
            knockstrengh = knockstrengh / 1.5f;
        }
        
        animations.enabled = false;    //disable anims and player controll, and set the space spam object above players head
        playercontroller.enabled = false;
         objectspace = Instantiate(space, transform.position + new Vector3(0,.1f,0), Quaternion.identity);
        try   //try to disable axe,gun
        {
            weapon.SetActive(false);

        }
        catch
        {

            axe.SetActive(false);
 
        }
    }

  

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)) //if player presses pace, add recovery and increase the pitch of the recover audio and decrease volume
        {
            recover += .7f;
            recoveraudio.pitch += 1.5f / knockstrengh;
            recoveraudio.volume -= .001f;
            
        }

        if (recover < knockstrengh / 2) //if recover is smaller than 50% of the knockdown strengh set it the anim to first frame
        {

            sprite.sprite = frames[0];
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (recover > knockstrengh / 2) // is we're over half of the knockstrengh, change to frame 1 to give off feedback that the player is trying to get up
        {

            sprite.sprite = frames[1];
        }
        if(recover > knockstrengh)  // if recover is higher than kncok strengh, gethim up
        {
            redscreenscript.fadeout();    
            Destroy(recoveraudio);                            
           
            Instantiate(wakeupsfx, transform.position, Quaternion.identity);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliderplayer.sharedMaterial.bounciness = 0;         
            DestroyImmediate(objectspace);
            try
            {
                weapon.SetActive(true);

            }
            catch
            {

                axe.SetActive(true);

            }
            playercontroller.enabled = true;
            animations.enabled = true;
            knockstrengh = oldknockstrengh;         //set knockout strengh back to the old one
            knockstrengh += 2;                          //increase knockdown strengh
            this.enabled = false;          //disable this
        }
        recover -= Time.deltaTime;    //every update decrease recover by time delta time
        if(recoveraudio.pitch >0)          // if the pitch is pigger than 0, decrease pitch and increase volume
        {
            recoveraudio.pitch -= .25f * Time.deltaTime;
            recoveraudio.volume += .05f * Time.deltaTime;
        }
       
        if (recover < -2)             //if recover is bellow -2, keep it at that
        {
            recover = -1;
        }
        
    }
}
