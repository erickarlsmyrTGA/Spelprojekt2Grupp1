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
        // Fetch all tiles in current Scene
        myTiles = new Dictionary<Vector3Int, Tile>();
        foreach (var tile in FindObjectsOfType<Tile>())
        {
            myTiles.Add(Vector3Int.FloorToInt(tile.transform.position), tile.GetComponent<Tile>());
            if (myDebugLevel >= 2)
            {
                Debug.Log("TileManager: Added Tile at " + Vector3Int.FloorToInt(tile.transform.position).ToString());
            }
            
        }

        if (myDebugLevel >= 1)
        {
            Debug.Log("TileManager: " + myTiles.Count.ToString() + " tiles added.");
        }
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
}
