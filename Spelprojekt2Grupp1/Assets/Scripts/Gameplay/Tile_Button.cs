using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile_Button : Tile
{
    [SerializeField]
    Tile myTargetTile;

    private bool myButtonIsActive = false;

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
        yield return ToggleButton();
    }

    /// <summary>
    /// Toggles animation for tile on animation-event
    /// </summary>
    /// <returns></returns>
    private IEnumerator ToggleButton()
    {
        // Get tile or a function from tile
        if (myButtonIsActive == false)
        {
            myButtonIsActive = true;    // temp. Animation event will change myButtonIsActive in the future
            Debug.Log("Button is true");
            //yield return StartCoroutine(myTargetTile);
            yield return null;
        }
        else if (myButtonIsActive == true)
        {
            myButtonIsActive = false;   // temp. Animation event will change myButtonIsActive in the future
            Debug.Log("Button is false");
            //yield return StartCoroutine(myTargetTile);
            yield return null;
        }
    }
}