using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool myCoroutineIsNotInAction;
    bool myStateIsSolid = true;

    [SerializeField] GameObject mySolidState;
    [SerializeField] GameObject myGasState;

    private void Start()
    {
        myCoroutineIsNotInAction = true;
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

        yield return StartCoroutine(HandleMouseInput());
        yield return StartCoroutine(ExecuteCurrentTile());
        yield return StartCoroutine(CheckFallDistanceAndFall());

        // Mark Coroutine has ended
        myCoroutineIsNotInAction = true;
    }

    IEnumerator HandleMouseInput()
    {
        // [2020-11-14 17:37][Jesper]
        // Example code 
        // Wait for Mouse Input
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // TODO: Implement this
    }

    IEnumerator ExecuteCurrentTile()
    {
        var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position);
        if (tile)
        {
            yield return StartCoroutine(tile.TGAExecute(gameObject));
        }
    }

    IEnumerator CheckFallDistanceAndFall()
    {
        // TODO: Implement this
        yield return null;
    }

    /// <summary>
    /// Part of the player coroutine. Changes the player-state to gas.
    /// </summary>
    /// <returns></returns>
    public IEnumerator TGAChangeToGasState()
    {
        if (myStateIsSolid == true)
        {
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
        if(myStateIsSolid != true)
        {
            myGasState.SetActive(false);
            mySolidState.SetActive(true);

            myStateIsSolid = true;
        }
        yield return null;
    }
}
