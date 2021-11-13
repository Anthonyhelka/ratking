using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
  public bool gameOver = false;
  private GameObject _gameOverMenu;
  private PlayerController _playerControllerScript;
  private PlayerCombat _playerCombatScript;

  void Awake() {
    _gameOverMenu = GameObject.Find("Game_Over_Menu");
    _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    _playerCombatScript = GameObject.Find("Player").GetComponent<PlayerCombat>();
    _gameOverMenu.SetActive(false);
  }

  public void GameOver() {
    _gameOverMenu.SetActive(true);
    _playerControllerScript.enabled = false;
    _playerCombatScript.enabled = false; 
    gameOver = true;
  }

  public void Restart() {
    SceneManager.LoadScene("Game");
    _gameOverMenu.SetActive(false);
    _playerControllerScript.enabled = true;
    _playerCombatScript.enabled = true; 
    gameOver = false;
  }

  public void Quit() {
    Application.Quit();
  }
}
