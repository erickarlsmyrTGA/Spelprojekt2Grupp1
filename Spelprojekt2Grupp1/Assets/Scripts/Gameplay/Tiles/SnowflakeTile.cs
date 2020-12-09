using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowflakeTile : Tile
{
    bool myIsPickedUp = false;
    [SerializeField] private GameObject mySnowflake = null;

    SnowflakeTile()
    {
        myName = "Snowflake";
        myType = TileType.Barrier | TileType.Ground;
    }
   
    static public int myIdGenerator = 0;
    public int myId { get; private set; }



    // Start is called before the first frame update
    void Awake()
    {
        myId = ++myIdGenerator;
        CollectableManager.ourInstance.IncreaseLocalMaxCount();
        Debug.Log(myId);
        
        myIsPickedUp = false;
        if (mySnowflake == null)
        {
            Debug.LogError("mySnowflake is fuckywucky");
        }
        else
        {
            mySnowflake.SetActive(true);
        }
    }

    public override IEnumerator TGAExecute()
    {
        if (myIsPickedUp == false)
        {
            myIsPickedUp = true;
            CollectableManager.ourInstance.OnPickUp(this);
            mySnowflake.SetActive(false);

            GameManager.ourInstance.myAudioManager.PlaySFXClip("Magic_Bubble");
        }

        yield return null;

    }

}