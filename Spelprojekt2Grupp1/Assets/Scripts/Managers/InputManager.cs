using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton Pattern
    public static InputManager ourInstance { get; private set; }
    private void Awake()
    {
        if (ourInstance != null && ourInstance != this)
        {
            Destroy(this);
        }
        else
        {
            ourInstance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the application area.
    /// </summary>
    public bool TGAPressed()
    {
        return false;
    }

    /// <summary>
    /// Returns the amount the user swiped the screen between this frame and last frame.
    /// </summary>
    public Vector3 TGASwipe()
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Forward" area.
    /// </summary>
    public bool TGAPressedForward()
    {
        return false;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Backward" area.
    /// </summary>
    public bool TGAPressedBackward()
    {
        return false;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Right" area.
    /// </summary>
    public bool TGAPressedRight()
    {
        return false;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Left" area.
    /// </summary>
    public bool TGAPressedLeft()
    {
        return false;
    }
}
