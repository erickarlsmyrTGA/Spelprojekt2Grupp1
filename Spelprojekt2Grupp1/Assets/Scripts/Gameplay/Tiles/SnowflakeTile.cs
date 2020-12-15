using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowflakeTile : Tile
{
    bool myIsPickedUpThisPlay = false;
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
        myId = ++myIdGenerator; // If order is not guaranteed - set Id:s manually in Serialized Field
        CollectableManager.ourInstance.IncreaseLocalMaxCount();
        // Check if already picked up
        bool isPickedUp = CollectableManager.ourInstance.IsSnowflakePickedUp(myId);
        if (isPickedUp)
        {
            // enable transparancy
            var material = mySnowflake.GetComponentInChildren<Renderer>().material;
            material.DisableKeyword("_EMISSION");
            Color color = material.color;
            color.a = 0.5f;
            material.SetColor("_Color", color);
        }


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
        if (myIsPickedUpThisPlay == false)
        {
            myIsPickedUpThisPlay = true;
            CollectableManager.ourInstance.OnPickUp(this);
            GameManager.ourInstance.myAudioManager.PlaySFXClip("Magic_Bubble");
        }        
        mySnowflake.SetActive(false);

        yield return null;

    }

}