using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject myMainMenu;
    [SerializeField] private GameObject mySettingsMenu;
    [SerializeField] private GameObject myCredits;

    public void PlayButton()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
        SceneManager.LoadScene(1);
    }

    public void EnterSettings()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
        myMainMenu.SetActive(false);
        mySettingsMenu.SetActive(true);
    }

    public void ExitSettings()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        myMainMenu.SetActive(true);
        mySettingsMenu.SetActive(false);
    }

    public void EnterCredits()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
        myMainMenu.SetActive(false);
        myCredits.SetActive(true);
    }

    public void ExitCredits()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
        myMainMenu.SetActive(true);
        myCredits.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Scene: QUIT");
        Application.Quit();
    }
}
