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
    /*
     * 
     * Det måste nog finnas en privat player-klass 
     * att referera till m. typ myPlayer. 
     * Skapa en private void OnCollisionEnter,
     * alt en OnTriggerEnter. Beror väl på, lite.
     * 
     * i den:
     * 
     
       if (myPlayer == null (inte viktigt nu->)|| myCogSound == null)
        {
            return;
        }

     * 
     * sen:
     * 
        if (aCollision.collider.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            myPlayer.IncreaseScore();
            (inte viktigt nu->)Instantiate(myCogSound, mySoundContainer.transform);
            Destroy(gameObject);
        }
     * 
     * (LayerMask är en struct som kommer från UnityEngine 
     * och bär medlemsfunktoinen NameToLayer.
     * Det var något som Morgan lade till f.a. 
     * cogs:en till Fenekk hade en attraction, 
     * och är därmed inget som behövs.)
     * 
     * (IncreaseScore(); är en public void-funktion som
     * kan ligga i player-script, och när den kallas, 
     * ökar en private int (typ myScore) med 1 varje gång
     * den öppnas. Det här kanske ska köras med klass, på
     * samma sätt i Kasino-uppgifterna. Kanske. Idk.)
     * 
     * 
     * 
     * Vidare tankar om kod finns i Davids anteckningsblock. 
     * Fråga honom om du läser igenom detta och har tankar.
     * 
     * 
     * 
     */

    static public int myIdGenerator = 0;
    public int myId { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        myId = ++myIdGenerator;
        Debug.Log(myId);
        mySnowflake.SetActive(true);
        myIsPickedUp = false;
        if (mySnowflake == null)
        {
            Debug.LogError("mySnowflake is fuckywucky");
        }
    }

    public override IEnumerator TGAExecute()
    {
        if (myIsPickedUp == false)
        {
            myIsPickedUp = true;
            CollectableManager.ourInstance.OnPickUp(this);
            mySnowflake.SetActive(false);
        }

        yield return null;

    }

}