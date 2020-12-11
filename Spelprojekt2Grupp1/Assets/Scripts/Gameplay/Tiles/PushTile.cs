using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTile : Tile
{
    [SerializeField]
    float myMoveSpeed = 0.0f;
    [SerializeField]
    float myGravity = 0.0f;
    [SerializeField]
    [Min(5)]
    int myMaxFallDistance;

    Movement myMovement;

    PushTile()
    {
        myName = "PushTile";
        myType = TileType.Barrier | TileType.Ground | TileType.Movable;
        myMovement = new Movement();
    }

    public IEnumerator TGAMoveInDirection(Vector3 aDirection)
    {
        yield return StartCoroutine(ExecuteCurrentTile());
        // Check if next tile is not barrier
        {
            var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + aDirection);
            if (tile)
            {
                if (!tile.myType.HasFlag(TileType.Barrier))
                {
                    // Move in direction
                    yield return StartCoroutine(myMovement.MoveInDirection(transform, aDirection, myMoveSpeed));
                }
            }
            else
            {
                // Move in direction
                yield return StartCoroutine(myMovement.MoveInDirection(transform, aDirection, myMoveSpeed));
            }
        }

        yield return StartCoroutine(CheckFallDistanceAndFall());
        yield return StartCoroutine(ExecuteCurrentTile());
    }

    IEnumerator CheckFallDistanceAndFall()
    {
        Debug.Log("PushTile: Checking fall distance...");

        Vector3 direction = Vector3.zero;

        for (int i = 0; i < myMaxFallDistance; i++)
        {
            direction += Vector3.down;
            var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + direction);
            if (tile && (tile.myType.HasFlag(Tile.TileType.Ground) || tile.myType.HasFlag(Tile.TileType.Barrier)))
            {
                i = myMaxFallDistance;
            }
        }
        direction += Vector3.up;

        Debug.Log("PushTile: Fall distance was " + Vector3Int.FloorToInt(direction).ToString());

        yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myGravity));
        TGASetPosition(transform.position);
    }

    IEnumerator ExecuteCurrentTile()
    {
        var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + Vector3.down);
        if (tile && (tile.myType.HasFlag(TileType.Button)))
        {
            yield return StartCoroutine(tile.TGAExecute());
        }
    }
}
