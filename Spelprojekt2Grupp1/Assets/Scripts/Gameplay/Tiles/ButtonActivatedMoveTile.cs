using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatedMoveTile : Tile
{
    [SerializeField] Vector3 myStartPos;
    [SerializeField] Vector3 myEndPos;
    Dictionary<Tile, Vector3> myStackedTiles;
    Movement myMovement;

    [SerializeField] bool myIsPressed;
    [SerializeField] bool myIsMoving;

    void Update()
    {
        //if (myIsPressed)
        //{
        //    StartCoroutine(TGAExecute(this));
        //}
    }

    void Start()
    {
        transform.position = myStartPos;
        if (TileManager.ourInstance.TGATryGetTileAt(myStartPos) == null)
        {
            TGASetPosition(myStartPos);
        }
        myStackedTiles = new Dictionary<Tile, Vector3>();
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
        Tile tile;
        myIsPressed = (bool)anObject;
        myIsMoving = (((myIsPressed && transform.position == myStartPos) || (!myIsPressed && transform.position == myEndPos)) ? true : false);

        myStackedTiles.Clear();

        if (myIsMoving)
        {
            for (int i = 1; i < 10; i++)
            {
                tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + Vector3.up * i);

                if (tile != null && tile.myType.HasFlag(TileType.Movable))
                {
                    myStackedTiles.Add(tile, tile.transform.position);
                    tile.transform.parent = gameObject.transform;
                    TileManager.ourInstance.RemoveTileAt(tile.transform.position);
                    //Debug.LogError("made child! " + myStackedTiles.Count);
                }
                else
                {
                    break;
                }
            }
        }

        yield return StartCoroutine(myMovement.MoveToPosition(transform, ((myIsPressed) ? myEndPos : myStartPos), 3));
        TGASetPosition(transform.position);

        myIsMoving = false;
        foreach (var stackedTile in myStackedTiles)
        {
            //Debug.LogError("lost child!");
            stackedTile.Key.transform.parent = gameObject.transform.parent;
            Vector3 newPos = stackedTile.Key.transform.position;
            stackedTile.Key.TGASetPosition(stackedTile.Key.transform.position);
            Debug.Log(stackedTile.Key.transform.position);
        }
    }

    public void ActivatedByButton()
    {
        Debug.LogError("BUTTON PRESSED");
        StartCoroutine(TGAExecute());
    }
}
