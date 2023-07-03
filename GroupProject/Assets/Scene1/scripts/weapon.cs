using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    /// <summary>
    /// this script is focused for the shotgun, it might be reused for other weapons that might come along the way, the coldown might be transfered to a coldown manager so that way its easiy to
    /// manage all weapons coldown to avoid weapon spam
    /// </summary>
    
    public GameObject projectile,shotgunsfx,bulletreadyicon; 
    private Transform shotPoint;
    private float shotdelay;
    public float starttimebtwshoots;
    private Quaternion spread;
    private Animator weaponanimator;
    private playercontrol player;
    public float weaponpunch,dashtime;
    private float dashtimeelapsed;
    private IEnumerator dash;
    private bool showicon=false;
    private SpriteRenderer shotgunsprite;
    private void Start()
    {
        shotgunsprite = GetComponent<SpriteRenderer>();
        shotPoint = transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        weaponanimator = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        weaponanimator = GetComponent<Animator>();
        weaponanimator.Play("idle");
    }

    void Update()
    {
        // if the player is mid dash, but decides to move .5 seconds before the dash is complete he will be able to, else he's just gonna do the full dash, really suddle right now might change in the future
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && dashtimeelapsed > dashtime / 2.5 && dash != null)
        {
                dashtimeelapsed = 0;
                StopCoroutine(dash);
           
                player.aircontroller = true;
        }
        //this is all the technical stuff for the shotgun, such as angles etc
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90)
        {
           
            shotgunsprite.flipY = true;
        }
        else
        {
            shotgunsprite.flipY = false;

        }

        if (shotdelay<=0 && showicon == true)
        {
            showicon = false;
            Instantiate(bulletreadyicon, transform.position, Quaternion.identity);
        }
        //if the shotdelay is bellow zero, meaning the reload has been complete he will be able to shoot again
        if(shotdelay <= 0)
        {
           
            if(Input.GetMouseButtonDown(0))
            {
                showicon = true;
                weaponanimator.Play("shooting");
               
                Instantiate(shotgunsfx, transform.position, Quaternion.identity);
                spread = transform.rotation;
                
                for(int i = 0; i < 4; i++)
                {
                    spread = Quaternion.Euler(0f, 0f, rotZ -90 + Random.Range(3,30));
                  
                Instantiate(projectile, shotPoint.position, spread); 
                }
                boost();
                shotdelay = starttimebtwshoots;
               
            }
        }
        else
        {
          
            shotdelay -= Time.deltaTime;
        }
    }

    //if player is airborne and uses the shoot gun the boost will be apllied
    void boost()
    {
        if(player.boostable == true) 
        {
            player.jumptimecounter = 0;
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
    IEnumerator dashcoldown()
    {
        dashtimeelapsed = 0;
        
           while(dashtimeelapsed < dashtime)
        {
            dashtimeelapsed += Time.deltaTime;
            yield return null;
        }
        player.aircontroller = true;

    }
}
