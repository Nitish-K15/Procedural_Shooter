using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
  public void Restart()
  {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        FirstPersonController.stop = false;
        WeaponSwitching.selectedWeapon = 0;
        FirstPersonController.isDead = false;
        Modifiers.instance.ResetModifiers();
  }

  public void BacktoMenu()
  {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        FirstPersonController.stop = false;
        WeaponSwitching.selectedWeapon = 0;
        FirstPersonController.isDead = false;
        Modifiers.instance.ResetModifiers();
    }

  public void Exit()
  {
        Application.Quit();
  }
}
