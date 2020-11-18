using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTile : Tile
{
    [SerializeField]
    private Vector3 myDestinationPoint;
    
    WindTile() 
    {
        myName = "Wind";
    }
    public override IEnumerator TGAExecute(GameObject aGameActor)
    {
        Debug.Log("Wind Tile activated");
        yield return null; 
    }

    private void Start()
    {
        StartCoroutine(TGAExecute(gameObject));
    }
}
