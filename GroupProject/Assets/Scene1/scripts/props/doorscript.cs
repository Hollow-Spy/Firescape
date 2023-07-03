using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{ 

    
    public GameObject block, door, doorsfx;
    bool onetime = true;
    //this script closes the door on the level 1 after coming through it, blocking any passage behind it and makeing it rotate on the  y axis so it gives
    //the idea of closing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && onetime == true)
        { 
            block.SetActive(true);
            onetime = false;
            StartCoroutine(closedoor());

        }
    }
    private IEnumerator closedoor()
    {
        for (int i = 3; i < 81; i += 3)
        {

            door.transform.rotation = Quaternion.Euler(0, door.transform.rotation.y + i, 0);
            yield return new WaitForSeconds(0.005f);
        }

        Instantiate(doorsfx, transform.position, Quaternion.identity);
        
    }

}


   
