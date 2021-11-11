using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {
  public float maxHealth = 100.0f;
  public float damageHopSpeed = 1.0f;
  public float damageCooldown = 0.125f;
  public float wallCheckDistance = 0.1f;
  public float ledgeCheckDistance = 0.35f;
  public LayerMask whatIsGround;
  public Vector2 minAggroDistance = new Vector2(2.25f, 1.25f);
  public Vector2 maxAggroDistance = new Vector2(2.25f, 1.25f);
  public LayerMask whatIsPlayer;
  public float closeRangeActionDistance = 0.3f;
  public string type = "Infected";
  public float meleeAttackCooldown = 1.0f;
  public int touchDamageAmount = 1;
  public Vector2 touchDamageDistance = new Vector2(0.2f, 0.3f);
  public GameObject hitParticle;
  public GameObject blockParticle;
}
