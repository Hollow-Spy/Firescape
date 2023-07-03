using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyehitbox : MonoBehaviour
{
    public GameObject impactparticles;
    private hurtbounce hurtscript;

    private void Start()
    {
        hurtscript = GameObject.FindGameObjectWithTag("Player").GetComponent<hurtbounce>();
    }
    IEnumerator attackrotatetimed(int direction)
    {
        for(int i = 0; i < 5; i++)    //all this function does is do the rotation when attacking
        {
            transform.Rotate(0, 0, 6 * direction);
            yield return new WaitForSeconds(.1f);
        }
        for(int i =0; i<3;i++)
        {
            transform.Rotate(0, 0, 40 * -direction);
            yield return new WaitForSeconds(.1f);
        }
        Instantiate(impactparticles, new Vector3(transform.position.x + 0.207f * direction,transform.position.y+ 0.203f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < 3; i++)
        {
            transform.Rotate(0, 0, 30 * direction);
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }
  public void attackrotate(int direction) // moves the hitbox accordingly to what direction the player is at
    {
        
        StartCoroutine(attackrotatetimed(direction));
    }

   public void moveup()     //moves hitbox up from the ground
    {
        StartCoroutine(moveuptimed());
    }
    IEnumerator moveuptimed()
    {
        for(int i = 0; i < 4;i++)
        {
            transform.Translate(0, .07225f, 0);
            yield return new WaitForSeconds(.1f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)   //hit the player, begin his bounce scrilpt
    {
        if(collision.CompareTag("Player"))
        {
           if(collision.transform.position.x > this.transform.position.x)
            {
                hurtscript.bounce(new Vector3(-.3f,0,0));
            }
           else
            {
                hurtscript.bounce(new Vector3(.3f, 0, 0));

            }


        }
    }

}
