using System;
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
  private float inputHoldTime = 0.2f;
  private float jumpInputStartTime;
  public bool DashInput { get; private set; }
  public bool SpecialInput { get; private set; }
  public bool SpecialInputStop { get; private set; }
  public bool PrimaryAttackInput { get; private set; }
  public bool SecondaryAttackInput { get; private set; }
  public bool Paused { get; private set; }
  private PauseMenu pauseMenu;

  private void Awake() {
    pauseMenu = GameObject.Find("UI").GetComponent<PauseMenu>();
  }

  private void Update() {
    CheckJumpInputHoldTime();
  }

  public void OnMoveInput(InputAction.CallbackContext context) {
    RawMovementInput = context.ReadValue<Vector2>();

    NormalizedInputX = Mathf.RoundToInt(RawMovementInput.x);
    NormalizedInputY = Mathf.RoundToInt(RawMovementInput.y);
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
      DashInput = true;
    }
  }

  public void UseDashInput() {
    DashInput = false;
  }
  
  public void OnPrimaryAttackInput(InputAction.CallbackContext context) {
    if (context.started) {
      PrimaryAttackInput = true;
    }

    if (context.canceled) {
      PrimaryAttackInput = false;
    }
  }

  public void OnSecondaryAttackInput(InputAction.CallbackContext context) {
    if (context.started) {
      SecondaryAttackInput = true;
    }

    if (context.canceled) {
      SecondaryAttackInput = false;
    }
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

  public void OnPauseInput(InputAction.CallbackContext context) {
    if (context.started) {
      Paused = !Paused;
      if (Paused) {
        pauseMenu.Resume();
      } else {
        pauseMenu.Pause();
      }
    }
  }
}
