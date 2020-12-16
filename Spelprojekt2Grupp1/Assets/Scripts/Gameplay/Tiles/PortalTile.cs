using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTile : Tile
{
    [SerializeField]
    SceneReference myScene;

    PortalTile()
    {
        myType = TileType.Barrier | TileType.Ground;
    }

    public override IEnumerator TGAExecute()
    {
        GameManager.ourInstance.TransitionToStage(myScene.ScenePath);
        yield return null;
    }
}
