using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : Tile
{
    [SerializeField] Tile myTargetTile;

    Movement myMovement;

    ButtonTile()
    {
        myName = "ButtonTile";
        myType = TileType.Barrier | TileType.Ground;
        myMovement = new Movement();
    }

    public override IEnumerator TGAExecute()
    {
        yield return StartCoroutine(myTargetTile.TGAExecute(gameObject));
        yield return null;
    }
}
