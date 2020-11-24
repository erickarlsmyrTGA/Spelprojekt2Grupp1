using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerTile : Tile
{
    [SerializeField] Player myPlayer;

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
    public override IEnumerator TGAExecute()
    {
        Debug.Log("Now I'm a cloud");
        yield return StartCoroutine(myPlayer.TGAChangeToSolidState());
        yield return null;
    }
}
