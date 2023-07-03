using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlebossbehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    mainboss bossscript;
    float timer;
    int randomnumber,chance=80;

    public float min, max;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        bossscript = animator.GetComponent<mainboss>();
        timer = Random.Range(min, max);
         randomnumber = Random.Range(0, 100);
      
    }




    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if(bossscript.agressive == true)    //after x seconds pick a random state between axe throw, boomerang and axe walk :) this is my first time using animtor's behaviour system and at first i was so scared but now i'm getting the hang of it and i'm amazed how intuitive and it is to create this kidna stuff yknnow, good stuff
        {
            if(timer <= 0)
            {
                timer = 5;
     
                if (randomnumber > chance)
                {
                    chance = 80;
                    animator.SetTrigger("axethrow");
                   
                }
                else
                {
                    if (randomnumber > 50)
                    {
                        chance -= 5;
                        animator.SetTrigger("boomerang");
                       

                    }
                    else
                    {
                        chance -= 5;
                        animator.SetTrigger("walkaxe");
                        

                    }
                    
                }
                

            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (animator.GetBool("axeless") == false && animator.GetBool("range") == true)
            {
                int i = Random.Range(0, 2);
                if (i == 1)
                {
                    animator.SetTrigger("axerange1");
                }
                else
                {
                    animator.SetTrigger("axerange2");

                }
            }
        }
    
  }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

  
}
