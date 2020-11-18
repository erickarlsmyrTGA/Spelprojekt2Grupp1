using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    /// <summary>
    /// Moves a GameObject in a direction with given speed
    /// </summary>
    /// <param name="aTransform">The transform component of the GameObject</param>
    /// <param name="aDirection">The direction the GameObject will move to</param>
    /// <param name="aSpeed">The speed of the movement. The execution will happen in (1 / aSpeed) seconds.</param>
    /// <returns>Returns an IEnumerator to be used in StartCoroutine</returns>
    public IEnumerator MoveInDirection(Transform aTransform, Vector3 aDirection, float aSpeed)
    {
        Vector3 direction = aDirection;
        //Checking for Ice
        if(direction.y == 0)
        {
            var tile = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + Vector3.down);
            while (tile && tile.myType.HasFlag(Tile.TileType.Ice))
            {
                tile = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + Vector3.down);
                direction += aDirection;
            }
            direction -= aDirection;
        }

        //Movement
        Vector3 position = aTransform.position;
        Vector3 target = position + direction;
        float divider = Mathf.Abs(Vector3.Distance(position, target));

        float percentage = 0.0f;
        while (percentage < 1.0f)
        {
            aTransform.position = Vector3.Lerp(position, target, percentage);
            percentage += Time.deltaTime * aSpeed / divider;
            yield return null;
        }
        aTransform.position = target;
    }

    public IEnumerator MoveToPosition(Transform aTransform, Vector3 anEndPosition, float aSpeed)
    {
        //Movement
        Vector3 position = aTransform.position;
        Vector3 target = anEndPosition;
        float divider = Mathf.Abs(Vector3.Distance(position, target));

        float percentage = 0.0f;
        while (percentage < 1.0f)
        {
            aTransform.position = Vector3.Lerp(position, target, percentage);
            percentage += Time.deltaTime * aSpeed / divider;
            yield return null;
        }
        aTransform.position = target;
    }
}
//                else if(tile.myType.HasFlag(Tile.TileType.Ice))
//                {
//                    while(tile.myType.HasFlag(Tile.TileType.Ice))
//                    {
//                        yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myMoveSpeed));
//                        tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + direction);
//                    }
//                    CheckFallDistanceAndFall();
//                }