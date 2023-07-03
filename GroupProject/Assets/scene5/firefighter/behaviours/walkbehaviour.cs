using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkbehaviour : StateMachineBehaviour
{
    Transform player,bosspos;
    public float speed;
     float timewalking=0;
     int walkcharge;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    /// <summary>
    /// alrigh so, main idea is to make the player walk walk walk for a couple of seconds and then chose a random ranged attack to perform.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("anger") ==true) //if he's angry he's faster
        {
            speed = 0.2f;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        bosspos = GameObject.FindGameObjectWithTag("boss").GetComponent<Transform>();
        animator.ResetTrigger("punch");
        animator.ResetTrigger("kick");
        animator.ResetTrigger("grab");
        animator.SetBool("block", false);
        walkcharge = Random.Range(3, 5); //how long he'll run for
        timewalking = 0; // the time counted walking

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timewalking += Time.deltaTime; 
      
        if(timewalking > walkcharge && animator.GetBool("anger") == true) //if he's angry and has walked enough, charge
        {
            animator.SetBool("charge",true);
        }
        if (player.position.x > animator.transform.position.x) //the flip
        {
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (animator.GetBool("range") == false) //while he's not in range, walk towards player
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(player.position.x, animator.transform.position.y), speed * Time.deltaTime);
           
        }
        else
        {
            float difference = Vector2.Distance( new Vector2( player.position.x,0), new Vector2 (bosspos.position.x,0)); // use difference from x's only as we're gonna use y's to determine which attack to perform

            if (difference <= 0.1 && player.position.y > bosspos.position.y + 0.448f) //first, if it's above head, try grabbing
            {
             
                animator.SetTrigger("grab");
            }
            else
            {

                if (player.position.y > bosspos.position.y + 0.343f) //else, above waist = punch
                {
                   

                  
                    animator.SetTrigger("punch");

                }
                else
                {
                    animator.SetTrigger("kick"); //else kick
                }
            }
        }
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //  
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    
}
