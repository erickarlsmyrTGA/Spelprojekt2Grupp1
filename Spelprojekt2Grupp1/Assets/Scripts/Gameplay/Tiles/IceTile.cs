using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : Tile
{
    Movement myMovement; 
    IceTile()
    {
        myName = "IceTile";
        myType = TileType.Ice | TileType.Barrier;
    }
}
                            