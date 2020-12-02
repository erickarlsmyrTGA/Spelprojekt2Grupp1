using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : Tile
{
    [SerializeField] Tile myTargetTile;

    Movement myMovement;
    bool myIsPressed = false;
    bool myCoroutineIsNotInAction;

    private void Start()
    {
        myCoroutineIsNotInAction = true;
    }

    private void Update()
    {
        //if (myCoroutineIsNotInAction)
        //{
        //    myCoroutineIsNotInAction = false;
        //    StartCoroutine(TGAExecute());
        //}
    }

    ButtonTile()
    {
        myName = "ButtonTile";
        myType = TileType.Barrier | TileType.Ground | TileType.Button;
        myIsPressed = false;
        myMovement = new Movement();
    }

    public override IEnumerator TGAExecute()
    {
        myIsPressed = !myIsPressed;
        //Debug.LogError("executed");
        //myIsPressed = (TileManager.ourInstance.TGATryGetTileAt(transform.position + Vector3.up) != null);
        yield return StartCoroutine(myTargetTile.TGAExecute(myIsPressed));
        yield return null;
        myCoroutineIsNotInAction = true;
    }
}
