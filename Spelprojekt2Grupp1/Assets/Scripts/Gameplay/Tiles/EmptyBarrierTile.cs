using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EmptyBarrierTile : Tile
{
    [SerializeField]
    GameObject myModel;

    EmptyBarrierTile()
    {
        myType = TileType.Barrier | TileType.Ground;
    }

    private void Start()
    {
        myModel.SetActive(false);
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(myModel.transform.position + new Vector3(0.5f, 0.5f, -0.5f), Vector3.one);

        base.DrawGizmos();
    }
}
