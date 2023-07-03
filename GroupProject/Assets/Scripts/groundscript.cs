using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundscript : MonoBehaviour
{
    public playercontrol player;
    public GameObject landsfx;
    IEnumerator coyote;
    /// <summary>
    /// perhaps one of the most important scripts, ground script manages when the player is on the ground and manages coyote time, meaning if the player walks off the edge he still has
    /// .1 seconds to react  and jump
    /// </summary>
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            Instantiate(landsfx, transform.position, Quaternion.identity);
            Instantiate(player.landdustparticles, new Vector3(transform.position.x -0.03f, transform.position.y - 0.01f,transform.position.z), Quaternion.identity);
            if(coyote !=null)
            {
                StopCoroutine(coyote);
            }
            player.boostable = false;
            player.onground = true;
            player.animations.SetBool("jumping", false);
            

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground") //after leaving the ground start coyote time
        {
           
            coyote = coyotetime();
            StartCoroutine(coyote);
          

        }
    }
    IEnumerator coyotetime() //gives you an extra .1 seconds in order to be able to jump
    {
        yield return new WaitForSeconds(.1f);
        player.onground = false;
        player.animations.SetBool("jumping", true);
    }
}
