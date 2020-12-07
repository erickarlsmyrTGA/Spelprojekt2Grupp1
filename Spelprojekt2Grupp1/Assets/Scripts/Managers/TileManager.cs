using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all Tiles in the current Scene
/// </summary>
public class TileManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Level of how much should be debugged.\t0 = Nothing1 = Amount of tiles added2 = All Debug.Logs")]
    private uint myDebugLevel = 0;
    private Dictionary<Vector3Int, Tile> myTiles;

    #region Singleton Pattern
    public static TileManager ourInstance { get; private set; }
    private void Awake()
    {
        if (ourInstance != null && ourInstance != this)
        {
            Destroy(this);
        }
        else
        {
            ourInstance = this;

            // Not Singleton Pattern
            LoadTiles();
        }
    }
    #endregion

    void LoadTiles()
    {
        if (myDebugLevel >= 1)
        {
            Debug.Log("TileManager: Loading Tiles");
        }

        // Fetch all tiles in current Scene
        myTiles = new Dictionary<Vector3Int, Tile>();
        foreach (var tile in FindObjectsOfType<Tile>())
        {
            Vector3Int key = Vector3Int.FloorToInt(tile.transform.position + tile.myPivotOffset);
            if (myTiles.ContainsKey(key))
            {
                if (myDebugLevel >= 2)
                {
                    Debug.LogWarning("TileManager: Found and removed duplicate key " + key.ToString());
                }
                myTiles.Remove(key);
            }

            if (myDebugLevel >= 2)
            {
                Debug.Log("TileManager: Adding tile at " + key.ToString());
            }
            myTiles.Add(key, tile.GetComponent<Tile>());
            tile.TGASetKey(key);
            if (myDebugLevel >= 2)
            {
                Debug.Log("TileManager: Added Tile at " + key.ToString());
            }
        }

        if (myDebugLevel >= 1)
        {
            Debug.Log("TileManager: " + myTiles.Count.ToString() + " tiles added.");
        }
    }

    /// <summary>
    /// !! Do not call this outside of Tile.cs !!
    /// </summary>
    public void TGAChangeKey(Vector3Int anOldKey, Vector3Int aNewKey, Tile aTile)
    {
        if (myDebugLevel >= 2)
        {
            Debug.Log("TileManager: Moved " + anOldKey.ToString() + " to " + aNewKey.ToString());
        }
        myTiles.Add(aNewKey, aTile);
        myTiles.Remove(anOldKey);
    }

    /// <summary>
    /// Tries to return the tile at given position
    /// </summary>
    /// <param name="aPosition">Position of expected tile</param>
    /// <returns>If successful, returns the tile at given position. Otherwise, returns null</returns>
    public Tile TGATryGetTileAt(Vector3 aPosition)
    {
        try
        {
            if (myDebugLevel >= 2)
            {
                Debug.Log("TileManager: Trying to return Tile at " + Vector3Int.FloorToInt(aPosition).ToString());
            }
            return myTiles[Vector3Int.FloorToInt(aPosition)];
        }
        catch (KeyNotFoundException)
        {
            if (myDebugLevel >= 2)
            {
                Debug.LogWarning("TileManager: Return null");
            }
            return null;
        }
    }


    public void RemoveTileAt(Vector3 aPosition)
    {
        Vector3Int key = Vector3Int.FloorToInt(aPosition);
        myTiles.Remove(key);
        if (myDebugLevel >= 2)
        {
            Debug.LogWarning("TileManager: Removed key " + key.ToString());
        }
    }
}
