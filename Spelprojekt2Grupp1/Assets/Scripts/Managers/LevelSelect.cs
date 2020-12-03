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

    private int mySnowMax = 7;
    private int mySnowMin = 2;

    private int myFireMax = 13;
    private int myFireMin = 8;

    private int myHatMax = 19;
    private int myHatMin = 14;

    private int myLevelAccessMax = 3; // TODO set 7 as default after beta
    private int myLevelAccessMin = 0;// TODO set 2 as default after beta
    private int mySelectedLevel = 0;// TODO set 2 as default after beta
    private int myNormalize = 1;


    private int[] myBetaLevelSelect = {3, 4, 6, 7};

    public void MoveNextLevel()
    {
        if(mySelectedLevel < myLevelAccessMax)
        {
            ++mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel + myNormalize).ToString(); // TODO set * to - after beta
        }

    }

    public void MovePreviousLevel()
    {
        if (mySelectedLevel > myLevelAccessMin)
        {
            --mySelectedLevel;
            Debug.Log(mySelectedLevel);
            myBtn.GetComponentInChildren<Text>().text = "Level " + (mySelectedLevel + myNormalize).ToString(); // TODO set * to - after beta

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
        SceneManager.LoadScene(myBetaLevelSelect[mySelectedLevel]);
        Debug.Log("Playing level " + myBetaLevelSelect[mySelectedLevel]);
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
