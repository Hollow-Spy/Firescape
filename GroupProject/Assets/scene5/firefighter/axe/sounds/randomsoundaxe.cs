using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomsoundaxe : MonoBehaviour
{
    AudioSource randompitch;
    // Start is called before the first frame update


    //make a random axe sound hit sound and make volume fade away
    void Start()
    {
         randompitch = GetComponent<AudioSource>();
        randompitch.pitch = Random.Range(0.8f, 1.1f);
    }
    private void Update()
    {
        randompitch.volume -= Time.deltaTime / 1.5f;
    }

}
