using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffe : MonoBehaviour
{
    public GameObject sign, coinsfx, particles;
    private bool inside;
    public coinscript coinupdater;
    public dialogmanager dialoguescript;
 
    
    private void OnTriggerEnter2D(Collider2D collision) //show item description when close
    {
        if (collision.CompareTag("Player"))
        {
            sign.SetActive(true);
            inside = true;
        }
    }

    private void Update() //coffe script! this coffe will make the recovery times shorter if bought, it stays saved in player prefs forever for now
    {
        if (Input.GetKeyDown(KeyCode.Space) && inside == true ) //if player is touching the coin and presses space
        {
            if (PlayerPrefs.GetInt("coins") >= 1) //AND has enough money
            {
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 1); //reduce coins, give the man his power, and play coin particles and play sound
                PlayerPrefs.SetFloat("recovery", 2);
                Instantiate(coinsfx, transform.position, Quaternion.identity);
                Instantiate(particles, transform.position, Quaternion.identity); //also start natashas dialogue and destroy it self
                coinupdater.updatecoin();
                dialoguescript.begindialog(2);
                Destroy(gameObject);

            }
            else
            {

                if (dialoguescript.done == true) dialoguescript.begindialog(0); //if she's not talking, then start dialogue 0
                
              
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
