using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData {
  [SerializeField] private WeaponAttackDetails[] weaponAttackDetails;
  public WeaponAttackDetails[] WeaponAttackDetails { get => weaponAttackDetails; set => weaponAttackDetails = value; }
  
  private void OnEnable() {
    amountOfAttacks = weaponAttackDetails.Length;
    movementSpeed = new float[amountOfAttacks];

    for (int i = 0; i < amountOfAttacks; i++) {
      movementSpeed[i] = weaponAttackDetails[i].movementSpeed;
    }
  }
}
