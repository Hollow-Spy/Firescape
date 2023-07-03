using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargerunbehaviour : StateMachineBehaviour
{
    public float speed;
    Rigidbody2D body;
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponent<Rigidbody2D>();
     }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int inv;
        if(animator.gameObject.transform.eulerAngles.y == 180) //check rotation before sending boss in a freenzy towards a direction
        {
            inv = 1;
        }
        else
        {
            inv = -1;
        }


        body.velocity = new Vector2( animator.transform.position.x,0) * speed * Time.deltaTime * inv; //the freenzy
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        body.velocity = Vector2.zero; //when leaving rmember to set velocity to 0 again

    }


}
