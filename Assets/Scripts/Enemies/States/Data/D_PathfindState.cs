using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPathfindStateData", menuName = "Data/State Data/Pathfind State")]
public class D_PathfindState : ScriptableObject {
  public float pathfindSpeed = 1.0f;
  public float nextWaypointDistance = 0.1f;
}

