using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{
    public GameObject nodeGO;
    public List<Neighbour> neighbors;
}
