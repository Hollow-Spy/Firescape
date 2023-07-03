using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectablescript : MonoBehaviour
{
    gamemaster thegamemaster;
    public int coinnumber;
    public GameObject coinsfx;

    //script for coins, just attribute a number for the coin that hasnt been taken yet in this level and the game master will handle the rest
    private void Awake()
    {
        thegamemaster = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
        if (thegamemaster.collectablecoins[coinnumber] == true)
        {
            Destroy(gameObject);
        }
        StartCoroutine(rotation());
    }
    
    IEnumerator rotation() //rotate coin
    {
        while (true)
        {
            transform.GetChild(0).transform.Rotate(0, Random.Range(5,8), 0, Space.World);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //if player touches coin play little sfx alter info in gamemaster and play lil jingle i did
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(coinsfx, transform.position, Quaternion.identity);
            thegamemaster.collectablecoins[coinnumber] = true;
            Destroy(gameObject);
        }
    }
  

}
