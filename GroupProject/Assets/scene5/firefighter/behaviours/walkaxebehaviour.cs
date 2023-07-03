using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkaxebehaviour : StateMachineBehaviour
{
    Transform player;
    public float speed;
    float timer;
    public float min, max;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(min, max);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("range") == false) //walk towards player until the animator detects range is true
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(player.position.x, animator.transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            int rand = Random.Range(0, 2);     //if range true select one of two attacks
            if(rand == 0)
            {
                
                animator.SetTrigger("axerange1");
            }
            if(rand == 1)
            {
               
                animator.SetTrigger("axerange2");
            }
        }
     
      
        if (player.position.x > animator.transform.position.x) // flip depending on player pos
        {
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(timer <= 0)     //if time runs out go to idle, from there chose a diffferent state
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
