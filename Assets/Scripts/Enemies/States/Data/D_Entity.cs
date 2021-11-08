using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {
  public float maxHealth = 100.0f;
  public float damageHopSpeed = 3.0f;
  public float wallCheckDistance = 0.1f;
  public float ledgeCheckDistance = 0.35f;
  public LayerMask whatIsGround;
  public float minAggroDistance = 3.0f;
  public float maxAggroDistance = 4.0f;
  public LayerMask whatIsPlayer;
  public float closeRangeActionDistance = 0.1f;
  public string type = "Infected";
  public float meleeAttackCooldown = 1.0f;
  public GameObject hitParticle;
}
