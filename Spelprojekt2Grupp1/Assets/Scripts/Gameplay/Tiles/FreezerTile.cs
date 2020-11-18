using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerTile : Tile
{
    FreezerTile()
    {
        myName = "Freezer";
        myType = TileType.Barrier | TileType.Ground;
    }
    /// <summary>
    /// Change the player state to solid
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        yield return StartCoroutine(aGameActor.GetComponent<Player>().TGAChangeToSolidState());
        yield return null;
    }
}
