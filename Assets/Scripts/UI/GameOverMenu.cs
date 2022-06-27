using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
  public bool gameOver = false;
  private GameObject _gameOverMenu;

  void Awake() {
    _gameOverMenu = GameObject.Find("Game_Over_Menu");
    _gameOverMenu.SetActive(false);
  }

  public void GameOver() {
    _gameOverMenu.SetActive(true);
    gameOver = true;
  }

  public void Restart() {
    SceneManager.LoadScene("Beta_Level_1");
    _gameOverMenu.SetActive(false);
    gameOver = false;
  }

  public void Quit() {
    Application.Quit();
  }
}
