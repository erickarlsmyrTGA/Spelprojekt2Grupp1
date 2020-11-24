using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTile : Tile
{
    [SerializeField] Player myPlayer;
    
    OvenTile()
    {
        myName = "Oven";
        myType = TileType.Barrier | TileType.Ground;
    }

    /// <summary>
    /// Change the player state to gas
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        Debug.Log("Now I'm a cloud");
        yield return StartCoroutine(myPlayer.TGAChangeToGasState());
        yield return null;
    }
}
