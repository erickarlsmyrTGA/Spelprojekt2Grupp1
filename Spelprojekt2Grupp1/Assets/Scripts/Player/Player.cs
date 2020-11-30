using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tile
{
   bool myCoroutineIsNotInAction;
   bool myStateIsSolid = true;
   public bool myGameIsOn = true;

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

   Movement myMovement;

   private void Start()
   {
      myCoroutineIsNotInAction = true;
      myMovement = new Movement();
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
         yield return StartCoroutine(HandleMouseInput());
         yield return StartCoroutine(CheckFallDistanceAndFall());
         yield return StartCoroutine(ExecuteCurrentTile());
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

      Vector3 direction = Vector3.zero;

      if (Input.GetKeyDown(KeyCode.W) || InputManager.ourInstance.TGAPressedForward())
      {
         direction += new Vector3(1, 0, 0);
      }
      if (Input.GetKeyDown(KeyCode.S) || InputManager.ourInstance.TGAPressedBackward())
      {
         direction += new Vector3(-1, 0, 0);
      }
      if (Input.GetKeyDown(KeyCode.A) || InputManager.ourInstance.TGAPressedLeft())
      {
         direction += new Vector3(0, 0, 1);
      }
      if (Input.GetKeyDown(KeyCode.D) || InputManager.ourInstance.TGAPressedRight())
      {
         direction += new Vector3(0, 0, -1);
      }

      // Check if next tile is barrier
      {
         var tile = TileManager.ourInstance.TGATryGetTileAt(transform.position + direction);
         if (tile)
         {
            if (tile.myType.HasFlag(Tile.TileType.Barrier))
            {
               if (tile.myName == "PushTile" && myStateIsSolid)
               {
                  yield return StartCoroutine(((PushTile)tile).TGAMoveInDirection(direction));
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
            yield return StartCoroutine(myMovement.MoveInDirection(transform, direction, myMoveSpeed));
         }
         TGASetPosition(transform.position);
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

      if (tile && !tile.myType.HasFlag(TileType.Button))
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

         myGasState.SetActive(false);
         mySolidState.SetActive(true);

         myStateIsSolid = true;
      }
      yield return null;
   }
}
