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
        //if (myIsMoving)
        //{
        //    StartCoroutine(TGAExecute(this));
        //}
    }

    ButtonActivatedMoveTile()
    {
        myName = "Button Activated Platform";
        myType = myType = TileType.Barrier | TileType.Ground;
        myMovement = new Movement();
    }

    /// <summary>
    /// Move the tile between positions on button press
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        yield return StartCoroutine(myMovement.MoveInDirection(transform, ((myIsPressed) ? myEndPos - myStartPos : myStartPos - myEndPos), 2));
        yield return null;
    }

    public void ActivatedByButton()
    {
        Debug.LogError("BUTTON PRESSED");
        myIsPressed = !myIsPressed;
        StartCoroutine(TGAExecute(gameObject));
    }
        
}
