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
  public bool DodgeInput { get; private set; }
  public bool SpecialInput { get; private set; }
  public bool SpecialInputStop { get; private set; }
  public bool PrimaryAttackInput { get; private set; }
  public bool SecondaryAttackInput { get; private set; }
  public bool DanceInput { get; private set; }
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
    if (context.started && !Paused) {
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
    if (context.performed && !Paused) {
      DashInput = true;
    }
  }

  public void UseDashInput() {
    DashInput = false;
  }
  
  public void OnDodgeInput(InputAction.CallbackContext context) {
    if (context.performed && !Paused) {
      DodgeInput = true;
    }
  }

  public void UseDodgeInput() {
    DodgeInput = false;
  }

  public void OnPrimaryAttackInput(InputAction.CallbackContext context) {
    if (context.started && !Paused) {
      PrimaryAttackInput = true;
    }

    if (context.canceled) {
      PrimaryAttackInput = false;
    }
  }

  public void OnSecondaryAttackInput(InputAction.CallbackContext context) {
    if (context.started && !Paused) {
      SecondaryAttackInput = true;
    }

    if (context.canceled) {
      SecondaryAttackInput = false;
    }
  }

  public void OnSpecialInput(InputAction.CallbackContext context) {
    if (context.started && !Paused) {
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

  public void OnDanceInput(InputAction.CallbackContext context) {
    if (context.started && !Paused) {
      Debug.Log("hiu");
      DanceInput = true;
    }
  }

  public void UseDanceInput() {
    DanceInput = false;
  }

  public void OnPauseInput(InputAction.CallbackContext context) {
    if (context.started) {
      if (Paused) {
        Paused = false;
        pauseMenu.Resume();
      } else {
        Paused = true;
        pauseMenu.Pause();
      }
    }
  }

  public void Unpause() {
    Paused = false;
  }
}
