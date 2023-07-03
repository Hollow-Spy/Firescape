using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savenatasha : MonoBehaviour
{
    public GameObject space;
    bool inside;
    playercontrol player;
   public weapon shoty;
    camerashake camerashaker;
    CameraControl cameratarget;
    Camera camerazoo;
    public Transform snappos;
    SpriteRenderer playersprite;
    private Animator playeranims;
    public Sprite[] liftframes;
    Animator boardlift;
    public GameObject liftsfx,woodsfx,firefighterobject,scriptedaxe,sparkles,impactsfx,axehitsfx,triggerfloor;
    public Transform cameratargetobject;
    public elevatordoorscript elevatordoor;
    public dialogmanager firedialogue;
    bool incutscene;

    private void Start()
    {
        playersprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();   //all references for cutscene
        playeranims = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
        camerazoo = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camerashaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();
        cameratarget = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        boardlift = GetComponent<Animator>();
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))      //if player's in show space
        {
            inside = true;
            space.SetActive(true);
            StartCoroutine(liftbro());
        }
 
    }
    private void Update()
    {
        if(incutscene == true)    //if in cutscene keep player disabled
        {
            player.enabled = false;
            shoty.enabled = false;
        }
    }
    IEnumerator liftbro() //if player inside and presses space iniciate cutscene
    {
        while(inside == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                player.enabled = false;
                shoty.gameObject.SetActive(false);
                playeranims.enabled = false;


                player.gameObject.transform.position = snappos.position; //snap to position etc, change frame image, zoom in and start lifting courutine
                playersprite.sprite = liftframes[0];
                inside = false;
                for(int i = 0;i < 10; i++)
                {
                    camerazoo.orthographicSize -= 0.02f;
                }
                StartCoroutine(lifting());
            }
            yield return null;
        }
    }
    IEnumerator lifting()
    {
      
        int i = 0;
        while(i < 16)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                i++;
               StartCoroutine(camerashaker.Shake(0.05f, 0.02f));
                Instantiate(liftsfx, transform.position, Quaternion.identity);      //lift, whenever space is hit a sound will be played screen shakes and gets closer to being lift
                    
            }
            yield return null;
        }
        Instantiate(woodsfx, transform.position, Quaternion.identity);           //after done playing wood animation an sfx
        boardlift.Play("liftanim");
        for(int a = 0; a < liftframes.Length; a++)                      //play player lifting hands anim
        {
            playersprite.sprite = liftframes[a];
            yield return new WaitForSeconds(0.02f);
        }
        incutscene = true;
        player.animations.enabled = true;      //enable animator
        player.animations.SetFloat("speed", 0);  //set speed to 0 so it stays idle
        shoty.gameObject.SetActive(true);         //enable shotgunagain
        Destroy(space);         //destroy space
        AudioSource musicplaying = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        musicplaying.Pause(); 
        
        yield return new WaitForSeconds(.3f);
        elevatordoor.StartCoroutine(elevatordoor.opendoor());    
        yield return new WaitForSeconds(1f);
        firedialogue.begindialog(1);
        firefighterobject.SetActive(true);
        while(firedialogue.done == false)      //start dialogue with firefighter while its not over, wait
        {
            yield return null;
        }
        cameratarget.target = cameratargetobject;         //set camera to target firefighter
        yield return new WaitForSeconds(0.3f);
        Animator firefighteranims = firefighterobject.GetComponent<Animator>();
        firefighteranims.Play("pointanim");
        yield return new WaitForSeconds(1.2f);
        Instantiate(impactsfx, transform.position, Quaternion.identity);        //point at player w impact sfx
        yield return new WaitForSeconds(0.3f);
        firedialogue.begindialog(2);                         //wait for second dialog to be finished
        while(firedialogue.done == false)
        {
            yield return null;
        }
        cameratarget.target = player.gameObject.transform;        //set cam to player

        yield return new WaitForSeconds(2.5f);
        firefighteranims.Play("idle");
        cameratarget.target = cameratargetobject;              
        yield return new WaitForSeconds(1f);
        firedialogue.begindialog(3);
        while(firedialogue.done== false)
        {
            yield return null;
        }
        firefighteranims.Play("axethrowanim");
        yield return new WaitForSeconds(1);
        scriptedaxe.SetActive(true);
        cameratarget.target = scriptedaxe.transform;
        int inv = 1;
        for(int b = 0; b < 7; b++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(axehitsfx, transform.position, Quaternion.identity);
            Instantiate(sparkles, new Vector3(scriptedaxe.transform.position.x, scriptedaxe.transform.position.y,sparkles.transform.position.z),Quaternion.Euler(90 * inv, 0,0));
            inv = inv * -1;
        }
        triggerfloor.SetActive(true);
        incutscene = false;
        cameratarget.target = player.gameObject.transform;
        yield return new WaitForSeconds(1.3f);
        scriptedaxe.SetActive(false);
        yield return new WaitForSeconds(2);

        Destroy(firefighterobject);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inside = false;
            try
            {
                space.SetActive(false);
            }
            catch { }
          
        }
    }
}
