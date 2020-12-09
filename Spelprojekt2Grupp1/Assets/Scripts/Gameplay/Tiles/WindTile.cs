using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTile : Tile
{
    [SerializeField]
    private Vector3 myFanStrength;

    [SerializeField]
    private Player myPlayer;

    [SerializeField]
    private float myFanSpeed;

    /*[SerializeField]
    private Transform myPlayerTransform;*/

    WindTile()
    {
        myName = "Wind";
        myType = TileType.Ground | TileType.Barrier;
            
    }
    public override IEnumerator TGAExecute()
    {
        Debug.Log("Wind Tile activated");
        if (myPlayer.IsSolidState == false)
        {
            GameManager.ourInstance.myAudioManager.PlaySFXClip("PaperSlide");
            yield return StartCoroutine(myPlayer.TGAMovement.MoveInDirection(myPlayer.transform, myFanStrength, myFanSpeed));
        }
    }

    private void Start()
    {
        StartCoroutine(TGAExecute(gameObject));
    }

    /*Movement myMovement;
    WindTile()
    {
        myName = "Wind";
        myType = TileType.Air;
    }*/
}

