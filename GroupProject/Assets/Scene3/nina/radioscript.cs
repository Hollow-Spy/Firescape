using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioscript : MonoBehaviour
{
    private bool inside;
    public AudioClip music;
    public AudioSource radio;
    public dialogmanager dialoguescript;
    public Animator ninadance;
    private void OnTriggerEnter2D(Collider2D collision) //easter egg radio switch up, if player jumps next to radio it will switch song and prompt a natasha dialogue and make her dance
    {
        if (collision.CompareTag("Player"))
        {
           
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inside == true)
        {
            radio.clip = music;
            radio.Play();
            dialoguescript.begindialog(4);
            ninadance.Play("dance");
            Destroy(this);
        }
    }
}
