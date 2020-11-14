using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all Tiles in the current Scene
/// </summary>
public class TileManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Check if Debug.Log should be called.")]
    private bool myDebugMode;
    private Dictionary<Vector3, Tile> myTiles;

    #region Singleton Pattern
    private static TileManager myInstance;
    public static TileManager ourInstance
    {
        get
        {
            return myInstance;
        }
    }
    #endregion

    void Start()
    {
        // Fetch all tiles in current Scene
        myTiles = new Dictionary<Vector3, Tile>();
        foreach (var tile in FindObjectsOfType<Tile>())
        {
            myTiles.Add(tile.transform.position, tile.GetComponent<Tile>());
        }

        if (myDebugMode)
        {
            Debug.Log(myTiles.Count.ToString() + " tiles found.");
        }
    }

    /// <summary>
    /// Returns the tile at given position
    /// </summary>
    /// <param name="aPosition">Position of expected tile</param>
    /// <returns>Tile</returns>
    public Tile GetTileAt(Vector3 aPosition)
    {
        return myTiles[aPosition];
    }
}
