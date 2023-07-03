using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeswing2behaviour : StateMachineBehaviour
{
    int shots=3;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        animator.ResetTrigger("axethrow");
        animator.ResetTrigger("boomerang");
        shots--;
        if(shots < 0)
        {
            shots = Random.Range(3,5);

           
           if(shots== 3)
            {
                animator.SetTrigger("axethrow");
            }
           else
                {
                animator.SetTrigger("boomerang");
            }

           
        }
   }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
