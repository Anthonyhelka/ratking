using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject {
  public GameObject projectile;
  public int projectileDamage = 1;
  public float projectileSpeed = 12.0f;
  public Vector2 projectileDirection = new Vector2(1, 0);
  public float projectTravelDistance = 5.0f;
  public float attackCooldown = 1.0f;

  public bool touchDamage = false;
}

