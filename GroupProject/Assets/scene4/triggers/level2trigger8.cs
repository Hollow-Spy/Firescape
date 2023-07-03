using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger8 : MonoBehaviour
{
    bool once=true;
    public BoxCollider2D blocker;
    public wavescript waver;
    public Transform targetforcamera;
    //trigger for wave to start moving towards the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true)
        {
        


            once = false;
         
            blocker.enabled = true;
            waver.gameObject.SetActive(true);
            waver.startmoving();
            try
            {
                AudioSource music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
                music.Play();
            }
            catch
            {

            }

           
            

            StartCoroutine(zoomout());


        }
        
    }
    IEnumerator zoomout()
    {
        Camera camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CameraControl target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();

        for (int i =0; i < 7; i++)
        {
            camerazoom.orthographicSize += 0.05469544f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        target.target = targetforcamera;
    }
}
