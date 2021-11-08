using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {
  public float wallCheckDistance = 0.1f;
  public float ledgeCheckDistance = 0.35f;
  public LayerMask whatIsGround;

  public float minAggroDistance = 3.0f;
  public float maxAggroDistance = 4.0f;
  public LayerMask whatIsPlayer;
}
