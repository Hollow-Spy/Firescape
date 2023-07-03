using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class timecount : MonoBehaviour
{
    public Text text,deathcounter;
    private double secs,minute=0,time;
    public GameObject deaths,dialogbox;
    public skullytext skull;
    public GameObject coinobject,spaceobject;
    
    
    private gamemaster gamegeneral;
    void Start()
    {
        gamegeneral = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();      //find the game manager and extract the timelapsed variable which contains that amout of time it took to complete the level
        time = gamegeneral.timelapsed;
       
        StartCoroutine(showtime());
    }
    IEnumerator showtime()
    {
        while(time > 0)        //show minutes and seconds
        {
            secs++;
            time--;
            if (secs >= 60)
            {
                minute++;
                secs = 0;
            }

            text.text = minute + ":" + secs;
            yield return null;
        }
        deaths.SetActive(true);          //show deaths count
        deathcounter.text = gamegeneral.deaths.ToString();   //set the counter to the amout of deaths
        yield return new WaitForSeconds(.7f);
        dialogbox.SetActive(true);            //enable the dialogue box for the skeleton so it animates
        yield return new WaitForSeconds(.2f);

        skull.funnyquote(gamegeneral.deaths); //say something funny about deaths, skully
        yield return new WaitForSeconds(1.5f);
        coinobject.SetActive(true);      //show coins now
       
        for (int i = 0; i < gamegeneral.collectablecoins.Length;i++)        //check for every coin you've collected and increase it on the player's pref
        {
            if(gamegeneral.collectablecoins[i] == true)
            {
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1);
            }
        }
       

        coinobject.GetComponent<Text>().text = PlayerPrefs.GetInt("coins").ToString(); //set coins to the playe pref value
        yield return new WaitForSeconds(1.5f);
        skull.funnymoneyquote(PlayerPrefs.GetInt("coins"), gamegeneral.collectablecoins.Length); //skull says smth about ur money
        yield return new WaitForSeconds(2);
        Instantiate(spaceobject, transform.position, Quaternion.identity);   //you're finnally able to go to the next scene
    }
  
}
