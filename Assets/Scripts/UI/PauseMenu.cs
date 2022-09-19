using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  private Player player;
  private GameObject pauseMenu;
  public enum Special { boomerang, shield, jetpack, glider };

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

  public void SwapToGlider() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.glider;
    SceneManager.LoadScene("Beta_Level_1");
    Resume();  
  }

  public void SwapToJetpack() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.jetpack;
    SceneManager.LoadScene("Beta_Level_1");
    Resume();  
  }

  public void SwapToShield() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.shield;
    SceneManager.LoadScene("Beta_Level_1");
    Resume();
  }

  public void SwapToBoomerang() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.boomerang;
    SceneManager.LoadScene("Beta_Level_1");
    Resume();
  }
}
