using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePauseMenu : MonoBehaviour
{

   [SerializeField] GameObject myPauseMenu;

   void OnApplicationPause(bool aPause)
   {
      if (aPause == true)
      {
         myPauseMenu.SetActive(true);
         Debug.Log("Pausad!");
      }
   }

   private void OnApplicationFocus(bool aFocus)
   {
      if (aFocus == false)
      {
         myPauseMenu.SetActive(true);
         Debug.Log("Ur Fokus!");
      }
   }

   public void ActivateGame()
   {
      myPauseMenu.SetActive(false);
      Debug.Log("INTE pausad!");
   }
}
