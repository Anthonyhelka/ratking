using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  private Player player;
  private GameObject pauseMenu;
  public enum Special { boomerang, shield, jetpack, glider };
  public GameObject KnightRat;
  public GameObject HammerRat;
  public GameObject NinjaRat;
  public GameObject SpearRat;
  public GameObject StrikeRat;
  public GameObject BombRat;
  public GameObject PlagueFly;

  void Awake() {
    player = GameObject.Find("Player").GetComponent<Player>();
    pauseMenu = GameObject.Find("Pause_Menu");
    pauseMenu.SetActive(false);
  }

  public void Resume() {
    player.enabled = true;
    pauseMenu.SetActive(false);
    player.GetComponent<PlayerInputHandler>().Unpause();
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
    Resume();  
  }

  public void SwapToJetpack() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.jetpack;
    Resume();  
  }

  public void SwapToShield() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.shield;
    Resume();
  }

  public void SwapToBoomerang() {
    player.GetComponent<Player>().playerData.selectedSpecial = (PlayerData.Special)Special.boomerang;
    Resume();
  }

  public void SpawnKnightRat() {
    GameObject.Instantiate(KnightRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnHammerRat() {
    GameObject.Instantiate(HammerRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnNinjaRat() {
    GameObject.Instantiate(NinjaRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnSpearRat() {
    GameObject.Instantiate(SpearRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnStrikeRat() {
    GameObject.Instantiate(StrikeRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnBombRat() {
    GameObject.Instantiate(BombRat, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }

  public void SpawnPlagueFly() {
    GameObject.Instantiate(PlagueFly, new Vector3(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
    Resume();
  }
}
