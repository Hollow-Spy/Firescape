using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger1 : MonoBehaviour
{
    public Fire fire;
    public musicmanagerscript music;
    bool onetime = true;
    private void OnTriggerEnter2D(Collider2D collision) //first ever trigger, once player is inside it will start music and make the fire rise
    {
        if(collision.tag == "Player" && onetime == true)
        {
            fire.StartCoroutine(fire.start());
            PlayerPrefs.SetInt("coins", 0);
            PlayerPrefs.SetInt("jumpheight", 0);
            PlayerPrefs.SetInt("recovery", 0);
            music.nextsound(0);
            onetime = false;
            
            
        }
    }
   
}
