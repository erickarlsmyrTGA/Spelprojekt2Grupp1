using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public int mySnowflakeScore = 0;

    public void OnPickUp()
    {
        mySnowflakeScore += 1;
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}