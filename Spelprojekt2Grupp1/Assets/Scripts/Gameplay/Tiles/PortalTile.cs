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
        // TODO: Use GameManager instead
        //UnityEngine.SceneManagement.SceneManager.LoadScene(myScene);
        GameManager.ourInstance.TransitionToStage(myScene.ScenePath);
        // Must check that it doesn't collide with the coroutine of stage transition
        yield return null;
    }
}
