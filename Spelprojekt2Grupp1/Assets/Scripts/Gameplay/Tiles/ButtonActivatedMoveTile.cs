using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatedMoveTile : Tile
{
    [SerializeField] Vector3 myStartPos;
    [SerializeField] Vector3 myEndPos;
    Movement myMovement;
    bool myIsPressed;

    void Update()
    {
        //if (myIsPressed)
        //{
        //    StartCoroutine(TGAExecute(this));
        //}
    }

    ButtonActivatedMoveTile()
    {
        myName = "Button Activated Platform";
        myType = TileType.Barrier | TileType.Ground;
        myMovement = new Movement();
        myIsPressed = false;
        Debug.Log("BAMT STARTED");
    }

    /// <summary>
    /// Move the tile between positions on button press
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameObject)
    {
        if (TileManager.ourInstance.TGATryGetTileAt(transform.position + Vector3.up))
        {
            myIsPressed = true;
        }
        else
        {
            myIsPressed = false;
        }

        yield return StartCoroutine(myMovement.MoveToPosition(transform, ((myIsPressed) ? myEndPos: myStartPos), 3));
        TGASetPosition(transform.position);
    }

    public void ActivatedByButton()
    {
        Debug.LogError("BUTTON PRESSED");
        StartCoroutine(TGAExecute());
    }
        
}
