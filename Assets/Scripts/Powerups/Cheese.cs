using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {
  [SerializeField] private int healAmount = 1;

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.transform.tag == "Player") {
      collision.gameObject.transform.SendMessage("Heal", healAmount);
      Destroy(gameObject);
    }
  }
}
