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
        myType = myType = TileType.Barrier | TileType.Ground;
        myMovement = new Movement();
        myIsPressed = false;
        Debug.Log("BAMT STARTED");
    }

    /// <summary>
    /// Move the tile between positions on button press
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute()
    {
        yield return StartCoroutine(myMovement.MoveToPosition(transform, ((myIsPressed) ? myEndPos: myStartPos), 2));
        yield return null;
    }

    public void ActivatedByButton()
    {
        Debug.LogError("BUTTON PRESSED");
        myIsPressed = !myIsPressed;
        StartCoroutine(TGAExecute());
    }
        
}
