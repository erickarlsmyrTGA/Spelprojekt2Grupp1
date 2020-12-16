using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTile : Tile
{
    [SerializeField] Player myPlayer;
    [SerializeField] private ParticleSystem myOvenPoof;
    OvenTile()
    {
        myName = "Oven";
        myType = TileType.Barrier | TileType.Ground;
    }

    private void Start()
    {
        myOvenPoof.Stop();
    }


    /// <summary>
    /// Change the player state to gas
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        if (myPlayer.myStateIsSolid)
        {
            Debug.Log("Now I'm a cloud");
            yield return StartCoroutine(myPlayer.TGAChangeToGasState());
            //osäker på den här:
            myOvenPoof.Play();
        }   
        yield return null;
    }
}
