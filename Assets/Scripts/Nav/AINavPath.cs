using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AINavAgent))]
public class AINavPath : MonoBehaviour
{
	public enum ePathType
	{
		Waypoint,
		Dijkstra,
		AStar
	}

	[SerializeField] AINavAgent agent;
	[SerializeField] private ePathType pathType;



    List<AINavNode> path = new List<AINavNode>();

	public AINavNode targetNode { get; set; } = null;
	public Vector3 destination 
	{ 
		get 
		{ 
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero; 
		}
		set
		{
			if(pathType == ePathType.Waypoint) { targetNode = agent.GetNearestAINavNode(value); }
			else if ( pathType == ePathType.Dijkstra || pathType == ePathType.AStar )
			{
				AINavNode startNode = agent.GetNearestAINavNode();
				AINavNode endNode = agent.GetNearestAINavNode(value);

				GeneratePath(startNode, endNode);
				targetNode = startNode;
			}
		}
	}

	private void Start()
	{
		agent = GetComponent<AINavAgent>();	
		//targetNode = (startNode != null) ? startNode : AINavNode.GetRandomAINavNode(); 
		
	}

	public bool HasPath()
	{
		return targetNode != null;
	}

	public AINavNode GetNextAINavNode(AINavNode node)
	{
		if (pathType == ePathType.Waypoint) return node.GetRandomNeighbor();
		if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar) return GetNextPathAiNavNode(node);
        

		return null;
	}

	private void GeneratePath(AINavNode startNode, AINavNode endNode)
	{
		AINavNode.ResetNodes();
		if(pathType == ePathType.Dijkstra) AiNavDijkstra.Generate(startNode, endNode, ref path);
		if(pathType == ePathType.AStar) AINavStar.Generate(startNode, endNode, ref path);
	}

	private AINavNode GetNextPathAiNavNode(AINavNode node) 
	{
		if(path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);

		if( index == -1 || index + 1 == path.Count ) return null;
		AINavNode nextNode = path[index + 1];

		return nextNode;
	}

    private void OnDrawGizmosSelected()
    {
        if (path.Count == 0) return;

        var pathArray = path.ToArray();

        for (int i = 1; i < path.Count - 1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(pathArray[i].transform.position + Vector3.up, 1);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pathArray[0].transform.position + Vector3.up, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pathArray[pathArray.Length - 1].transform.position + Vector3.up, 1);
    }
}
