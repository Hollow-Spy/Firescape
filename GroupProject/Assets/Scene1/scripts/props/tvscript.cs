using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvscript : MonoBehaviour
{

    public GameObject space,dialogobject;
    public dialogmanager dialoguescript;
    public GameObject tvcutscene, video,blackscreen,fadein;
    bool playerin,once=true;
    private playercontrol player;
    private Camera camerazoom;
    public weapon shoty;
    private footsteps footstepnoise;
    private void Start()
    {
        footstepnoise = GameObject.FindGameObjectWithTag("Player").GetComponent<footsteps>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.tag == "Player")
        {
           
            space.SetActive(true);
            playerin = true;

        }
    }
    //if player is inside the collider and preses space, start the zoom, fade in and enable the tv cutscene, also begin dialog number 0
    private void Update()
    {
        if(playerin == true && Input.GetKeyDown(KeyCode.Space) && once == true)         
        {
            once = false;
            player.enabled = false;
            shoty.enabled = false;               //disable playercontroll,shotgun,turnblackscreen on, start tv cutscene and wait a small second
            dialogobject.SetActive(true);
            tvcutscene.SetActive(true);
            blackscreen.SetActive(true);
            StartCoroutine(waitsmall());

        }
        if(dialoguescript.done == true) //if dialogue is done, reset back to normal
        {
            
            Instantiate(fadein, transform.position, Quaternion.identity);
            space.SetActive(false);
            player.enabled = true;
            tvcutscene.SetActive(false);            //disable tv cutscene, start fade in, disable space,  enable player movement, shoty on, footstep noise, and reset camera, disable dialogue and destroy this
            shoty.enabled = true;
            footstepnoise.enabled = true;
            for (int i = 0; i < 20; i++)
            {
                camerazoom.orthographicSize += 0.02f;
            }

            dialogobject.SetActive(false);
            Destroy(this);
        }
    }


    IEnumerator waitsmall()
    {
        for(int i = 0; i < 20; i++)         //wait a small second after zooming and start dialogue, vido and disable black screen
        {
            camerazoom.orthographicSize -= 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(.5f);
        dialoguescript.begindialog(0);
        video.SetActive(true);
        blackscreen.SetActive(false);

    }
    private void OnTriggerExit2D(Collider2D collision)       //disable space and player when he leaves
    {
        if(collision.CompareTag("Player"))
        {
            playerin = false;
            space.SetActive(false);
        }
    }
}
