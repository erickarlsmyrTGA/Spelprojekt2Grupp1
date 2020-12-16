using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FreezerTile : Tile
{
    [SerializeField] Player myPlayer;
    [SerializeField] private ParticleSystem myFreezerPoof;
    FreezerTile()
    {
        myName = "Freezer";
        myType = TileType.Barrier | TileType.Ground;
    }

    private void Start()
    {
        myFreezerPoof.Stop();
    }

    /// <summary>
    /// Change the player state to solid
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        if (!myPlayer.myStateIsSolid)
        {
            Debug.Log("Now I'm a snowman?");
            //^det stod "Now I'm a cloud tidigare"^
            yield return StartCoroutine(myPlayer.TGAChangeToSolidState());
            //osäker på den här:
            myFreezerPoof.Play();            
        }           
        yield return null;
    }
}
