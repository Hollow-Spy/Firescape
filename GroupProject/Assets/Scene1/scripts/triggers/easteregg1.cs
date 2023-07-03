using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easteregg1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject eastereggsong, breakingsfx,destroysfx,walls, breakparticles;
    private int hp = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        playercontrol playerspeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>(); //easter egg!! 
        if(collision.CompareTag("Player") && playerspeed.body.velocity.magnitude > .8f)   //whenever the player's body hits the wall at a certain speed the wall will lose 1 hp and do a break sfx, when broke it will play particles and play the little jingle that i did for the easteregg
        { 
            hp--;
            Instantiate(breakingsfx, transform.position, Quaternion.identity);
            if(hp <=0)
            {
                Instantiate(eastereggsong, transform.position, Quaternion.identity);   // I LOVE THIS JINGLE
                Instantiate(destroysfx, transform.position, Quaternion.identity); 
                Instantiate(breakparticles, new Vector3(transform.position.x,transform.position.y +0.25f,transform.position.z), Quaternion.identity);
                Destroy(walls);
            }
        }
    }
}
