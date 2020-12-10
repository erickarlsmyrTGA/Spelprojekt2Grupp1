using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFanTile : Tile
{
    //fläktgrejer:
    [SerializeField]private Vector3 myFanStrength;
    [SerializeField]private float myFanSpeed;

    //flyttar-på-sig-grejer
    [SerializeField]bool myIsPressed;
    [SerializeField]bool myIsMoving;
    [SerializeField]Vector3 myStartPos;
    [SerializeField]Vector3 myEndPos;
    
    [SerializeField]private Player myPlayer;

    Dictionary<Tile, Vector3> myStackedTiles;
    Movement myMovement;
        
    MovingFanTile()
    {
        myName = "MovingFan";
        myType = TileType.Barrier | TileType.Ground;
        myMovement = new Movement();
        Debug.Log("The Moving Fan Should Be Moving Now.");
    }

    private void Start()
    {
        StartCoroutine(TGAExecute(gameObject));
        transform.position = myStartPos;
        if (TileManager.ourInstance.TGATryGetTileAt(myStartPos) == null)
        {
            TGASetPosition(myStartPos);
        }
        myStackedTiles = new Dictionary<Tile, Vector3>();
    }
    public override IEnumerator TGAExecute()
    {
        if (myPlayer.IsSolidState == false)
        {
            GameManager.ourInstance.myAudioManager.PlaySFXClip("PaperSlide");
            yield return StartCoroutine(myPlayer.TGAMovement.MoveInDirection(myPlayer.transform, myFanStrength, myFanSpeed));
        }
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
