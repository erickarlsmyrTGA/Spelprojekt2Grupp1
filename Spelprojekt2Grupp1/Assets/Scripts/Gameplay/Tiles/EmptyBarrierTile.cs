using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBarrierTile : Tile
{
    EmptyBarrierTile()
    {
        myType = TileType.Barrier | TileType.Ground;
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0.5f, -0.5f), Vector3.one);

        base.DrawGizmos();
    }
}
