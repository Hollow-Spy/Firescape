using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Tilemaps;

public class stairslidescript : MonoBehaviour
{
    public GameObject slideparticles;
   
    GameObject particles;
    private playercontrol player;
    private Rigidbody2D playerbody;
    private CameraControl camerascript;
    private Camera camerazoom;
    private bool active=false,onetime=true;
    private AudioSource musicsource;

    //this script handles the slide on the stairs for level 1
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        playerbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        camerascript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
     


    }
    private void OnTriggerEnter2D(Collider2D collision) //when player enters the zoom changes music becomes louder, player cant move, only use the weapon and his velocity is stuck so he's going down
    {
        if(collision.CompareTag("Player") && onetime== true) 
        {
            onetime = false;
            camerazoom.orthographicSize = .6f;
            musicsource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
            musicsource.volume = musicsource.volume * 2;
            player.enabled = false;
            camerascript.smoothTime = .1f;
            playerbody.velocity = new Vector2(.5f,0);
             particles = Instantiate(slideparticles, transform.position, Quaternion.identity); // display dust particles
            active = true;   //while active is true keep these particles attached to the player's feet
        }
       

    }
    private void Update()
    {
        if(active == true)
        {
            particles.transform.position = new Vector2(playerbody.transform.position.x-0.01f,playerbody.transform.position.y-.028f);
        }
    }
    private void OnDestroy()
    {
        try
        {
            camerazoom.orthographicSize = 0.5665228f; //when destroyed, reeset camera zoom , the smooth time, destroy particles and enable the player mov again
            camerascript.smoothTime = .3f;

            player.enabled = true;
            Destroy(particles);

        }
        catch
        {

        }
       
        
    }
}
