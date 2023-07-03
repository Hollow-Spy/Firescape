using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randompitchsfx : MonoBehaviour
{
    AudioSource source;
    //get a random pitch for sound
        void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(0.8f, 1.1f);
    }

 
}
