using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicmanagerscript : MonoBehaviour
{
    public AudioClip[] audios = new AudioClip[2];
    private AudioSource audioplayer;
    public GameObject permanentaudio;
   /// <summary>
   /// this script was aimed to controll most music sounds but due to some recent changes it will be a bit underused in future scenes
   /// </summary>
   ///// (OBSOLETE, it was only used once on level 1, now i use "can't destroy on load" simple game objects with the tag "music"
    private void Start()
    {
        audioplayer = GetComponent<AudioSource>();
    }
    public void nextsound(int nextsong)
    {

        if (!audioplayer.isPlaying)
        {
            audioplayer.clip = audios[nextsong];
            audioplayer.Play();
        }
        else
        {
            StartCoroutine(wait(nextsong));
            
        }


    }
    IEnumerator wait(int nextsong)
    {
        audioplayer.loop = false;
        while(audioplayer.time < audioplayer.clip.length)
        {
            yield return null;
        }
     
        audioplayer.clip = audios[nextsong];
        audioplayer.loop = true;
        audioplayer.Play();
    }
    public void pause()
    {
        audioplayer.Stop();
    }
    public void permanentsong()
    {
        audioplayer.Pause();

        StartCoroutine(waitseconds(5f));
        
       


    }
    IEnumerator waitseconds(float time)
    {
    
        yield return new WaitForSeconds(time);
  
        DontDestroyOnLoad(Instantiate(permanentaudio, transform.position, Quaternion.identity));
        audioplayer.Stop();
    }
}