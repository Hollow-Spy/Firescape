using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutscene1 : MonoBehaviour
{
    bool once = true;
    CameraControl camerafocus;
    public GameObject natashaobject;
    public dialogmanager dialoguer;
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true) // this is the first cutscene for the level 5, it simply pans into natasha, starts her "help" dialogue
            //and then back to the player when the dialogue is done
        {
            once = false;
            camerafocus = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
            camerafocus.target = natashaobject.transform;
            camerafocus.smoothTime = 0.5f;

            yield return new WaitForSeconds(0.5f);
            dialoguer.gameObject.SetActive(true);
            dialoguer.begindialog(0);
            while(dialoguer.done == false)
            {
                yield return null;
            }
            camerafocus.smoothTime = .3f;
            camerafocus.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
