using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  private Player player;
  private GameObject pauseMenu;
  
  void Awake() {
    player = GameObject.Find("Player").GetComponent<Player>();
    pauseMenu = GameObject.Find("Pause_Menu");
    pauseMenu.SetActive(false);
  }

  public void Resume() {
    player.enabled = true;
    pauseMenu.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public void Pause() {
    player.enabled = false;
    pauseMenu.SetActive(true);
    Time.timeScale = 0.0f;
  }

  public void Restart() {
    SceneManager.LoadScene("Beta_Level_1");
    Resume();
  }

  public void Quit() {
    Application.Quit();
  }
}
