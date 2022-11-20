using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichIdle : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    Enemy self;
    int maxHealth;
    float cooldown;
    float currentTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self = animator.GetComponent<Enemy>();
        maxHealth = self.Health;
        currentTime = cooldown;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(self.Health <= maxHealth * .67f)
        {
            animator.SetTrigger("Stage 2");
        }
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            animator.SetTrigger("Cast");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
