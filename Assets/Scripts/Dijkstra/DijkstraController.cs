using System.Collections.Generic;
using UnityEngine;

public class DijkstraController : MonoBehaviour
{
    [SerializeField] private List<Node> nodes;
    private void Start()
    {
        List<GameObject> path = FindShortestPath(nodes[0].nodeGO, nodes[5].nodeGO);

        Debug.Log("Shortest path from node 1 to node 6:");
        Debug.Log(string.Join(" -> ", path));
    }

    public List<GameObject> FindShortestPath(GameObject startNode, GameObject endNode)
    {
        Dictionary<GameObject, int> edgeDistance = new Dictionary<GameObject, int>();
        Dictionary<GameObject, GameObject> previous = new Dictionary<GameObject, GameObject>();
        List<GameObject> unvisited = new List<GameObject>();

        foreach (Node node in nodes)
        {
            edgeDistance[node.nodeGO] = int.MaxValue;
            previous[node.nodeGO] = null;
            unvisited.Add(node.nodeGO);
        }

        edgeDistance[startNode] = 0;

        while (unvisited.Count > 0)
        {
            GameObject node = GetClosestNode(unvisited, edgeDistance);
            unvisited.Remove(node);

            if (node == endNode)
            {
                break;
            }

            Node currentNod = nodes.Find(x => x.nodeGO == node);

            foreach (Neighbour neighbor in currentNod.neighbors)
            {
                int newEdgeDistance = edgeDistance[node] + neighbor.edgeDistance;

                if (newEdgeDistance < edgeDistance[neighbor.nodeGO])
                {
                    edgeDistance[neighbor.nodeGO] = newEdgeDistance;
                    previous[neighbor.nodeGO] = node;
                }
            }
        }

        List<GameObject> path = new List<GameObject>();
        GameObject currentNode = endNode;
        while (currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = previous[currentNode];
        }

        return path;
    }

    private GameObject GetClosestNode(List<GameObject> unvisited, Dictionary<GameObject, int> edgeDistance)
    {
        GameObject closestNode = null;
        int closestEdgeDistance = int.MaxValue;

        foreach (GameObject node in unvisited)
        {
            if (edgeDistance[node] < closestEdgeDistance)
            {
                closestNode = node;
                closestEdgeDistance = edgeDistance[node];
            }
        }

        return closestNode;
    }
}