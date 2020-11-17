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
        // yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S));

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector3(2, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector3(-1, 0, 0);
        }

        // Move to target
        {
            Vector3 position = transform.position;
            Vector3 target = position + direction;
            float divider = Mathf.Abs(Vector3.Distance(position, target));

            float percentage = 0.0f;
            while (percentage < 1.0f)
            {
                transform.position = Vector3.Lerp(position, target, percentage);
                percentage += Time.deltaTime / divider;
                yield return null;
            }
            transform.position = target;
        }
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
