using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninascript : MonoBehaviour
{
    private bool inside;
    public GameObject space;
    private int dialognumber = 5;
    public dialogmanager dialoguescript;
    public AudioSource voice;

    // Update is called once per frame
    void Update()
    {
        //this script simply switches between nastasha's dialogues, when it reaches the max lengh it just plays the last dialogue over and over again
        //also changes the pitch of the voice in specific line of a specific dialogue which is 9 adding for comedic effect

        if(Input.GetKeyDown(KeyCode.Space) && inside==true && dialoguescript.done == true)
        {
            dialoguescript.begindialog(dialognumber);
            dialognumber++;
          
        }
   
        if (dialognumber == 9 && dialoguescript.linepos == 1)
        {
           
            voice.pitch = 1.3f;

        }
        else
        {
            voice.pitch = 1;
        }
        if (dialognumber == dialoguescript.dialoguescript.Length)
        {
            dialognumber--;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(true);
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            space.SetActive(false);
            inside = false;

        }

    }
}
