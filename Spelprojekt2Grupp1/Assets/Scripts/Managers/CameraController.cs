using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Android;

public class CameraController : MonoBehaviour
{
    #region Singleton Pattern
    public static CameraController ourInstance { get; private set; }
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

    Vector3[] mySnapPoints = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 360, 0) };
    [SerializeField] float mySnapSpeed = 20f;
    [SerializeField] float myRotateSpeed = 100;

    void Update()
    {
        if (InputManager.ourInstance.TGATouchingScreen())
        {
            Rotate();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, 90, 0, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, -90, 0, Space.World);
        }

        Snap();
    }

    void Rotate()
    {
        InputManager.ourInstance.TGARotate();
        if (InputManager.ourInstance.TGACurrentTouchState() == InputManager.TouchState.Rotating)
        {
            transform.Rotate(0, InputManager.ourInstance.TGARotate().x * myRotateSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    void Snap()
    {
        if (!AtSnapPoint() && (InputManager.ourInstance.TGACurrentTouchState() != InputManager.TouchState.Rotating || !InputManager.ourInstance.TGATouchingScreen()))
        {
            SnapToPoint();
        }
    }

    void SnapToPoint()
    {
        int index = ClosestSnapPoint();

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(mySnapPoints[index]);
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, Time.deltaTime * mySnapSpeed);
    }

    public bool AtSnapPoint()
    {
        foreach (Vector3 point in mySnapPoints)
        {
            if (transform.eulerAngles.y == point.y)
            {
                return true;
            }
        }

        return false;
    }

    int ClosestSnapPoint()
    {
        float currentClosestDistance = 361;
        int currentClosestIndex = 0;

        for (int i = 0; i < mySnapPoints.Length; i++)
        {
            float currentDistance = Mathf.Abs(mySnapPoints[i].y - transform.eulerAngles.y);

            if (currentDistance < currentClosestDistance)
            {
                currentClosestDistance = currentDistance;
                currentClosestIndex = i;
            }
        }

        return currentClosestIndex;
    }

    public Vector3 ClosestSnapPointVector3()
    {
        int index = ClosestSnapPoint();

        return mySnapPoints[index];
    }
}