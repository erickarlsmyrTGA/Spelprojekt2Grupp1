using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    GroundTile()
    {
        myName = "Ground";
        myType = TileType.Ground | TileType.Barrier;
    }

    public override IEnumerator TGAExecute()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Thunk_05");
        return base.TGAExecute();
    }
}
