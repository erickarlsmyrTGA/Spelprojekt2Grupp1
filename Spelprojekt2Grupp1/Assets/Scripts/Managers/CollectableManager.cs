﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public GameData.StageData myStageData { get; private set; }
    

    public void OnPickUp(SnowflakeTile collectable)
    {
        myStageData.myCollectables.Add(collectable.myId);

        // Notify UI
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
    }

    // Update is called once per frame
    void Update()
    {
    }
}