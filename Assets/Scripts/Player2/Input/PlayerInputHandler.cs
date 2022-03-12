using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
  private PlayerInput playerInput;
  private Camera camera;
  public Vector2 rawMovementInput { get; private set; }
  public Vector2 rawDashDirectionInput { get; private set; }
  public Vector2Int dashDirectionInput { get; private set; }
  public int normalizedInputX { get; private set; }
  public int normalizedInputY { get; private set; }
  public bool jumpInput { get; private set; }
  public bool jumpInputStop { get; private set; }
  public bool grabInput { get; private set; }
  public bool dashInput { get; private set; }
  public bool dashInputStop { get; private set; }
  [SerializeField] private float inputHoldTime = 0.2f;
  private float jumpInputStartTime;
  private float dashInputStartTime;

  private void Start() {
    playerInput = GetComponent<PlayerInput>();
    camera = Camera.main;
  }

  private void Update() {
    CheckJumpInputHoldTime();
    CheckDashInputHoldTime();
  }

  public void OnMoveInput(InputAction.CallbackContext context) {
    rawMovementInput = context.ReadValue<Vector2>();

    if (Mathf.Abs(rawMovementInput.x) > 0.5f) {
      normalizedInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
    } else {
      normalizedInputX = 0;
    }
    if (Mathf.Abs(rawMovementInput.y) > 0.5f) {
      normalizedInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
    } else {
      normalizedInputY = 0;
    }
  }

  public void OnJumpInput(InputAction.CallbackContext context) {
    if (context.started) {
      jumpInput = true;
      jumpInputStop = false;
      jumpInputStartTime = Time.time;
    }
    if (context.canceled) {
      jumpInputStop = true;
    }
  }

  public void UseJumpInput() {
    jumpInput = false;
  }

  private void CheckJumpInputHoldTime() {
    if (Time.time >= jumpInputStartTime + inputHoldTime) {
      jumpInput = false;
    }
  }

  public void OnGrabInput(InputAction.CallbackContext context) {
    if (context.started) {
      grabInput = true;
    }
    if (context.canceled) {
      grabInput = false;
    }
  }

  public void OnDashInput(InputAction.CallbackContext context) {
    if (context.started) {
      dashInput = true;
      dashInputStop = false;
      dashInputStartTime = Time.time;
    }
    if (context.canceled) {
      dashInputStop = true;
    }
  }

  public void UseDashInput() {
    dashInput = false;
  }

  private void CheckDashInputHoldTime() {
    if (Time.time >= dashInputStartTime + inputHoldTime) {
      dashInput = false;
    }
  }

  public void OnDashDirection(InputAction.CallbackContext context) {
    rawDashDirectionInput = context.ReadValue<Vector2>();

    if (playerInput.currentControlScheme == "Keyboard") {
      rawDashDirectionInput = camera.ScreenToWorldPoint((Vector3)rawDashDirectionInput) - transform.position;
    }

    dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput);
  }
}
