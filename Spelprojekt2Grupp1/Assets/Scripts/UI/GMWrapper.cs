using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMWrapper : MonoBehaviour
{
    /// <summary>
    /// For calling Delete and create new save file from UI buttons
    /// </summary>
    public void DeleteSaves()
    {
        GameManager.ourInstance.DeleteSavedGameData();   
        GameManager.ourInstance.LoadGameData();   
    }  
    
}
