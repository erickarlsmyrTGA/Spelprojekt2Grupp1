using System;
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

    Touch myTouch;
    Vector2 myStartCoords;
    TouchState myCurrentTouchState;

    enum TouchState
    {
        Holding,
        Swiping, 
        Released
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ListenForTouchPhase();
        TGAPressed();
        TGASwipe();
    }

    void ListenForTouchPhase()
    {
        if (TouchingScreen())
        {
            myTouch = Input.GetTouch(0);

            if (myTouch.phase == TouchPhase.Began)
            {
                myCurrentTouchState = TouchState.Holding;
                myStartCoords = GetTouchScreenPos(myTouch);

                Debug.Log(myStartCoords.x + ", " + myStartCoords.y);
            }
            else if (myTouch.phase == TouchPhase.Ended)
            {
                if (myCurrentTouchState == TouchState.Holding)
                {
                    myCurrentTouchState = TouchState.Released;
                }
            }
        }
    }

    Vector2 GetTouchScreenPos(Touch aTouch)
    {
        Vector2 touchPos;
        touchPos = aTouch.position;
        touchPos.y = (Screen.height - touchPos.y) / Screen.height;
        touchPos.x = 1 - (Screen.width - touchPos.x) / Screen.width;

        return touchPos;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the application area.
    /// </summary>
    public bool TGAPressed()
    {
        if (TouchingScreen())
        {
            if (Vector2.Distance(GetTouchScreenPos(myTouch), myStartCoords) <= 0.03 && myCurrentTouchState == TouchState.Released)
            {
                Debug.Log("TAPPED");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns the amount the user swiped the screen between this frame and last frame.
    /// </summary>
    public Vector2 TGASwipe()
    {
        if (TouchingScreen())
        {
            if (Vector2.Distance(GetTouchScreenPos(myTouch), myStartCoords) > 0.03)
            {
                myCurrentTouchState = TouchState.Swiping;
                Debug.LogError("MOVED! " + DeltaTouchPos());
                return DeltaTouchPos();
            }
        }

        return new Vector2(0.0f, 0.0f);
    }

    Vector2 DeltaTouchPos()
    {
        return myTouch.deltaPosition;
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

    bool TouchingScreen()
    {
        return (Input.touches.Length != 0);
    }
}
