using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public GameData.StageData myStageData { get; private set; }

    public void OnPickUp(SnowflakeTile collectable)
    {
        myStageData.myCollectables.Add(collectable);

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

        myStageData = GameManager.ourInstance.GetSavedCurrentStageData();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}