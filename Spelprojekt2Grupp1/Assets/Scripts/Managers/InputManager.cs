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

    [SerializeField] Transform myPlayer;
    [SerializeField] float mySwipeThreashold = 0.75f;
    Touch myTouch;
    Vector2 myStartCoords;
    TouchState myCurrentTouchState;


    bool myFirstFrame = false;
    bool myPresssedForward = false;
    bool myPresssedBackward = false;
    bool myPresssedRight = false;
    bool myPresssedLeft = false;

    public enum TouchState
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
        // Reset pressed bools
        myPresssedForward = false;
        myPresssedBackward = false;
        myPresssedRight = false;
        myPresssedLeft = false;

        
        ListenForTouchPhase();
        //TGAPressed();
        //TGASwipe();

        if (myFirstFrame)
        {
            if (myPresssedForward) Debug.LogWarning("PRESSED FORWARD");
            if (myPresssedBackward) Debug.LogWarning("PRESSED BACKWARD");
            if (myPresssedRight) Debug.LogWarning("PRESSED RIGHT");
            if (myPresssedLeft) Debug.LogWarning("PRESSED LEFT");
        }
    }

    public float TGAGetSwipeThreashold()
    {
        return mySwipeThreashold;
    }


    void ListenForTouchPhase()
    {
        if (TGATouchingScreen())
        {
            myTouch = Input.GetTouch(0);

            if (myTouch.phase == TouchPhase.Began)
            {
                myFirstFrame = true;
                myCurrentTouchState = TouchState.Holding;
                myStartCoords = GetTouchScreenPos(myTouch);
                Debug.Log(myStartCoords.x + ", " + myStartCoords.y);
                CalculatePoint();
            }
            else
            {
                myFirstFrame = false;
                if (myTouch.phase == TouchPhase.Ended)
                {
                    if (myCurrentTouchState == TouchState.Holding)
                    {
                        myCurrentTouchState = TouchState.Released;
                    }
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

    Vector2 ConvertVector2ToScreenSpace(Vector2 aCoord)
    {
        Vector2 temp;
        temp.y = (Screen.height - aCoord.y) / Screen.height;
        temp.x = 1 - (Screen.width - aCoord.x) / Screen.width;

        return temp;
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the application area.
    /// </summary>
    public bool TGAPressed()
    {
        if (TGATouchingScreen())
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
        if (TGATouchingScreen())
        {
            if (myCurrentTouchState == TouchState.Swiping)
            {
                Debug.LogError("MOVED! " + DeltaTouchPos());
                return DeltaTouchPos();
            }
        }

        return new Vector2(0.0f, 0.0f);
    }

    void CalculatePoint()
    {
        // Create offset based of the middle of the screen
        
        Vector2 offset = Camera.main.WorldToScreenPoint(myPlayer.position);

        // Get mouse position relative to offset
        Vector2 relativeMousePosition = myTouch.position - offset;

        Vector2 rotatedMousePosition = relativeMousePosition;

        // Manually checks if at snap point
        float angle = -1.0f;
        float snapPoint = CameraController.ourInstance.ClosestSnapPointVector3().y;

        if (snapPoint % 90 == 0)
        {
            angle = snapPoint;
            Debug.LogWarning("ANGLE: " + angle);
        }

        // Rotate MouseCoords around origo depending on the angle
        switch (angle)
        {
            case 0.0f:
                // Rotate back 0 aka 0
                rotatedMousePosition = relativeMousePosition;
                break;
            case 90.0f:
                // Rotate back 90 aka 270
                rotatedMousePosition = new Vector3(-relativeMousePosition.y, relativeMousePosition.x);
                break;
            case 180.0f:
                // Rotate back 180 aka 180
                rotatedMousePosition = new Vector3(-relativeMousePosition.x, -relativeMousePosition.y);
                break;
            case 270.0f:
                // Rotate back 270 aka 90
                rotatedMousePosition = new Vector3(relativeMousePosition.y, -relativeMousePosition.x);
                break;
        }

        // Move back MouseCoords around origo
        rotatedMousePosition += offset;

        if (rotatedMousePosition.x > offset.x) // Right of Offset
        {
            if (rotatedMousePosition.y > offset.y) // Above Offset
            {
                // R U ==> TopRight
                myPresssedForward = true;
            }
            else // Below Offset
            {
                // R D ==> BottomRight
                myPresssedRight = true;
            }
        }
        else // Left of Offset
        {
            if (rotatedMousePosition.y > offset.y) // Above Offset
            {
                // L U ==> TopLeft
                myPresssedLeft = true;
            }
            else // Below Offset
            {
                // L D ==> BottomLeft
                myPresssedBackward = true;
            }
        }

        if (myStartCoords.y >= mySwipeThreashold)
        {
            myPresssedForward = false;
            myPresssedBackward = false;
            myPresssedRight = false;
            myPresssedLeft = false;
            myCurrentTouchState = TouchState.Swiping;
        }
    }

    public TouchState TGACurrentTouchState()
    {
        return myCurrentTouchState;
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
        return (myPresssedForward);
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Backward" area.
    /// </summary>
    public bool TGAPressedBackward()
    {
        return (myPresssedBackward);
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Right" area.
    /// </summary>
    public bool TGAPressedRight()
    {
        return (myPresssedRight);
    }

    /// <summary>
    /// Returns true during the frame the user pressed in the "Left" area.
    /// </summary>
    public bool TGAPressedLeft()
    {
        return (myPresssedLeft);
    }

    public bool TGATouchingScreen()
    {
        return (Input.touches.Length != 0);
    }
}
