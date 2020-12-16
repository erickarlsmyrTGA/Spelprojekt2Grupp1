using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    [SerializeField]
    private Player myPlayer;

    [SerializeField]
    SceneReference myNextScene = null;

    GoalTile()
    {
        myName = "Goal";
        myType = TileType.Ground | TileType.Barrier;
    }

    //private void OnValidate()
    //{
    //    bool isSolidState = FindObjectOfType<Player>().IsSolidState;
    //    if (isSolidState == true)
    //    {
    //        GoalTile.TGAExecute();
    //    }
    //}


    private void OnValidate()
    {
        myPlayer = FindObjectOfType<Player>();
    }


    /// <summary>
    /// Win stage, trigger GameManager's OnStageCompleted
    /// </summary>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        if (myPlayer.IsSolidState)
        {
            if (myNextScene != null)
            {
                GameManager.ourInstance.TransitionToStage(myNextScene.ScenePath);
            }
            else
            {
                GameManager.ourInstance.OnStageCleared();
            }
        }
        yield return null;
    }
}
