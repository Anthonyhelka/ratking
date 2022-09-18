using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBlockStateData", menuName = "Data/State Data/Block State")]
public class D_BlockState : ScriptableObject {
  public float minBlockTime = 1.0f;

  public bool touchDamage = false;
}
