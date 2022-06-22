using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
  public Vector2 RawMovementInput { get; private set; }
  public int NormalizedInputX { get; private set; }
  public int NormalizedInputY { get; private set; }
  public bool JumpInput { get; private set; }
  public bool JumpInputStop { get; private set; }
  [SerializeField] private float inputHoldTime = 0.2f;
  private float jumpInputStartTime;
  public bool DashInput { get; private set; }
  public bool SpecialInput { get; private set; }
  public bool SpecialInputStop { get; private set; }

  private void Update() {
    CheckJumpInputHoldTime();
  }

  public void OnMoveInput(InputAction.CallbackContext context) {
    RawMovementInput = context.ReadValue<Vector2>();

    if (Mathf.Abs(RawMovementInput.x) > 0.5f) {
      NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
    } else {
      NormalizedInputX = 0;
    }

    if (Mathf.Abs(RawMovementInput.y) > 0.5f) {
      NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    } else {
      NormalizedInputY = 0;
    }
  }

  public void OnJumpInput(InputAction.CallbackContext context) {
    if (context.started) {
      JumpInput = true;
      JumpInputStop = false;
      jumpInputStartTime = Time.time;
    }

    if (context.canceled) {
      JumpInputStop = true;
    }
  }

  public void UseJumpInput() {
    JumpInput = false;
  }

  private void CheckJumpInputHoldTime() {
    if (Time.time >= jumpInputStartTime + inputHoldTime) {
      JumpInput = false;
    }
  }

  public void OnDashInput(InputAction.CallbackContext context) {
    if (context.performed) {
      Debug.Log("DASH INPUT");
      DashInput = true;
    }
  }

  public void UseDashInput() {
    DashInput = false;
  }
  
  public void OnSpecialInput(InputAction.CallbackContext context) {
    if (context.started) {
      if (GetComponent<Player>().BoomerangThrowState.CanThrowBoomerang()) {
        SpecialInput = true;
        SpecialInputStop = false;
      }
    }

    if (context.canceled) {
      SpecialInputStop = true;
    }
  }

  public void UseSpecialInput() {
    SpecialInput = false;
  }
}
