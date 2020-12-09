using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //[SerializeField] private GameObject mySnowButton;
    //[SerializeField] private GameObject myFireButton;
    //[SerializeField] private GameObject myHatButton;

    //[SerializeField] private GameObject myLeftButton;
    //[SerializeField] private GameObject myRightButton;

    //[SerializeField] public Sprite mySprite;
    //Image myImageComponent;

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

    public void MoveNextLevel()
    {
        if(mySelectedLevel < myLevelAccessMax)
        {
            ++mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
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

    }

    public void ReturnMainMenu()
    {
        mySelectedLevel = mySnowMin;
        myLevelAccessMax = mySnowMax;
        myLevelAccessMin = mySnowMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
        SceneManager.LoadScene(0);
    }

    public void PlaySelected()
    {
        SceneManager.LoadScene(mySelectedLevel);
        Debug.Log("Playing level " + mySelectedLevel);
    }

    public void Theme_SnowButton()
    {
        mySelectedLevel = mySnowMin;
        myLevelAccessMax = mySnowMax;
        myLevelAccessMin = mySnowMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
    }

    public void Theme_FireButton()
    {
        mySelectedLevel = myFireMin;
        myLevelAccessMax = myFireMax;
        myLevelAccessMin = myFireMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
    }

    public void Theme_HatButton()
    {
        mySelectedLevel = myHatMin;
        myLevelAccessMax = myHatMax;
        myLevelAccessMin = myHatMin;
        myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel - myNormalize).ToString();
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
