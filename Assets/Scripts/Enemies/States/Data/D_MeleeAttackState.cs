using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject {
  public int attackDamage = 1;
  public float attackRadius = 0.25f;
  public float attackCooldown = 0.2f;
  public LayerMask whatIsPlayer;
}
