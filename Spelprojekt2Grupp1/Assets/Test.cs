using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tile tile = TileManager.ourInstance.TGATryGetTileAt(new Vector3(0, 0, 0));
        if (tile)
        {
            StartCoroutine(tile.TGAExecute(gameObject));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
