using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public bool paused = false;
  private GameObject _pauseMenu;
  private PlayerController _playerControllerScript;
  private PlayerCombat _playerCombatScript;
  private GameOverMenu _gameOverMenuScript;

  void Awake() {
    _pauseMenu = GameObject.Find("Pause_Menu");
    _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    _playerCombatScript = GameObject.Find("Player").GetComponent<PlayerCombat>();
    _gameOverMenuScript = GetComponent<GameOverMenu>();
    _pauseMenu.SetActive(false);
  }

  void Update() {
    if (Input.GetButtonDown("Cancel") && !_gameOverMenuScript.gameOver) {
      if (paused) {
        Resume();
      } else {
        Pause();
      }
    }
  }

  public void Resume() {
    _pauseMenu.SetActive(false);
    _playerControllerScript.enabled = true;
    _playerCombatScript.enabled = true; 
    Time.timeScale = 1.0f;
    paused = false;
  }

  void Pause() {
    _pauseMenu.SetActive(true);
    _playerControllerScript.enabled = false;
    _playerCombatScript.enabled = false; 
    Time.timeScale = 0.0f;
    paused = true;
  }

  public void Restart() {
    SceneManager.LoadScene("Beta_Level_1");
    Resume();
  }

  public void Quit() {
    Application.Quit();
  }
}
