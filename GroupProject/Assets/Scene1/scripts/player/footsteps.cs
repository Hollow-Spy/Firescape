using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class footsteps : MonoBehaviour
{
    private AudioSource footstepsounds;
    private Animator playeranimation;
   
    private void Start()
    {
        playeranimation = GetComponent<Animator>();
        footstepsounds = GetComponent<AudioSource>();
    }
    //simple footstep script, will alter value so it sounds like it's not just one sound
    private void Update()
    {
        if(playeranimation.GetCurrentAnimatorStateInfo(0).IsName("run") && !footstepsounds.isPlaying)
        {

            footstepsounds.pitch = Random.Range(.85f, 1f);
            footstepsounds.volume = Random.Range(.3f, .4f);
            footstepsounds.Play();
        }
    }

}
