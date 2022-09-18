using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCooldownStateData", menuName = "Data/State Data/Cooldown State")]
public class D_CooldownState : ScriptableObject {
  public float cooldownTime = 1.0f;

  public bool touchDamage = false;
}