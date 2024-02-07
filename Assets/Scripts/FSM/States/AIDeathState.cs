using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIDeathState : AIState
{

    float timer = 0;
    public AIDeathState(AIStateAgent agent): base(agent) { }


    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Death");
        timer = Time.time + 5; 
    }
    public override void OnUpdate()
    {
        if (Time.time > timer) 
        {
            GameObject.Destroy(agent.gameObject);
        }
    }
    public override void OnExit()
    {

    }
}
