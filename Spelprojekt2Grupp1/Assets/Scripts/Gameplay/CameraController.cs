using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Android;

#region Singleton Pattern

public class CameraController : MonoBehaviour
{
    Vector3[] mySnapPoints = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(0, 360, 0), };
    [SerializeField] float mySnapSpeed = 20f;
    [SerializeField] float myRotateSpeed = 100;

    void Update()
    {
        Rotate();
        Snap();
    }

    void Rotate()
    {
        if (TouchingScreen())
        {
            Touch touch = Input.GetTouch(0);

            transform.Rotate(0, touch.deltaPosition.x * myRotateSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    void Snap()
    {
        if (!AtSnapPoint() && !TouchingScreen())
        {
            SnapToPoint();
        }
    }

    bool TouchingScreen()
    {
        return (Input.touches.Length != 0);
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

#endregion