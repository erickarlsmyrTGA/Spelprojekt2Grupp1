using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePauseMenu : MonoBehaviour
{
   [SerializeField] Player myPlayer;
   [SerializeField] GameObject myPauseMenu;

   void OnApplicationPause(bool aPause)
   {
      if (aPause == true)
      {
         myPlayer.myGameIsOn = false;
         myPauseMenu.SetActive(true);
         Debug.Log("Pausad!");
      }
   }

   private void OnApplicationFocus(bool aFocus)
   {
      if (aFocus == false)
      {
         myPlayer.myGameIsOn = false;
         myPauseMenu.SetActive(true);
         Debug.Log("Ur Fokus!");
      }
   }

   public void ActivateGame()
   {
      myPlayer.myGameIsOn = true;
      myPauseMenu.SetActive(false);
      Debug.Log("INTE pausad!");
   }
}
