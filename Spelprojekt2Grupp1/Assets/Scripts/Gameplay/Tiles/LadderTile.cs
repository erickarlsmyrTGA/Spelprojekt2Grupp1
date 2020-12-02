using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTile : Tile
{
    [Header("References")]
    [SerializeField]
    Transform myModelTransform;

    [Header("Properties")]
    [SerializeField]
    Vector3 myPivot;
    [SerializeField]
    [Range(0,5)]
    int myDirection;
    [SerializeField]
    bool myShowGizmos;

    Vector3 myLadderPosition;

    public int TGADirection { get { return myDirection; } private set { myDirection = value; } }

    LadderTile()
    {
        myName = "Ladder";
        myType |= TileType.Barrier | TileType.Ground;
    }

    private void OnValidate()
    {
        if (myDirection == 0)
            myDirection = 4;
        if (myDirection == 5)
            myDirection = 1;

        myLadderPosition = transform.position;
        Quaternion rotation = myModelTransform.rotation;
        switch (myDirection)
        {
            case 1:
                myLadderPosition += new Vector3(1, 0, 0) + myPivot;
                rotation.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 2:
                myLadderPosition += new Vector3(0, 0, 1) + myPivot;
                rotation.eulerAngles = new Vector3(0, 90, 0);
                break;
            case 3:
                myLadderPosition += new Vector3(-1, 0, 0) + myPivot;
                rotation.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 4:
                myLadderPosition += new Vector3(0, 0, -1) + myPivot;
                rotation.eulerAngles = new Vector3(0, 270, 0);
                break;
            default:
                break;
        }

        myModelTransform.rotation = rotation;
        myModelTransform.position = myLadderPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.125f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + myPivot, Vector3.one);
        if (myShowGizmos)
        {
            Gizmos.DrawWireCube(myLadderPosition, Vector3.one);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
