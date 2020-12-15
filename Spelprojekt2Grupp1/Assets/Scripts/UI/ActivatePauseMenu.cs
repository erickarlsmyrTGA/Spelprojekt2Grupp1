using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePauseMenu : MonoBehaviour
{
   [SerializeField] Player myPlayer;
   [SerializeField] GameObject myUIMenu;
   [SerializeField] GameObject myPauseMenu;
   [SerializeField] GameObject mySettingsMenu;

    void OnApplicationPause(bool aPause)
   {
      if (aPause == true)
      {
         myPlayer.myGameIsOn = false;
         myPauseMenu.SetActive(true);
         Debug.Log("Pausad!");
      }
   }

   private void OnApplicationFocus(bool aFocus)
   {
      if (aFocus == false)
      {
         myPlayer.myGameIsOn = false;
         myPauseMenu.SetActive(true);
         Debug.Log("Ur Fokus!");
      }
   }

   public void ActivateGame()
   {
      myPlayer.myGameIsOn = true;
      myPauseMenu.SetActive(false);
      Debug.Log("INTE pausad!");
   }

    public void EnterPauseMenu()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
        myPlayer.myGameIsOn = false;
        myUIMenu.SetActive(false);
        myPauseMenu.SetActive(true);
    }

    public void EnterSettingMenu()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
        myPauseMenu.SetActive(false);
        mySettingsMenu.SetActive(true);
    }

    public void ReturnToPause()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        myPauseMenu.SetActive(true);
        mySettingsMenu.SetActive(false);
    }

    public void ReturnToGame()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        myPlayer.myGameIsOn = true;
        myUIMenu.SetActive(true);
        myPauseMenu.SetActive(false);
    }

    public void ResetLevel()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        GameManager.ourInstance.RestartCurrentStage();
    }

    public void LoadMainMenu()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        Debug.Log("Loading menu...");
        SceneManager.LoadScene(0); // TODO: use fader - update StageManager
        GameManager.ourInstance.StartOrChangeMusic(0);

    }
}
