using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CollectableManager : MonoBehaviour
{
    public GameData.StageData myStageData { get; private set; }
    [SerializeField] TextMeshProUGUI myScoreText;

    private int myLocalMaxCount = 0;

    public void OnPickUp(SnowflakeTile collectable)
    {
        if (!myStageData.myCollectables.Contains(collectable.myId))
        {
            myStageData.myCollectables.Add(collectable.myId);

            UpdateScoreUI();
        }        
    }

    private void UpdateScoreUI()
    {
        myScoreText.text = myStageData.myCollectables.Count.ToString() + " / " + myLocalMaxCount.ToString();
    }

    internal void IncreaseLocalMaxCount()
    {
        myLocalMaxCount++;
    }

    public static CollectableManager ourInstance
    { 
        get; 
        private set; 
    }

    private void Awake()
    {
        if (ourInstance != null && ourInstance != this)
        {
            Destroy(this);
        }
        else
        {
            ourInstance = this;
        }
                
    }


    // Start is called before the first frame update
    void Start()
    {
        myStageData = GameManager.ourInstance.GetSavedCurrentStageData();        
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
    }
}