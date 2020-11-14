using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all tiles in Unity scenes
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>
    /// Executes a behaviour according its type
    /// </summary>
    /// <param name="aGameActor">The gameobject belonging to the Player</param>
    public IEnumerator TGAExecute(GameObject aGameActor) { yield return null; }
}
