using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banana : MonoBehaviour
{
    public GameObject sign,coinsfx,particles;
    private bool inside;
    public coinscript coinupdater;
    public dialogmanager dialoguescript;

 
    //banana thats bought on the shop script, just increases the main jump height using the player prefs as communcation method, similar to coffe
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sign.SetActive(true);
            inside = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inside == true)
        {
            if (PlayerPrefs.GetInt("coins") >= 2)
            {
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 2);
                PlayerPrefs.SetFloat("jumpheight", .07f);
                Instantiate(coinsfx, transform.position, Quaternion.identity);
                Instantiate(particles, transform.position, Quaternion.identity);
                coinupdater.updatecoin();
                Destroy(gameObject);
                dialoguescript.begindialog(3);

            }
            else
            {
                if(dialoguescript.done == true) dialoguescript.begindialog(1);

            }
        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sign.SetActive(false);
            inside = false;

        }

    }
}
