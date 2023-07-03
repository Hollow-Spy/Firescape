using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowdetect : MonoBehaviour
{
    public windowscript window;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) window.inside = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) window.inside = false;

    }
}
