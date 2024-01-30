using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    private void Start()
    {
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //shoots a ray fromthe cam and stores it
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //true if a hit, false if not
            if(Physics.Raycast(ray, out hit))
            {
                //Moves the agent via UnityAgent.AI to the hit point
                agent.SetDestination(hit.point);

                 
            }
        }
        if(agent.remainingDistance > agent.stoppingDistance) 
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            //stops the animation lol
            character.Move(Vector3.zero, false, false);
        }
    }

}
