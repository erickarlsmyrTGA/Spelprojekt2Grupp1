using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{

    private Button myBtn;
    [SerializeField] private GameObject myPlayButton;
    [SerializeField] private TextMeshProUGUI myLevelText;
    private int myWorld = 1;
    private int myLevel = 1;

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

            ++myLevel;
            myLevelText.SetText(myWorld + "-" + myLevel);
        }

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel - 1);
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

            --myLevel;
            myLevelText.SetText(myWorld + "-" + myLevel);
        }

        string path = SceneUtility.GetScenePathByBuildIndex(mySelectedLevel - 1);
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

        myWorld = 1;
        myLevel = 1;
        myLevelText.SetText(myWorld + "-" + myLevel);

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

        myWorld = 2;
        myLevel = 1;
        myLevelText.SetText(myWorld + "-" + myLevel);

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

        myWorld = 3;
        myLevel = 1;
        myLevelText.SetText(myWorld + "-" + myLevel);

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
