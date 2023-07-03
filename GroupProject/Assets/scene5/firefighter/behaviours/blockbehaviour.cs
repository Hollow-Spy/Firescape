using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockbehaviour : StateMachineBehaviour
{
    int shots=4;
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shots--;                 //reduce shots, everytime he plays this animation he gets closer to charging the player
        if(shots <= 0)
        {
            shots = Random.Range(1, 3);
            animator.SetBool("charge",true);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
