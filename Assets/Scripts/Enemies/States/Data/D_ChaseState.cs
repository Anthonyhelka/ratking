using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChaseStateData", menuName = "Data/State Data/Chase State")]
public class D_ChaseState : ScriptableObject {
  public float chaseSpeed = 1.0f;
  public float turnDelayTime = 0.5f;

  public bool touchDamage = false;
}

