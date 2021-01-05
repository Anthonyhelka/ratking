using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{   
  private Rigidbody2D _rb;
  [SerializeField] private float _speed = 0.3f;
  [SerializeField] private float _distance  = 0.1f;
  [SerializeField] private bool _movingRight;
  [SerializeField] private Transform _groundDetection;
  [SerializeField] private List<string> UnwalkableGround = new List<string>() { "Walls", "Spikes" };

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {

    RaycastHit2D groundInformation = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);
    if (groundInformation.collider == false || UnwalkableGround.Contains(groundInformation.collider.tag) || groundInformation.collider.tag == "Player") {
      if (_movingRight == true) {
        _rb.velocity = new Vector2(-1.0f * _speed, _rb.velocity.y);
        transform.eulerAngles = new Vector2(0, -180);
        _movingRight = false;
      } else {
        _rb.velocity = new Vector2(1.0f * _speed, _rb.velocity.y);
        transform.eulerAngles = new Vector2(0, 0);
        _movingRight = true;
      }
    }
  }

  public void Stop() {
    _rb.velocity = new Vector2(0, 0);
    this.enabled = false;
  }
}
