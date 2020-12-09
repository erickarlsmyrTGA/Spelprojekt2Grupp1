using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    GroundTile()
    {
        myName = "Ground";
        myType = TileType.Ground | TileType.Barrier;
    }
}
