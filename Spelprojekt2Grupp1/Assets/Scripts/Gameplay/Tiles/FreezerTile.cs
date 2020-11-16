using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerTile : Tile
{
    FreezerTile()
    {
        myName = "Freezer";
    }
    /// <summary>
    /// Change the player state to solid
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        //aGameActor. ändra state
        yield return null;
    }
}
