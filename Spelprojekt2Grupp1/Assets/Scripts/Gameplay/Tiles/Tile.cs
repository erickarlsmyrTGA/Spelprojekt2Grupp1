using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all tiles in Unity scenes
/// </summary>
public class Tile : MonoBehaviour
{
    [System.Flags]
    public enum TileType
    {
        /// <summary>
        /// Movables can freely move within these tiles.
        /// </summary>
        Air = 1 << 0,
        /// <summary>
        /// Movables can not move through these tiles.
        /// </summary>
        Barrier = 1 << 1,
        /// <summary>
        /// Movables can move on top of these tiles.
        /// </summary>
        Ground = 1 << 2,
        /// <summary>
        /// Movables can move and will glide on top of these tiles.
        /// </summary>
        Ice = 1 << 3,
        /// <summary>
        /// Movables can trigger these by standing on them.
        /// </summary>
        Button = 1 << 4,
        /// <summary>
        /// Is movable.
        /// </summary>
        Movable = 1 << 5
    }

    /// <summary>
    /// The name of the Tile. Base class has "Empty" as name.
    /// </summary>
    public string myName { get; protected set; }

    /// <summary>
    /// The type of the Tile. Base class har "Air" as type.
    /// </summary>
    public TileType myType { get; protected set; }

    protected Tile()
    {
        myName = "Empty";
        myType |= TileType.Air;
    }

    /// <summary>
    /// Set by TGASetPosition. Do not edit this directly.
    /// </summary>
    private Vector3Int myKey;
    /// <summary>
    /// Sets the position of the tile in the TileManager.
    /// Not to be confused with transform.position
    /// </summary>
    /// <param name="aPosition">The new position of the Tile</param>
    public void TGASetPosition(Vector3 aPosition)
    {
        Vector3Int intPosition = Vector3Int.FloorToInt(aPosition);
        if (myKey != intPosition)
        {
            TileManager.ourInstance.TGAChangeKey(myKey, intPosition, this);
            myKey = intPosition;
        }
    }

    /// <summary>
    /// !! Do not call this outside of TileManager.cs !!
    /// </summary>
    public void TGASetKey(Vector3Int aKey)
    {
        myKey = aKey;
    }

    /// <summary>
    /// Sets the position of the tile in the TileManager.
    /// Not to be confused with transform.position
    /// </summary>
    /// <param name="aPosition">The new position of the Tile</param>
    public void TGASetPosition(Vector3Int aPosition)
    {
        if (myKey != aPosition)
        {
            TileManager.ourInstance.TGAChangeKey(myKey, aPosition, this);
            myKey = aPosition;
        }
    }

    /// <summary>
    /// Executes a behaviour according its type
    /// </summary>
    /// <param name="aGameActor">The gameobject belonging to the Player</param>
    public virtual IEnumerator TGAExecute(GameObject aGameActor) { yield return null; }

    /// <summary>
    /// Executes a behaviour according its type
    /// </summary>
    public virtual IEnumerator TGAExecute(Object anObject) { yield return null; }

    public virtual IEnumerator TGAExecute(System.Object anObject) { yield return null; }

    /// <summary>
    /// Executes a behaviour according its type
    /// </summary>
    public virtual IEnumerator TGAExecute()
    {
        yield return null;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Vector3Int.FloorToInt(transform.position), 0.0625f);
    }
}
