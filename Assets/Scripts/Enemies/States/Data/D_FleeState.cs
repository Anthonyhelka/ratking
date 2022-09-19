using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFleeStateData", menuName = "Data/State Data/Flee State")]
public class D_FleeState : ScriptableObject {
  public float fleeSpeed = 1.0f;

  public bool touchDamage = false;
}

