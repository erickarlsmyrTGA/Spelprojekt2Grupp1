using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTile : Tile
{
    OvenTile()
    {
        myName = "Oven";
    }

    /// <summary>
    /// Change the player state to gas
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        //aGameActor. ändra state
        yield return null;
    }
}
