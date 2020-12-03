using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool myGameIsPaused = false;
    public GameObject myPauseMenu;
    public GameObject myInGameUI;
   [SerializeField] GameObject myOptionsMenu;


    public void Resume()
    {
        myPauseMenu.SetActive(false);
        myInGameUI.SetActive(true);
        Time.timeScale = 1f; // stops ingame time
        myGameIsPaused = false;
    }
    public void Pause()
    {
        myPauseMenu.SetActive(true);
        myInGameUI.SetActive(false);
        Time.timeScale = 0f; 
        myGameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {        
        GameManager.ourInstance.RestartCurrentStage();
    }

   public void ActivateOptionsMenu()
   {
      myOptionsMenu.SetActive(true);
      myPauseMenu.SetActive(false);
   }
}
