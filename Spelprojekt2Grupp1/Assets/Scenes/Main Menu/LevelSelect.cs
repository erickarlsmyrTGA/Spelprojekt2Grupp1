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
    [SerializeField] public GameObject myPlayButton;

    int mySelectedLevel = 1;
    int myLevelAccessMax = 6;
    int myLevelAccessMin = 1;

    public void MoveNextLevel()
    {
        if(mySelectedLevel < myLevelAccessMax)
        {
            ++mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
        }

    }

    public void MovePreviousLevel()
    {
        if (mySelectedLevel > myLevelAccessMin)
        {
            --mySelectedLevel;
            //myButtonText.text = mySelectedLevel.ToString();
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();

        }

    }

    public void ReturnMainMenu()
    {
        mySelectedLevel = 1;
        myLevelAccessMax = 6;
        myLevelAccessMin = 1;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
        SceneManager.LoadScene(0);
    }

    public void PlaySelected()
    {
        //SceneManager.LoadScene(mySelectedLevel);
        Debug.Log("Playing level " + mySelectedLevel);
    }

    public void Theme_SnowButton()
    {
        mySelectedLevel = 1;
        myLevelAccessMax = 6;
        myLevelAccessMin = 1;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
    }

    public void Theme_FireButton()
    {
        mySelectedLevel = 7;
        myLevelAccessMax = 12;
        myLevelAccessMin = 7;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
    }

    public void Theme_HatButton()
    {
        mySelectedLevel = 13;
        myLevelAccessMax = 18;
        myLevelAccessMin = 13;
        myBtn.GetComponentInChildren<Text>().text = "Level " + mySelectedLevel.ToString();
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
