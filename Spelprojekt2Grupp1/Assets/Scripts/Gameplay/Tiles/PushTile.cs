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

    PushTile()
    {
        myName = "PushTile";
        myType = TileType.Barrier | TileType.Ground;
    }

    public IEnumerator TGAMoveInDirection(Vector3 aDirection)
    {
        // Check if next tile is not barrier
        {
            var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + aDirection);
            if (tile)
            {
                if (!tile.myType.HasFlag(TileType.Barrier))
                {
                    // Move in direction
                    yield return StartCoroutine(MoveInDirection(aDirection, myMoveSpeed));
                }
            }
            else
            {
                // Move in direction
                yield return StartCoroutine(MoveInDirection(aDirection, myMoveSpeed));
            }
        }

        yield return StartCoroutine(CheckFallDistanceAndFall());
    }

    IEnumerator MoveInDirection(Vector3 aDirection, float aSpeed)
    {
        Vector3 position = transform.position;
        Vector3 target = position + aDirection;
        float divider = Mathf.Abs(Vector3.Distance(position, target));

        float percentage = 0.0f;
        while (percentage < 1.0f)
        {
            transform.position = Vector3.Lerp(position, target, percentage);
            percentage += Time.deltaTime * aSpeed / divider;
            yield return null;
        }
        transform.position = target;
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

        yield return StartCoroutine(MoveInDirection(direction, myGravity));
        TGASetPosition(transform.position);
    }
}
