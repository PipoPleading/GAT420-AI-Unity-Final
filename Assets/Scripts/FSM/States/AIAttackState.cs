using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    float timer = 0;
    public AIAttackState(AIStateAgent agent) : base(agent) { }


    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Trigger");
        timer = Time.time + 2;
    }
    public override void OnUpdate()
    {
        if(Time.time >= timer)
        {
            agent.stateMachine.SetState(nameof(AIIdleState));
        }
    }
    public override void OnExit()
    {

    }
}
