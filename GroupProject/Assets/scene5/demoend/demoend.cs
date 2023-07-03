using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demoend : MonoBehaviour
{
    // Start is called before the first frame update
    public Text time, deaths;
    void Start()
    {
        gamemaster gamer = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
        time.text = "time:" + gamer.timelapsed;
        deaths.text = "deaths: " + gamer.deaths;
    }

 
}
