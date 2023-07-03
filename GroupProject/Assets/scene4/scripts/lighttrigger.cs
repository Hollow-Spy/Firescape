using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class lighttrigger : MonoBehaviour
{
    private GameObject lightobject;
    bool once = true;
    //these lil light bulb scripts every time they're triggered they'll enable a spot light from an area, shake the screen play an earthquake sound and make stones fall from the ceiling
    private void Awake()
    {
        lightobject = transform.GetChild(0).gameObject;
        lightobject.SetActive(false);
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true)
        {
             once = false;
             camerashake shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();      //star camera shake and lower smooth
            shake.StartCoroutine(shake.Shake(.5f, .02f));
            yield return new WaitForSeconds(0.3f);         //wait a lil

           
            lightobject.SetActive(true);                  //enable child of this object light
            AudioSource changepitch = lightobject.GetComponent<AudioSource>();    // give a random pitch to rock sounds
            changepitch.pitch = Random.Range(.8f, 1.1f);
            Light2D pointlight = lightobject.transform.GetChild(1).gameObject.GetComponent<Light2D>();      // get 2dlight component to edit intensity and angles
            pointlight.intensity = 0;
            pointlight.pointLightInnerAngle = 0;
            pointlight.pointLightOuterAngle = 0;

            for (int i =0; i < 20;i++)  //increase intensity and angle
            {
                pointlight.pointLightInnerAngle += .8f;
                pointlight.pointLightOuterAngle += 1.1f;
                pointlight.intensity += .06f;
                yield return new WaitForSeconds(0.05f);
            }
            
        }
    }
}
