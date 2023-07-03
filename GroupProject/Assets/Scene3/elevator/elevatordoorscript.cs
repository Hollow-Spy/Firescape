using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatordoorscript : MonoBehaviour
{
    public GameObject door, doorsfx;
    private bool once=true;
    //this script simply opens the elevator door when touched
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true )
        {
            once = false;
            StartCoroutine(opendoor());
        }
      
    }
    public IEnumerator opendoor()
    {
        Instantiate(doorsfx, transform.position, Quaternion.identity);
        for(int i = 0; i < 10; i++)
        {
            door.transform.Translate(0, 0.05f, 0);
            yield return new WaitForSeconds(0.06f);
        }
        Destroy(this);
    }
}
