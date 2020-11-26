using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatedMoveTile : Tile
{
    [SerializeField] Vector3 myStartPos;
    [SerializeField] Vector3 myEndPos;
    Movement myMovement;

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
        Debug.Log("BAMT STARTED");
    }

    /// <summary>
    /// Move the tile between positions on button press
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public override IEnumerator TGAExecute(System.Object anObject)
    {
        bool isPressed = (bool)anObject;
        yield return StartCoroutine(myMovement.MoveToPosition(transform, ((isPressed) ? myEndPos: myStartPos), 3));
        TGASetPosition(transform.position);
    }

    public void ActivatedByButton()
    {
        Debug.LogError("BUTTON PRESSED");
        StartCoroutine(TGAExecute());
    }
        
}
