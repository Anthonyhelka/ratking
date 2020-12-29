using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{   
  [SerializeField] private float _speed = 0.3f;
  [SerializeField] private float _distance  = 0.1f;
  [SerializeField] private bool _movingRight;
  [SerializeField] private Transform _groundDetection;
  [SerializeField] private List<string> HarmfulGround = new List<string>() { "Spikes" };

  void Update()
  {
    transform.Translate(Vector2.right * _speed * Time.deltaTime);
    RaycastHit2D groundInformation = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);
    if (groundInformation.collider == false || HarmfulGround.Contains(groundInformation.collider.tag)) {
      if (_movingRight == true) {
        transform.eulerAngles = new Vector2(0, -180);
        _movingRight = false;
      } else {
        transform.eulerAngles = new Vector2(0, 0);
        _movingRight = true;
      }
    }
  }
}
