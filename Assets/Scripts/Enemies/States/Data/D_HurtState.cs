using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHurtStateData", menuName = "Data/State Data/Hurt State")]
public class D_HurtState : ScriptableObject {
  public float hurtTime = 1.0f;
  // Hurt damper/multiplier?
  // Hurt time/duration
  // Hurt until ground touch?
}
