using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{   
    public float _speed = 0.3f;
    public float _distance  = 0.1f;
    public bool _movingRight;
    public Transform _groundDetection;

    void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        RaycastHit2D groundInformation = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);
        if (groundInformation.collider == false) {
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

