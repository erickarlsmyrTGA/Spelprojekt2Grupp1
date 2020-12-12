using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    GoalTile()
    {
        myName = "Goal";
        myType = TileType.Ground | TileType.Barrier;
    }

    /// <summary>
    /// Win stage, trigger GameManager's OnStageCompleted
    /// </summary>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        //GameManager.ourInstance.myAudioManager.PlaySFXClip("Christmas_Sound", 1.0f, 0.7f);
        GameManager.ourInstance.OnStageCleared();
        yield return null;
    }
}
