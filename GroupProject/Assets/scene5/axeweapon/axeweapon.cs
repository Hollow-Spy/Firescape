using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeweapon : MonoBehaviour
{
    SpriteRenderer axesprite;
    SpriteRenderer playersprite;
    playercontrol player;
    public float coldown,delay,dashtime,weaponpunch;
    private IEnumerator dash;
    private Animator animator;
    public GameObject slash,bigslash,playerboomerang;
    public Transform axepoint;
    bool attacking;
    float chargeattack = .8f ,dashtimeelapsed;
    
    bool axeless;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        animator = GetComponent<Animator>();
        playersprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        axesprite = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if(axeless == false) //as long as player as the axe, he's able to use the axe attacks
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // rotate the axe arround the mouse pos
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (attacking == false) //if we're not attacking that is, else he'll do a lil attack animation
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            }
            if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90) //flip on y if over the shoulder
            {

                axesprite.flipY = true;
            }
            else
            {
                axesprite.flipY = false;
            }

            if (Input.GetMouseButtonDown(0)) //if player presses and holds m1 it will start charging, when he lets go if enough time has passed he will have a big slash (working but slash have same size need to resize slash anims)
            {
                StartCoroutine(charging());
            }
            if (Input.GetMouseButtonUp(0) && coldown <= 0) //if the weapon cold down is 0 meaning he can use it and we get m1 up
            {
                StopCoroutine(charging()); //stop charging
                attacking = true; //set attacking to true so it stops chasing the mouse rotation for a second
                coldown = delay; //reset the coldown delay
                Quaternion rot = Quaternion.Euler(0f, 0f, rotZ - 180); //using this to fix the spawn for the axe pos (rotation was inverted)
                if (chargeattack > 0)        //check if charge was activated
                {
                    Instantiate(slash, axepoint.position, rot);
                }
                else
                {
                    Instantiate(bigslash, axepoint.position, rot);

                }
                chargeattack = 1;
                StartCoroutine(swipe());

            }

            if(Input.GetMouseButtonDown(1)) //if mouse 2 is used instead we will do the playerrang move
            {
                axeless = true;
                axesprite.enabled = false;
                Instantiate(playerboomerang, axepoint.position,  Quaternion.Euler(0f, 0f, rotZ));
            }

        }
        

         if(coldown > 0) //if coldown is bigger than 0, reduce it gradually
        {
            coldown -= Time.deltaTime;
        }
     


    }
    public void axeback() //when this is called it means the axe is back so its operational again, call in for boost in case player is in mid air
    {
        axesprite.enabled = true;
        axeless = false;
        boost();
        
    }
    void boost() // boost. similar to shotgun dont need to explain
    {
        if (player.onground == false)
        {

            player.aircontroller = false;
            Vector2 aux;
            aux.x = transform.right.x;
            aux.y = transform.right.y;
            aux = aux * weaponpunch * -1;

            player.body.velocity = aux;
            dash = dashcoldown();
            StartCoroutine(dash);



        }

    }
    //dash coldown meaning the player will be stuck in a certain direction for atleast 0.25 seconds

    IEnumerator dashcoldown() //same thing for dash coldown
    {
        dashtimeelapsed = 0;

        while (dashtimeelapsed < dashtime)
        {
            dashtimeelapsed += Time.deltaTime;
            yield return null;
        }
        player.aircontroller = true;

    }
    IEnumerator charging() // chargung function
    {
       while(attacking == false)
        {
            chargeattack -= Time.deltaTime;
            yield return  null;
        }
      
    }
    public IEnumerator swipe() // handles the animation for axe
    {
        int inv;
        if(axesprite.flipY == true)
        {
            inv = -1;
        }
        else
        {
            inv = 1;
        }


        for(int i =0;i < 6; i++)
        {
            transform.Rotate(0, 0, -15 * inv);
            yield return new WaitForSeconds(0.03333333333f);
        }
        attacking = false;
    }
}
