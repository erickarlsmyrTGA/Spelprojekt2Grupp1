﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private Button myBtn;
    [SerializeField] private GameObject myPlayButton;

    private int mySnowMax = 5;
    private int mySnowMin = 2;

    private int myFireMax = 9;
    private int myFireMin = 6;

    private int myHatMax = 13;
    private int myHatMin = 10;

    private int myLevelAccessMax = 5;
    private int myLevelAccessMin = 2;
    private int mySelectedLevel = 2;
    private int myNormalize = 1;

    

    public void PlaySelected()
    {
        SceneManager.LoadScene(mySelectedLevel);
    }

    public void MoveNextLevel()
    {
        if (mySelectedLevel < myLevelAccessMax)
        {
            ++mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
        }

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    public void MovePreviousLevel()
    {
        if (mySelectedLevel > myLevelAccessMin)
        {
            --mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
        }

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    public void ReturnMainMenu()
    {
        mySelectedLevel = mySnowMin;
        myLevelAccessMax = mySnowMax;
        myLevelAccessMin = mySnowMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
        SceneManager.LoadScene(0);

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    public void Theme_SnowButton()
    {
        mySelectedLevel = mySnowMin;
        myLevelAccessMax = mySnowMax;
        myLevelAccessMin = mySnowMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    public void Theme_FireButton()
    {
        mySelectedLevel = myFireMin;
        myLevelAccessMax = myFireMax;
        myLevelAccessMin = myFireMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    public void Theme_HatButton()
    {
        mySelectedLevel = myHatMin;
        myLevelAccessMax = myHatMax;
        myLevelAccessMin = myHatMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel);
        bool isLevelUnlocked = GameManager.ourInstance.IsStageCleared(path);
        if (isLevelUnlocked == true || mySelectedLevel == 2)
        {
            myBtn.interactable = true;

            Debug.Log("Playing level " + mySelectedLevel);
        }
        else
        {
            myBtn.interactable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myBtn = myPlayButton.GetComponent<Button>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
