using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all tiles in Unity scenes
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>
    /// The name of the Tile. Base class has "Empty" as name.
    /// </summary>
    public string myName { get; protected set; }
    protected Tile()
    {
        myName = "Empty";
    }

    /// <summary>
    /// Executes a behaviour according its type
    /// </summary>
    /// <param name="aGameActor">The gameobject belonging to the Player</param>
    public virtual IEnumerator TGAExecute(GameObject aGameActor) { yield return null; }
}
