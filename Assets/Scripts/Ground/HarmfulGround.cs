using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulGround : MonoBehaviour {
  void OnCollisionStay2D(Collision2D collision) {
    if (collision.transform.tag == "Player") {
      AttackDetails attackDetails;
      attackDetails.position = collision.transform.position;
      attackDetails.damageAmount = 1;
      attackDetails.type = gameObject.tag;
      collision.gameObject.transform.SendMessage("Damage", attackDetails);
    } else if (collision.transform.tag == "Enemy") {
      Debug.Log("here!!");
      AttackDetails attackDetails;
      attackDetails.position = collision.transform.position;
      attackDetails.damageAmount = 10000;
      attackDetails.type = gameObject.tag;
      collision.gameObject.transform.SendMessage("Damage", attackDetails);
    }
  }
}