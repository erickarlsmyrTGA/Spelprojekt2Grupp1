using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all tiles in Unity scenes
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>
    /// The name of the Tile. Base class has "Empty" as name.
    /// </summary>
    public string myName { get; protected set; }
    protected Tile()
    {
        myName = "Empty";
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
}
