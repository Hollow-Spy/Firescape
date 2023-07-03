using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    bool once = true;
    public GameObject ambiencemusic;
    private void OnTriggerEnter2D(Collider2D collision) //simply start playing music that can't be destroyed on load, replace old one if triggered again
    {
        if(collision.CompareTag("Player") && once == true)
        {
            once = false;
            try
            {
                Destroy(GameObject.FindGameObjectWithTag("music"));

            }
            catch { }
            DontDestroyOnLoad(Instantiate(ambiencemusic, transform.position, Quaternion.identity));
        }
       

    }



}
