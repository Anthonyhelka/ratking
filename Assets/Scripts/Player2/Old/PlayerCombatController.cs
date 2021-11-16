using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour {
  private Animator anim;

  [SerializeField] private bool combatEnabled;
  private bool gotInput;
  private bool isAttacking;
  private bool isFirstAttack;

  [SerializeField] private float inputTimer;
  private float lastInputTime = -1.0f;
  [SerializeField] private float attack1Radius;
  [SerializeField] private float attack1Damage;

  [SerializeField] private Transform attack1HitboxPosition;
  private LayerMask whatIsDamageable;

  private void Start() {
    anim = GetComponent<Animator>();
    anim.SetBool("canAttack", combatEnabled);
  }

  private void Update() {
    CheckCombatInput();
    CheckAttacks();
  }

  private void CheckCombatInput() {
    if (Input.GetButtonDown("Fire1")) {
      if (combatEnabled) {
        gotInput = true;
        lastInputTime = Time.time;
      }
    }
  }

  private void CheckAttacks() {
    if (gotInput) {
      if (!isAttacking) {
        gotInput = false;
        isAttacking = true;
        isFirstAttack = !isFirstAttack;
        anim.SetBool("attack1", true);
        anim.SetBool("firstAttack", isFirstAttack);
        anim.SetBool("isAttacking", isAttacking);
      }
    }

    if (Time.time >= lastInputTime + inputTimer) {
      gotInput = false;
    }
  }

  private void CheckAttack1Hitbox() {
    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitboxPosition.position, attack1Radius, whatIsDamageable);

    foreach (Collider2D collider in detectedObjects) {
      collider.transform.parent.SendMessage("Damage", attack1Damage);
    }
  }

  private void FinishAttack1() {
    isAttacking = false;
    anim.SetBool("isAttacking", isAttacking);
    anim.SetBool("attack1", false);
  }

  private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(attack1HitboxPosition.position, attack1Radius);
  }
}
