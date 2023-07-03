using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class elevator : MonoBehaviour
{
    private playercontrol playercontroller;
    private Animator playeranimation;
    private weapon playergun;
    public GameObject door,doorsfx,blackbackground,elevatorsfx,gameend;
    private camerashake camerashakescript;
    private CameraControl camerachase;

    private AudioSource musicsource;
    //get components from player, his gun, camera ann player animation
    void Start()
    {
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        playergun = GameObject.Find("shoty").GetComponent<weapon>();
        camerashakescript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();
        camerachase = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
         playeranimation = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

    }
    //once player touches collider begin the end sequence
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.CompareTag("Player")) //stops music if it can, sets the players anims and functionalities off and it starts end game
        {
            try
            {
                musicsource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
                musicsource.enabled = false;
            }
            catch{ }
      
        playeranimation.SetFloat("speed", 0);
        playergun.enabled = false;
        camerachase.enabled = false;
        playercontroller.enabled = false;
        StartCoroutine(endinggame());
        }

    }
    IEnumerator endinggame() //this is responsible for doing all the things that happend on screen, ie 
    {
        yield return new WaitForSeconds(1f);
        for(int a = 0; a < 5; a++)
        {
            door.transform.Translate(0, -0.08857174271f, 0);
            yield return new WaitForSeconds(0.02f);

        }
        Instantiate(doorsfx, transform.position, Quaternion.identity); // playing door sound, 
        StartCoroutine(camerashakescript.Shake(.2f, 0.02f)); //shaking cam
        blackbackground.SetActive(true); //enable black background
       
        yield return new WaitForSeconds(1.5f);
        camerachase.enabled = true;   //destroy the camera controll script after a couple seconds
        Destroy(camerachase, 1.5f);
        Instantiate(elevatorsfx, transform.position, Quaternion.identity); //play elevator sound
        for(int i = 0; i < 60; i++)      //and make elevator go up
        {
            transform.Translate(0,0.02f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        gameend.SetActive(true); //and finally iniciate game end screen


    }
}
