using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : Tile
{
    IceTile()
    {
        myName = "IceTile";
        myType = TileType.Ice;
    }

    /// <summary>
    /// The player glides in the same direction until it reaches a barrier
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        //yield return StartCoroutine(aGameActor.GetComponent<Player>().MoveInDirection(direction, myMoveSpeed));
        yield return null;

    }

}
