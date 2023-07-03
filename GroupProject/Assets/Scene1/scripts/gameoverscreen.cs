using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverscreen : MonoBehaviour
{

    private AudioSource musicsource;
    private void Start()
    {
        try
        {
            AudioSource audioplayer = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>(); //when it first shows up, pause music
            audioplayer.Pause();
        }
        catch
        {

        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //if player pressers r music will upause and it will reload the scene
        {
            try
            {       
            musicsource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
            if(musicsource.isPlaying == false)
            {
                    musicsource.UnPause();
            }
            }
            catch { 
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
  

}
