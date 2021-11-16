using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {
  private Rigidbody2D rb;
  private Animator anim;

  private int facingDirection = 1;
  private int amountOfJumpsLeft;
  public int amountOfJumps = 1;

  private float horizontalInputDirection;
  private float verticalInputDirection;
  private float jumpTimer;
  private float turnTimer;
  private float wallJumpTimer;
  private float dashTimeLeft;
  private float lastImageXPosition;
  private float lastDash = -1.0f;

  public float speed = 10.0f;
  public float jumpForce = 16.0f;
  public float groundCheckRadius = 0.3f;
  public float wallCheckDistance = 0.4f;
  public float wallSlideSpeed = 2.0f;
  public float airDragMultiplier = 0.95f;
  public float variableJumpHeightMultiplier = 0.5f;
  public float wallJumpForce = 20.0f;
  public float jumpTimerSet = 0.15f;
  public float turnTimerSet =  0.1f;
  public float wallJumpTimerSet = 0.5f;
  public float dashTime = 0.2f;
  public float dashSpeed = 50.0f;
  public float distanceBetweenImages = 0.1f;
  public float dashCooldown = 2.5f;

  private bool isFacingRight = true;
  private bool isWalking;
  private bool isGrounded;
  private bool isTouchingWall;
  private bool isWallSliding;
  private bool isAttemptingToJump;
  private bool canNormalJump;
  private bool canWallJump;
  private bool checkJumpMultiplier;
  private bool canMove;
  private bool canFlip;
  private bool justWallJumped;
  private bool isDashing;

  public Transform groundCheck;
  public Transform wallCheck;

  public LayerMask whatIsGround;

  public Vector2 wallJumpDirection = new Vector2(1.0f, 2.0f);

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    amountOfJumpsLeft = amountOfJumps;
    wallJumpDirection.Normalize();
  }

  private void Update() {
    CheckInput();
    CheckMovementDirection();
    UpdateAnimations();
    CheckIfCanJump();
    CheckIfWallSliding();
    CheckJump();
    CheckDash();
  }

  private void FixedUpdate() {
    ApplyMovement();
    CheckSurroundings();
  }

  private void CheckInput() {
    verticalInputDirection = Input.GetAxisRaw("Vertical");
    horizontalInputDirection = Input.GetAxisRaw("Horizontal");

    if (Input.GetButtonDown("Jump")) {
      if (isGrounded && amountOfJumpsLeft > 0 && !isTouchingWall) {
        NormalJump();
      } else {
        jumpTimer = jumpTimerSet;
        isAttemptingToJump = true;
      }
    }

    if (checkJumpMultiplier && !Input.GetButton("Jump")) {
      checkJumpMultiplier = false;
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
    }

    if (Input.GetButtonDown("Horizontal") && isTouchingWall) {
      if (!isGrounded && horizontalInputDirection != facingDirection) {
        canMove = false;
        canFlip = false;
        turnTimer = turnTimerSet;
      }
    }

    if (!canMove) {
      turnTimer -= Time.deltaTime;
      wallJumpTimer -= Time.deltaTime;
      if (turnTimer <= 0.0f && wallJumpTimer <= 0.0f) {
        canMove = true;
        canFlip = true;
      }
    }

    if (Input.GetButtonDown("Dash")) {
      if (Time.time >= lastDash + dashCooldown)
        AttemptToDash();
      }
  }

  private void ApplyMovement() {
    if (!isGrounded && !isWallSliding && horizontalInputDirection == 0.0f) {
      rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
    } else if (canMove) {
      rb.velocity = new Vector2(horizontalInputDirection * speed, rb.velocity.y);
    }

    if (isWallSliding) {
      if (rb.velocity.y < -wallSlideSpeed) {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
      }
    }
  }

  private void CheckMovementDirection() {
    if (isFacingRight && horizontalInputDirection < 0) {
      Flip();
    } else if (!isFacingRight && horizontalInputDirection > 0) {
      Flip();
    }
    if (Mathf.Abs(rb.velocity.x) >= 0.01f) {
      isWalking = true;
    } else {
      isWalking = false;
    }
  }

  private void Flip() {
    if (canFlip) {
      facingDirection *= -1;
      isFacingRight = !isFacingRight;
      transform.Rotate(0.0f, 180.0f, 0.0f);
    }
  }

  public void DisableFlip() {
    canFlip = false;
  }

  public void EnableFlip() {
    canFlip = true;
  }

  private void CheckJump() {
    if (jumpTimer > 0.0f) {
      if (!isGrounded && isTouchingWall) {
        WallJump();
      } else if (isGrounded) {
        NormalJump();
      }
    }

    if (isAttemptingToJump) {
      jumpTimer -= Time.deltaTime;
    }
  } 

  private void NormalJump() {
    if (canNormalJump) {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
      amountOfJumpsLeft--;
      jumpTimer = 0.0f;
      isAttemptingToJump = false;
      checkJumpMultiplier = true;
    } 
  }

  private void WallJump() {
    if (canWallJump) {
      isWallSliding = false;
      wallJumpTimer = wallJumpTimerSet;
      Flip();
      canMove = false;
      canFlip = false;
      amountOfJumpsLeft = amountOfJumps;
      amountOfJumpsLeft--;
      rb.velocity = new Vector2(0.0f, 0.0f);
      Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * facingDirection, wallJumpForce * wallJumpDirection.y);
      rb.AddForce(forceToAdd, ForceMode2D.Impulse);
      jumpTimer = 0.0f;
      isAttemptingToJump = false;
      checkJumpMultiplier = true;
      turnTimer = 0.0f;
      justWallJumped = true;
    }
  }

  private void CheckIfCanJump() {
    if (isGrounded && rb.velocity.y <= 0.01f) {
      amountOfJumpsLeft = amountOfJumps;
      justWallJumped = false;
    }

    if (isTouchingWall) {
      canWallJump = true;
    }
    
    if (amountOfJumpsLeft <= 0) {
      canNormalJump = false;
    } else {
      canNormalJump = true;
    }
  }

  private void AttemptToDash() {
    isDashing = true;
    dashTimeLeft = dashTime;
    lastDash = Time.time;
    if (isWallSliding) {
      Flip();
    }
    PlayerAfterImagePool.Instance.GetFromPool();
    lastImageXPosition = transform.position.x;
  }

  private void CheckDash() {
    if (isDashing) {
      if (dashTimeLeft > 0.0f) {
        canMove = false;
        canFlip = false;
        rb.velocity = new Vector2(dashSpeed * facingDirection, 0.0f);
        rb.gravityScale = 0.0f;

        dashTimeLeft -= Time.deltaTime;

        if (Mathf.Abs(transform.position.x - lastImageXPosition) > distanceBetweenImages) {
          PlayerAfterImagePool.Instance.GetFromPool();
          lastImageXPosition = transform.position.x;
        }
      } 
      
      if (dashTimeLeft <= 0.0f || (isTouchingWall && !isWallSliding)) {
        isDashing = false;
        canMove = true;
        canFlip = true;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, 0.0f, speed), rb.velocity.y);
        rb.gravityScale = 8.0f;
      }
    }
  }

  private void UpdateAnimations() {
    anim.SetBool("isWalking", isWalking);
    anim.SetBool("isGrounded", isGrounded);
    anim.SetBool("isWallSliding", isWallSliding);
    anim.SetFloat("yVelocity", rb.velocity.y);
  }

  private void CheckSurroundings() {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
  }

  private void CheckIfWallSliding() {
    if (!isGrounded && isTouchingWall && rb.velocity.y < 0.01f && (horizontalInputDirection == facingDirection || justWallJumped)) {
      isWallSliding = true;
    } else if (isGrounded || !isTouchingWall) {
      isWallSliding = false;
    }
  }

  public int GetFacingDirection() {
    return facingDirection;
  }

  private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
  }
}
