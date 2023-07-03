
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class dialogmanager : MonoBehaviour
{
  

    public dialogue[] dialoguescript;                   // list of dialogues to introduce 
    private playercontrol player;    //reference to player
    public weapon shoty; //refernce to weapon
    public GameObject dialogboxobject; //reference to the dialogue box object aka the lil box w text
    public RawImage npcface;   // the face of the dialogue
    public int linepos=0, dnumber;  //line pos, meaning which line we're in the the dialogue and number of dialogue, 
    private AudioSource npcvoice;     //the npc voice
    public Text text;    //text for dialogue
    public bool talking; //boool talking
    public bool done=true; //bool done so wwe know when the dialogue is over
    footsteps footstepnoise;  //reference to annnoying footstep script so it disables it during dialogue
   public axeweapon axe;   //reference to axe
   

    private void Start()
    { //getting all the components
        
        npcvoice = GetComponent<AudioSource>();        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();       
        footstepnoise = GameObject.FindGameObjectWithTag("Player").GetComponent<footsteps>();

    }
   public void begindialog(int dialognumber)     //begin dialogue (number of the dialogue) 
    {
        done = false;                      //we set done to false to indicate the dialogue is still going
        footstepnoise.enabled = false; //remove damn step noises
        dnumber = dialognumber;    //set the d number to dialog number,
        linepos = 0;      //start from line 0
        player.animations.SetFloat("speed", 0);    //set the player speed to 0 so it plays idle
        player.enabled = false;   // disable player control
        try   //try to disable shotgun else disable axe
        {
            shoty.enabled = false;
        }
        catch
        {
            axe.enabled = false;
        }


        StartCoroutine(movedown());     // move the text box down
        npcvoice.clip = dialoguescript[dialognumber].voice;  // set the voice to the inputed dialog voice
        npcface.texture = dialoguescript[dialognumber].initialface;   //chance face

        StartCoroutine(talk());  //start talking courtine
    

    }

    private void Update()       //if player presses m1 and we're not done, do skip
    {
        if(Input.GetMouseButtonDown(0) && done == false)
        {
          
            skip();
        }
    }

    void skip()
    {
        if(talking == false && linepos >= dialoguescript[dnumber].lines.Length)   //if the npc is done talking and we've reach the limit of lines from the dialogue we're done
        {
            done = true;
            StartCoroutine(moveup());
            
        }
        if (talking == false && done == false)      //if npc aint talking but we're not done either read next line
        {
            
            StartCoroutine(talk());
        }
        else
        {
            if (talking == true)            //otherwise just skip the read time
            {
                talking = false;
            }

        }


    }
    
    //talk function
    IEnumerator talk()         
    {
       
        int i = 1;
        text.text = "";     // first thing is to set int i to 1 and text to nothing
        talking = true;   // talking will be set to true, this will influence the sounds
        StartCoroutine(facechanger());   // start paralel function for faces
        while (text.text != dialoguescript[dnumber].lines[linepos] && talking == true)       //while the text is not equal to the one we're using rn and talking is true
        {

            text.text = dialoguescript[dnumber].lines[linepos].Substring(0,i);        //the text is equal to the dialog text +3 letters everytime
            npcvoice.pitch = Random.Range(.83f, 1);      //slightly change pitch of voice between those numbers
            npcvoice.Play();    //play npc voice
            i += 3;  // increase i by 3 so next iteratio will be the character that we already had +3 new ones
            if(i > dialoguescript[dnumber].lines[linepos].Length)  // if i is bigger than the line's lengh
            {
                i = dialoguescript[dnumber].lines[linepos].Length; //set it equal to the lengh
            }
            yield return new WaitForSeconds(0.1f);
            
        }
        text.text = dialoguescript[dnumber].lines[linepos];   // text is equal to the dialog text
        talking = false;   //talking false
        linepos++;   // we go to next line
    }
    //change fece expressions function
    IEnumerator facechanger() // face changer (under used but works, just gotta play with different delay values) used for newscaster only rn
    {
        int i = 0;  
        while(talking == true) //while npc is talkin 
        {
            if(i > dialoguescript[dnumber].facialsmanager[linepos].face.Length -1)  //if i is bigger than the face limit, set it back to 0
            {
                i = 0;
            }
            npcface.texture = dialoguescript[dnumber].facialsmanager[linepos].face[i]; //set the face to the i number
            i++; 
            yield return new WaitForSeconds(Random.Range(.6f,1f)); //how long it can take to change faces
           
        }
        try
        {
            npcface.texture = dialoguescript[dnumber].facialsmanager[linepos-1].face[0];   // try to set the face to the first face of the last dialog after getting skipped, cause when u finish dialogue it already thinks you're in the next one, thus the -1
        }
        catch { }
       


    }
   
    //simply move dialog box back up/down
    IEnumerator moveup()       
    {
        for (int i = 0; i < 17; i++)
        {
            dialogboxobject.transform.Translate(0, 16, 0);
            yield return null;
        }
        player.enabled = true;
        footstepnoise.enabled = true;
        
        try
        {
            shoty.enabled = true;
        }
        catch
        {
            axe.enabled = true;

        }
       

    }

    IEnumerator movedown()     
    {
    

        for (int i = 0; i < 17;i++)
        {
            dialogboxobject.transform.Translate(0, -16, 0);
            yield return null;
        }
       
    }
  
}
