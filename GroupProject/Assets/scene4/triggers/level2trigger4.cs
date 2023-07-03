using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger4 : MonoBehaviour
{
    private bool once=true;
    public bool active;
    public GameObject targetobject;
    //simply disables a target object and pauses the music it can
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(once == true && collision.CompareTag("Player"))
        {
            once = false;
            targetobject.SetActive(active);
            try
            {
                AudioSource musicc = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
                musicc.Pause();
            }
            catch
            {

            }
         

            Destroy(gameObject);
        }
     
    }
}
