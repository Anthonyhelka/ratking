using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject {
  public int attackDamage = 2;
  public float attackRadius = 0.5f;
  public float attackCooldown = 0.0f;
  public LayerMask whatIsPlayer;
}
