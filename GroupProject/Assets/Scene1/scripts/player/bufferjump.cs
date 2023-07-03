using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bufferjump : MonoBehaviour
{
    /// <summary>
    ///  this is script is manly focused on buffering the jump, meaning if the player presses and holds the space button before they hit the ground it will count as a jump
    ///  this is meant to make the game more forgiving in the jump time
    /// </summary>
    private playercontrol player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>(); 
        //here we get the player controller component 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("ground"))
        {
            StartCoroutine(holding());
           
        }
      //once the box collider thats set as a trigger on the player's feet touches the ground it will start the check if the player is holding the space button
    }
    //constant check if player is holding space until he fully hits the ground
    IEnumerator holding()
    {
        while (player.onground == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.bufferjump = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                player.bufferjump = false;
            }
            yield return null;

        }
    }
}
