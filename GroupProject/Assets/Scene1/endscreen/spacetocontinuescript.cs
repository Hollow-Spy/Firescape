using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spacetocontinuescript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))      //if player presses space, reset deaths, reset time, reset coins and build next scene
        {
            gamemaster gamegeneral = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
            gamegeneral.deaths = -1;
            gamegeneral.timelapsed = 0;
            gamegeneral.checkpoint = Vector3.zero;
            for(int i=0; i < gamegeneral.collectablecoins.Length; i++){
                gamegeneral.collectablecoins[i] = false;

            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
