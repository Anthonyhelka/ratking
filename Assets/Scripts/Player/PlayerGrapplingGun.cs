using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapplingGun : MonoBehaviour
{
  [SerializeField] private Transform _grapplingGun;
  [SerializeField] private float _grapplingGunCheckDistance;
  [SerializeField] private LayerMask _whatIsGrappable;
  [SerializeField] private LineRenderer _lr;
  [SerializeField] private bool hasHit;
  [SerializeField] private Vector2 hitPosition;
  [SerializeField] private Rigidbody2D _rb;
  [SerializeField] private float _pullForce;
  [SerializeField] private PlayerController _playerControllerScript;
  [SerializeField] private float _lastDistance = 0.0f;
  [SerializeField] private float _currentDistance = 0.0f;
  [SerializeField] private float _ropeStrength;
  private IEnumerator _grappleRoutine;

  public void ShootGun() {
    if (hasHit) {
      StopCoroutine(_grappleRoutine);
      hasHit = false;
      return;
    }

    Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    RaycastHit2D hit = Physics2D.Raycast(_grapplingGun.position, direction, _grapplingGunCheckDistance, _whatIsGrappable);

    if (hit.transform != null) {
      hasHit = true;
      hitPosition = hit.point;
      Debug.Log(hitPosition);
      // DrawRope();
      // hitPosition.x += hitPosition.x / 10;
      // hitPosition.y += Mathf.Abs(hitPosition.y) / 10;
      DrawRope();
    }
  }

  void Update() {
  }

  void FixedUpdate() {
    if (hasHit) {
      hasHit = false;
      _grappleRoutine = GrappleRoutine();
      StartCoroutine(_grappleRoutine);
    }
  }

  private void DrawRope() {
    _lr.SetPosition(0, transform.position);
    _lr.SetPosition(1, hitPosition);
  }

  IEnumerator GrappleRoutine() {
    _playerControllerScript._lockPlayerInput = true;
    _playerControllerScript.Grappling = true;
    _rb.velocity = new Vector2(0, 0);
    float duration = 0.0f;
    Vector2 trackVelocity;
    Vector2 lastPos = new Vector3(0, 0);
    while (duration <= 0.25f) {

      if (Input.GetButtonDown("Grapple")) {
        break;
      }

      _currentDistance = Vector3.Distance(hitPosition, transform.position);


      // if (_currentDistance < _ropeStrength) break;
      _rb.MovePosition(Vector3.MoveTowards(transform.position, hitPosition, _ropeStrength * Time.deltaTime));
      // trackVelocity = (_rb.position - lastPos) * 50;
      // lastPos = _rb.position;
      // Debug.Log(trackVelocity);
      // _rb.MovePosition(Vector3.Lerp(transform.position, hitPosition, _ropeStrength));

      // _rb.AddForce(((Vector3)(hitPosition) - transform.position) * _pullForce);

      _currentDistance = Vector3.Distance(hitPosition, transform.position);

      duration += Time.deltaTime;
      yield return 0;
    }

    _playerControllerScript._lockPlayerInput = false;
    _playerControllerScript.Grappling = false;
    _lastDistance = 0.0f;
  }

  void OnDrawGizmos() {
    Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    Debug.DrawLine(transform.position, transform.position + (Vector3)(direction * _grapplingGunCheckDistance));
  }
}
