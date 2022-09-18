using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class D_ChargeState : ScriptableObject {
  public float chargeSpeed = 1.5f;
  public float chargeTime = 1.0f;
  public float chargeCooldown = 1.0f;

  public bool touchDamage = false;
}
