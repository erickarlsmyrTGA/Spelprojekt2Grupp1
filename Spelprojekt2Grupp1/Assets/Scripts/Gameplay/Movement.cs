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
   public IEnumerator MoveInDirection(Transform aTransform, Vector3 aDirection, float aSpeed, bool aIsSolid = true)
   {
      Vector3 direction = aDirection;
      //Handling Ice Tiles
      if (direction.y == 0 && direction != Vector3.zero && aIsSolid == true)
      {
         var tile1 = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + Vector3.down);
         var tile2 = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + aDirection);

         if (tile1 && tile1.myType.HasFlag(Tile.TileType.Ice))
         {
            Debug.Log("isIce = true");
            while (tile1 && tile1.myType.HasFlag(Tile.TileType.Ice) && (tile2 == null || !(tile2.myType.HasFlag(Tile.TileType.Barrier))))
            {
               Debug.Log("isBarrier = FALSE");
               direction += aDirection;
               tile1 = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + Vector3.down);
               tile2 = TileManager.ourInstance.TGATryGetTileAt(aTransform.position + direction + aDirection);
            }
         }
      }

      Debug.Log("isIce = DONE");

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
