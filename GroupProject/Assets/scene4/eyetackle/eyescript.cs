using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyescript : MonoBehaviour
{
    public int hp;
    public GameObject hurtsfx, deathsfx,bloodparticles,tentacleobject;
    public eyetackle eyetacklescript;
    public Animator animations;
   
    public BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet")) //if bullets collides with eye hitbox
        {
            Instantiate(bloodparticles, new Vector2(collision.transform.position.x, collision.transform.position.y), collision.transform.rotation); // do blood particles
            Instantiate(hurtsfx, transform.position, Quaternion.identity); //playt hurt sfx
            Destroy(collision); //destroy bullet that collided
        
            hp--; //decrease hp
        
            if (hp < 0) //if hp bellow 0, destroy hitbox, death sound, eye script,play anim and destroy tentacle after seconds 1.1
            {
                hitbox.enabled = false;
                Instantiate(deathsfx, transform.position, Quaternion.identity);
                Destroy(eyetacklescript);
                animations.Play("deathanim");
                Destroy(tentacleobject, 1.1f);
            }
        }

    }
}
