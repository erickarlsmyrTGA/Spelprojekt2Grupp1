using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile_Button : Tile
{
    [SerializeField]
    Tile myTargetTile;

    private bool myRePressableButton = false;

    Tile_Button()
    {
        myName = "Button";
    }

    /// <summary>
    /// Calls a function from myTargetTile
    /// </summary>
    /// <param name="aGameActor"></param>
    /// <returns></returns>
    public virtual IEnumerator TGAExecute(GameObject aGameActor)
    {
        // Get tile or a function from tile
        Debug.Log("Button is pressed");
        yield return null;
    }
}
