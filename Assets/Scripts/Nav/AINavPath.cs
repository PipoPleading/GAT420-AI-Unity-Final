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

	[SerializeField] private ePathType pathType;
	[SerializeField] private AINavNode startNode;
	[SerializeField] private AINavNode endNode;

	AINavAgent agent;

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

			}
		}
	}

	private void Start()
	{
		agent = GetComponent<AINavAgent>();	
		targetNode = (startNode != null) ? startNode : AINavNode.GetRandomAINavNode(); 
		
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
		AiNavDijkstra.Generate(startNode, endNode, ref path);
	}

	private AINavNode GetNextPathAiNavNode(AINavNode node) 
	{
		if(path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);

		if(index + 1 == path.Count) return null;
		if(index < 0) return null;
		AINavNode nextNode = path[index + 1];

		return null;
	}
}
