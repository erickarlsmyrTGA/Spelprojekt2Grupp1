using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowflakeCollectionScript : MonoBehaviour
{
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
     */





    // Start is called before the first frame update
    void Start()
    {
        //den här är inte viktig än, men kommer behövas senare
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
