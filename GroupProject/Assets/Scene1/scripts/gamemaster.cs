using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemaster : MonoBehaviour
{
    private static gamemaster instance;
    public float timelapsed;
    public int deaths;
    public Vector3 checkpoint;
    public bool[] collectablecoins;
    //this script is responsible to keep track of deaths, elapsed time, and money caught through out the level, it uses dont destroy on load in order not to be 
    //deleted and it replaces his old partner so they dont stack on each other
    void Awake()
    {
      
        if (instance == null)
        {
            
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            {
            instance.deaths++;
           
            Destroy(gameObject);
            }
       
    }
    private void Update()
    {
        timelapsed += Time.deltaTime;
    }
}
