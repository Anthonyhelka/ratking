using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {
  public bool canDash { get; private set; }
  private bool isHolding;
  private bool dashInputStop;
  private float lastDashTime;
  private Vector2 dashDirection;
  private Vector2 dashDirectionInput;
  private Vector2 lastAfterImagePosition;

  public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();

    canDash = false;
    player.inputHandler.UseDashInput();
    isHolding = true;
    dashDirection = Vector2.right * player.facingDirection;
    Time.timeScale = playerData.holdTimeScale;
    Time.fixedDeltaTime = 0.02F * Time.timeScale;
    startTime = Time.unscaledTime;
    player.dashDirectionIndicator.gameObject.SetActive(true);
  }

  public override void Exit() {
    base.Exit();

    if (player.currentVelocity.y > 0.0f) {
      player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
    }
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!isExitingState) {
      player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
      player.animator.SetFloat("yVelocity", player.currentVelocity.y);
      if (isHolding) {
        dashDirectionInput = player.inputHandler.dashDirectionInput;
        dashInputStop = player.inputHandler.dashInputStop;
        if (dashDirectionInput != Vector2.zero) {
          dashDirection = dashDirectionInput;
          dashDirection.Normalize();
        }
        float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
        player.dashDirectionIndicator.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f);

        if (dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime) {
          isHolding = false;
          Time.timeScale = 1.0f;
          Time.fixedDeltaTime = 0.02F ;
          startTime = Time.time;
          player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
          player.rb.drag = playerData.drag;
          player.SetVelocity(playerData.dashVelocity, dashDirection);
          player.dashDirectionIndicator.gameObject.SetActive(false);
          PlaceAfterImage();
        }
      } else {
        player.SetVelocity(playerData.dashVelocity, dashDirection);
        CheckIfShouldPlaceAfterImage();

        if (Time.time >= startTime + playerData.dashTime) {
          player.rb.drag = 0.0f;
          isAbilityDone = true;
          lastDashTime = Time.time;
        }
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public bool CanDash() {
    return canDash && Time.time >= lastDashTime + playerData.dashCooldown;
  }

  public void ResetDash() {
    canDash = true;
  }

  private void PlaceAfterImage() {
    PlayerAfterImagePool.Instance.GetFromPool();
    lastAfterImagePosition = player.transform.position;
  }

  private void CheckIfShouldPlaceAfterImage() {
    if (Vector2.Distance(player.transform.position, lastAfterImagePosition) >= playerData.distanceBetweenAfterImages) {
      PlaceAfterImage();
    }
  }
}