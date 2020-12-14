using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile
{
    [SerializeField]
    private Player myPlayer;

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
        //GameManager.ourInstance.myAudioManager.PlaySFXClip("Christmas_Sound", 1.0f, 0.7f);
        GameManager.ourInstance.OnStageCleared();
        yield return null;
        }
    }
}
