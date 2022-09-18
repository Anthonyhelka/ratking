using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent {  
  [SerializeField] private Transform groundCheck;
  public Transform GroundCheck { 
    get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
    private set => groundCheck = value;
  }
  [SerializeField] private Transform wallCheck;
  public Transform WallCheck { 
    get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
    private set => wallCheck = value;
  }
  [SerializeField] private Transform ledgeCheckHorizontal;
  public Transform LedgeCheckHorizontal { 
    get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
    private set => ledgeCheckHorizontal = value;
  }
  [SerializeField] private Transform ledgeCheckVertical;
  public Transform LedgeCheckVertical { 
    get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
    private set => ledgeCheckVertical = value;
  }
  [SerializeField] private float groundCheckRadius;
  [SerializeField] private float wallCheckDistance;
  [SerializeField] private float ledgeCheckDistance;
  [SerializeField] private LayerMask whatIsGround;

  public bool Grounded => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);

  public bool WallFront => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);

  public bool WallBack => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);

  public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, ledgeCheckDistance, whatIsGround);

  public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, ledgeCheckDistance, whatIsGround);

  public void DrawGizmos() {
    Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
    Gizmos.DrawLine(WallCheck.position, WallCheck.position + (Vector3)(Vector2.right * core.Movement.FacingDirection * wallCheckDistance));
    Gizmos.DrawLine(LedgeCheckVertical.position, LedgeCheckVertical.position + (Vector3)(Vector2.down * ledgeCheckDistance));
  }
}
