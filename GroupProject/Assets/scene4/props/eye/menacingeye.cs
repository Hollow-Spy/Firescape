using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menacingeye : MonoBehaviour
{
    bool inside = false;
    //this script is just pure evil, player walks into this eye's range and the pupil will rotate in a way that it's always looking at the player
    //this was a clever mix of recycled code and using the pupils offset, so that way the rotation happends in a way that it appears that the eye
    //is always looking at the player
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inside = true;
            while(inside == true)
            {
                Vector3 difference = collision.transform.position - transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ -90);
                yield return new WaitForSeconds(.001f);
                
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inside = false;
        }
    }
}
