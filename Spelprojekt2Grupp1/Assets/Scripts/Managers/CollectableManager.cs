using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public int mySnowflakeScore = 0;    
    GameData.StageData myStageData = GameData.StageData.ourInvalid;

    public void OnPickUp(SnowflakeTile collectable)
    {
        myStageData.myCollectables.Add(collectable);
        mySnowflakeScore += 1;

        // Notify UI
    }

    #region Singleton Pattern
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
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        myStageData = GameManager.ourInstance.GetStageData();
    }

    // Update is called once per frame
    void Update()
    {

    }
}