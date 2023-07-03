using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battetrigger : MonoBehaviour
{
    bool once = true; //activate trigger only once
    public dialogmanager dialoguefire; //reference to dialogue manager
    Camera camerazoom;    // camera reference to use for zooms
    CameraControl cameratarget; // refernce to camera control
    mainboss bossscript;     //reference to main boss script
   
    bool readtyforcutscene;      // bool for being ready for cutscene
    public GameObject impactsfx,axehitsfx,knucklesfx, pickableaxe,healthbar;   // refence to multiple objects, impact sound, axe sound, knuckle sound, pickalbe sound, and health bar
    public weapon shoty; //reference to weapon
     playercontrol player; 
    public AudioClip bossmusic,afterbossmusic;      

    private IEnumerator OnTriggerEnter2D(Collider2D collision) 
    {
        if(once== true && collision.CompareTag("Player")) //if its the player colliding with trigger and once is true
        {
            
            once = false;      // no further triggers 
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>(); //get all references
             camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
             cameratarget = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
            cameratarget.enabled = false;
            camerazoom.orthographicSize = 0.6216953f;      //zoom in
            camerazoom.transform.position = new Vector3(30.93f, -0.98f,-30f); //fix camera

            dialoguefire.begindialog(Random.Range(4,10));     // being a random dialogue between these ranges
            while(dialoguefire.done == false)     // when dialogue is done
            {
                yield return null;
            }
             bossscript = GameObject.FindGameObjectWithTag("boss").GetComponent<mainboss>(); //set boss to agressive
            bossscript.agressive = true;
            healthbar.SetActive(true);     //enalbe boss hp
            try
            {
                GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().clip = bossmusic;   //try to play boss music
                GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
            }
            catch
            {

            }

        }
    }
  


    public void begincutscene() //start cutscene 
    {
        Instantiate(impactsfx, transform.position, Quaternion.identity);   
        
        readtyforcutscene = true;
        
    }
   public IEnumerator secondphase()   // second phase cutscene.
    {
        player.enabled = false;
        shoty.enabled = false;
        cameratarget.enabled = true; 
        healthbar.SetActive(false);              //freeze player, disable shotgun, put camera focused on boss


        Transform aux;     
        aux = bossscript.axepoint;                  //aux position is for saved to teleport the pickable axe to
        aux.position = new Vector3(aux.position.x, aux.position.y, -30);  

        cameratarget.target = aux;                     //set camera target to aux
        
        for (int c =0; c < 10; c++)       //zoom in
        {
           
            camerazoom.orthographicSize -= 0.02665228f;
            yield return new WaitForSeconds(0.01f);

        }
        
        while(readtyforcutscene == false)    //wait while not ready fot cutscene
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);    
        dialoguefire.begindialog(11);               //start dialogue after point animation
        while(dialoguefire.done == false)     // when dialogue done
        {
            yield return null;
        }
        player.enabled = false;
        shoty.enabled = false;           // keep player and shot gun disabled
        bossscript.animatorr.SetBool("anger", true);       // enable anger
        yield return new WaitForSeconds(1.2f);          //sync axe sound
        Instantiate(axehitsfx, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.4f);           // sync knuckle sfx
        Instantiate(knucklesfx, transform.position, Quaternion.identity);
        dialoguefire.begindialog(12);       // start last dialogue
        while (dialoguefire.done == false) //wait for to be end
        {
            yield return null;
        }
        pickableaxe.SetActive(true);    // enable pickable axe 
        cameratarget.enabled = false;       //disable target enable healthbox and set camera to same position.
        healthbar.SetActive(true);
        bossscript.animatorr.SetTrigger("idle");
        camerazoom.orthographicSize = 0.6216953f;
        camerazoom.transform.position = new Vector3(30.93f, -0.98f, -30f);
    }

    public IEnumerator deathphase()
    {
        healthbar.SetActive(false);
        cameratarget.enabled = true;
        bossscript.axepoint.position = new Vector3(bossscript.axepoint.position.x, bossscript.axepoint.position.y, 0);
        Transform aux = bossscript.axepoint.transform;
        aux.position = new Vector3(bossscript.axepoint.position.x, bossscript.axepoint.position.y, 0);

        cameratarget.target = aux;
        player.enabled = false;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Pause();
       
        yield return new WaitForSeconds(1.5f);
        dialoguefire.begindialog(13);
        while (dialoguefire.done == false) //wait for to be end
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().clip = afterbossmusic;
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
        cameratarget.target = player.transform;
        bossscript.killable = true;



    }

}
