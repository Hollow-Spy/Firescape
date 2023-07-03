using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupaxe : MonoBehaviour
{
    bool inside;
    public GameObject space,liftsfx,axeweapon;
    private playercontrol player;
    public weapon shoty;
    
    Animator playeranims;
    SpriteRenderer playersprite;
    public Sprite liftframe;
    public Transform snappos;
    int remaining=10;
    public Transform axeposition;
    public knockdown knockdownscript;
    private void Start()
    {
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        playeranims = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playersprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        transform.position = new Vector2 (axeposition.position.x, -1.081f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
            space.SetActive(true);
            StartCoroutine(liftbro());
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shoty.gameObject.SetActive(true);
            inside = false;
            space.SetActive(false);
            shoty.enabled = true;
         
        }
    }
    IEnumerator liftbro()
    {
       
        while (inside == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && knockdownscript.enabled == false)       //of player is next to the axe and not knocked down and presses space
            {
                player.enabled = false;       //disable controll & shot
                try
                {
                    shoty.gameObject.SetActive(false);
                }
                catch
                {

                }
                
                Instantiate(liftsfx, transform.position, Quaternion.identity); //make sound and disable anims
                playeranims.enabled = false;
               

                player.gameObject.transform.position = snappos.position; //snap him to position and change his sprite
                playersprite.sprite = liftframe;
               
                remaining--;  //reduce remanining spaces
                if(remaining <= 0)
                {
                    remaining = 10;
                    ///////// switch weapons
                    Destroy(shoty.gameObject);
                    axeweapon.SetActive(true);
                    playeranims.enabled = true;
                    player.enabled = true;



                    Destroy(gameObject);
                   
                }
              
            }
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) //if trys to move left or right turn controll back, to avoid being stuck there and dying
            {
              
                shoty.gameObject.SetActive(true);
                shoty.enabled = true;

                playeranims.enabled = true;
                player.enabled = true;
            }
           
            yield return null;
        }
    }
}
