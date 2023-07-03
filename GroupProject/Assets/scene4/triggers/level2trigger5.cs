using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger5 : MonoBehaviour
{
    private bool once = true;
    public eyetackle[] enemies;
    public GameObject spike;
    public AudioClip music;
    AudioSource musicc;
    public blockerspike blockerscript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when player enteres the trigger, enable blocker on the left, play the music and start spawning enemies 
        if(collision.CompareTag("Player") && once == true)
        {
            spike.SetActive(true);
            once = false;
            try
            {
                 musicc = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
                musicc.clip = music;
                musicc.Play();

            }
            catch
            {

            }
            StartCoroutine(spawner());
        }
    }
    IEnumerator spawner()
    {
        //every 3-5 seconds a new enemy will spawn, we set range to true, call behaviour and quickly set it to false so he spawns without the player having to be next to him
        int i = 0;
        while (i < enemies.Length)
        {
            yield return new WaitForSeconds(Random.Range(3, 5));

            enemies[i].gameObject.SetActive(true);
            enemies[i].range = true;
            enemies[i].behaviour();
            enemies[i].range = false;
            i++;
           
        }
        StartCoroutine(waitforenemydeath());
    }
    IEnumerator waitforenemydeath()
    {
        int i=0;
        while(i < enemies.Length-1) //while the total lengh of the enmies is bigger than 0 wait.
        {
           
           
                if(enemies[i] != null)
                {
                    i = 0;
                }
                else
                     {
                i++;
                     }
           
           
            
            yield return null;

        }
        //once they're all dead , open the right exit and pause the music
        try
        {
            musicc.Pause();
        }
        catch
        {

        }
        blockerscript.StartCoroutine( blockerscript.Lowerspike() );
        
    }
}
