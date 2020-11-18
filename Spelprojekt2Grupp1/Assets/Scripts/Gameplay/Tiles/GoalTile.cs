using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    GoalTile()
    {
        myName = "Goal";
    }

    /// <summary>
    /// Win stage, trigger GameManager's OnStageCompleted
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {        
        GameManager.ourInstance.OnStageCompleted();
        yield return null;
    }
}
