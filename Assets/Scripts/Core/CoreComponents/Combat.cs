using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable {
  [SerializeField] private float maxKnockbackTime = 0.2f;
  [SerializeField] private float knockbackMultiplier = 1.0f;
  public bool isKnockbackActive;
  private float knockbackStartTime;

  public void LogicUpdate() {
    CheckKnockback();
  }

  public void Damage(AttackDetails attackDetails) {
    Debug.Log(core.transform.parent.name + " Damaged!");
  }

  public void Knockback(Vector2 angle, float strength, int direction) {
    if (isKnockbackActive) {
      core.Movement.CanSetVelocity = true;
    }
    core.Movement.SetVelocityZero();
    core.Movement.SetVelocity(strength * knockbackMultiplier, angle, direction);
    core.Movement.CanSetVelocity = false;
    isKnockbackActive = true;
    knockbackStartTime = Time.time;
  }

  private void CheckKnockback() {
    if (isKnockbackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Grounded) || Time.time >= knockbackStartTime + maxKnockbackTime)) {
      isKnockbackActive = false;
      core.Movement.CanSetVelocity = true;
    }
  }
}
