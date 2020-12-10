using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tile
{
    bool myCoroutineIsNotInAction;
    public bool myStateIsSolid = true;
    public bool myGameIsOn = true;

    bool myCheckFallDistanceThisFrame = true;

    public Movement TGAMovement { get { return myMovement; } }
    public bool IsSolidState { get { return myStateIsSolid; } }

    Player()
    {
        myType = TileType.Barrier | TileType.Ground;
    }

    [SerializeField] GameObject mySolidState;
    [SerializeField] GameObject myGasState;

    [SerializeField]
    float myMoveSpeed = 0.0f;
    [SerializeField]
    float myGravity = 0.0f;
    [SerializeField]
    [Min(5)]
    int myMaxFallDistance;

    [SerializeField]
    Animator myAnimationController;
    [SerializeField]
    Animator myGasAnimationController;


    Movement myMovement;

    private void Start()
    {
        myCoroutineIsNotInAction = true;
        myMovement = new Movement();

        //myAnimationController.SetTrigger("JumpP");
    }

    // Update is called once per frame
    void Update()
    {
        // Always tries to run GameLogic
        if (myCoroutineIsNotInAction)
        {
            StartCoroutine(GameLogicCoroutine());
        }
    }

    IEnumerator GameLogicCoroutine()
    {
        // Mark Coroutine has started
        myCoroutineIsNotInAction = false;

        if (myGameIsOn == true)
        {
            Vector3 lastPos = transform.position;

            yield return StartCoroutine(HandleMouseInput());

            if (myCheckFallDistanceThisFrame && myStateIsSolid)
            {
                yield return StartCoroutine(CheckFallDistanceAndFall());
            }
            myCheckFallDistanceThisFrame = true;

            if (lastPos != transform.position)
            {
                yield return StartCoroutine(ExecuteCurrentTile());
            }
        }


        // Mark Coroutine has ended
        myCoroutineIsNotInAction = true;
    }

    IEnumerator HandleMouseInput()
    {
        // [2020-11-14 17:37][Jesper]
        // Example code 
        // Wait for Mouse Input
        // yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // TODO: Change to WaitIntil() InputManager Touch
        yield return new WaitUntil(() =>
               Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D)
            || InputManager.ourInstance.TGAPressedForward()
            || InputManager.ourInstance.TGAPressedBackward()
            || InputManager.ourInstance.TGAPressedLeft()
            || InputManager.ourInstance.TGAPressedRight()
        );

        if (myGameIsOn == true)
        {
            Vector3 direction = Vector3.zero;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, 0);

            if (Input.GetKeyDown(KeyCode.W) || InputManager.ourInstance.TGAPressedForward())
            {
                direction += new Vector3(1, 0, 0);
                rotation.eulerAngles = new Vector3(0, 90, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) || InputManager.ourInstance.TGAPressedBackward())
            {
                direction += new Vector3(-1, 0, 0);
                rotation.eulerAngles = new Vector3(0, 270, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) || InputManager.ourInstance.TGAPressedLeft())
            {
                direction += new Vector3(0, 0, 1);
                rotation.eulerAngles = new Vector3(0, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) || InputManager.ourInstance.TGAPressedRight())
            {
                direction += new Vector3(0, 0, -1);
                rotation.eulerAngles = new Vector3(0, 180, 0);
            }
            mySolidState.transform.rotation = rotation;
            myGasState.transform.rotation = rotation;

            // Check if next tile is barrier
            {
                var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + direction);
                if (tile)
                {
                    if (tile.myType.HasFlag(Tile.TileType.Barrier))
                    {
                        if (tile.myName == "PushTile" && myStateIsSolid)
                        {
                            myAnimationController.SetTrigger("PushP");
                            yield return new WaitForSeconds(0.5f);
                            yield return StartCoroutine(((PushTile)tile).TGAMoveInDirection(direction));
                            myCheckFallDistanceThisFrame = false;

                            GameManager.ourInstance.myAudioManager.PlaySFXClip("Thunk_03");
                        }
                        else if (tile.myName == "Ladder" && myStateIsSolid)
                        {
                            Debug.LogWarning("DIRECTION = " + (tile.transform.position - transform.position).ToString());

                            var directionToLadder = (tile.transform.position - transform.position);
                            if (directionToLadder == new Vector3(-1f, 0f, 0f) && ((LadderTile)tile).TGADirection == 1)
                            {
                                yield return StartCoroutine(myMovement.MoveInDirection(transform, Vector2.up, myMoveSpeed));
                            }
                            if (directionToLadder == new Vector3(0f, 0f, -1f) && ((LadderTile)tile).TGADirection == 2)
                            {
                                yield return StartCoroutine(myMovement.MoveInDirection(transform, Vector2.up, myMoveSpeed));
                            }
                            if (directionToLadder == new Vector3(1f, 0f, 0f) && ((LadderTile)tile).TGADirection == 3)
                            {
                                yield return StartCoroutine(myMovement.MoveInDirection(transform, Vector2.up, myMoveSpeed));
                            }
                            if (directionToLadder == new Vector3(0f, 0f, 1f) && ((LadderTile)tile).TGADirection == 4)
                            {
                                yield return StartCoroutine(myMovement.MoveInDirection(transform, Vector2.up, myMoveSpeed));
                            }

                            myCheckFallDistanceThisFrame = false;
                        }
                    }
                    else
                    {
                        // Move to target
                        yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myMoveSpeed));
                    }
                }
                else
                {
                    // Move to target
                    Vector3 lastPos = transform.position;

                    myAnimationController.SetTrigger("JumpP");
                    myGasAnimationController.SetTrigger("FloatP");
                    yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myMoveSpeed));

                    var lastTile = TileManager.ourInstance.TGATryGetTileAt(lastPos + Vector3.down);

                    if (lastTile)
                    {
                        yield return StartCoroutine(lastTile.TGAExecute());
                    }
                }
                TGASetPosition(transform.position);
            }
        }
    }

    IEnumerator MoveInDirection(Vector3 aDirection, float aSpeed)
    {
        Vector3 position = transform.position;
        Vector3 target = position + aDirection;
        float divider = Mathf.Abs(Vector3.Distance(position, target));

        float percentage = 0.0f;
        while (percentage < 1.0f)
        {
            transform.position = Vector3.Lerp(position, target, percentage);
            percentage += Time.deltaTime * aSpeed / divider;
            yield return null;
        }
        transform.position = target;
    }

    IEnumerator ExecuteCurrentTile()
    {
        var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + Vector3.down);

        if (tile)
        {
            yield return StartCoroutine(tile.TGAExecute());
        }
    }

    IEnumerator CheckFallDistanceAndFall()
    {
        // TODO: Implement this

        Debug.Log("Player: Checking fall distance...");

        Vector3 direction = Vector3.zero;

        for (int i = 0; i < myMaxFallDistance; i++)
        {
            direction += Vector3.down;
            var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + direction);
            if (tile && (tile.myType.HasFlag(Tile.TileType.Ground) || tile.myType.HasFlag(Tile.TileType.Barrier)))
            {
                i = myMaxFallDistance;
            }
        }
        direction += Vector3.up;

        Debug.Log("Player: Fall distance was " + Vector3Int.FloorToInt(direction).ToString());

        yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myGravity));
        TGASetPosition(transform.position);
    }

    /// <summary>
    /// Part of the player coroutine. Changes the player-state to gas.
    /// </summary>
    /// <returns></returns>
    public IEnumerator TGAChangeToGasState()
    {
        if (myStateIsSolid == true)
        {
            Debug.Log("now I'm a cloud");

            GameManager.ourInstance.myAudioManager.PlaySFXClip("MrCloud_Transform");

            mySolidState.SetActive(false);
            myGasState.SetActive(true);

            myStateIsSolid = false;
        }
        yield return null;
    }

    /// <summary>
    /// Part of the player coroutine. Changes the player-state to solid.
    /// </summary>
    /// <returns></returns>
    public IEnumerator TGAChangeToSolidState()
    {
        if (myStateIsSolid != true)
        {
            Debug.Log("now I'm a snowman");

            GameManager.ourInstance.myAudioManager.PlaySFXClip("Frosty_Transform");

            myGasState.SetActive(false);
            mySolidState.SetActive(true);

            myStateIsSolid = true;
        }
        yield return null;
    }
}
