using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent {
  [SerializeField] private Transform groundCheck;
  public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
  [SerializeField] private float groundCheckRadius;
  [SerializeField] private LayerMask whatIsGround;

  public bool Grounded => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

  public void DrawGizmos() {
    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
  }
}
