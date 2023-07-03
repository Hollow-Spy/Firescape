using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger3 : MonoBehaviour
{
    private CameraControl cameratarget;
    private Camera camerazoom;
    public GameObject target,arenatrigger;
    public GameObject chorus,spike;
    private bool once=true;
    
  
    private void Start()
    {
        cameratarget = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //this script is crazy, its for the heart it pans out the camera slowly , play chorus and enables the blocker behind so you can't leave, the back to the player
        if(once == true && collision.CompareTag("Player"))
        {
            once = false;
            Instantiate(chorus, transform.position, Quaternion.identity);
            spike.SetActive(true);
            cameratarget.target = target.transform;
            cameratarget.smoothTime = 4f;
            StartCoroutine(zoomout());

      
        }
    }

 
    IEnumerator zoomin()
    {
        while(camerazoom.orthographicSize > 0.5665228f)
        {
            camerazoom.orthographicSize -= 0.03f;
            yield return null;
        }
        cameratarget.smoothTime = 0.3f;
        arenatrigger.SetActive(true);
    }
    IEnumerator zoomout()
    {
       
        for(int a = 0; a < 80; a++)
        {
            camerazoom.orthographicSize += .03f;
            yield return null;
        }
        
        yield return new WaitForSeconds(6f);
        cameratarget.smoothTime = .5f;
        cameratarget.target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(zoomin());

        
    }

}
