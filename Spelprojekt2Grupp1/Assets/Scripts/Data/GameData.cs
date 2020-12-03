using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on:
// https://www.red-gate.com/simple-talk/dotnet/c-programming/saving-game-data-with-unity/

[System.Serializable]
public class GameData
{     
    public Dictionary<string, StageData> myStageDataStr = new Dictionary<string, StageData>(); // scenepath as key
    //public Dictionary<int, StageData> myStageDataInt; // sceneIndex as key  TODO: remove the unused variant after testing.
    
    public int myTotalCollected { get {
            int tot = 0;
            foreach (var d in myStageDataStr)
            {
                tot += d.Value.myNumCollected;
            }
            return tot;
        } }
    public int myTotalNumAvailableCol { get {
            int tot = 0;
            foreach (var d in myStageDataStr)
            {
                tot += d.Value.myNumCollected;
            }
            return tot;
        } } // TODO: replace with known constant eventually

    [System.Serializable]
    public struct StageData
    {
        public static StageData ourInvalid => new StageData { myNumAvailable = 0, myNumCollected= 0, myIsStageCleared = false, myCollectables = new List<int>()};

        public List<int> myCollectables { get; set; }
        public int myNumAvailable{ get; set; } // number of collectibles in stage
        public int myNumCollected { get; set; }
        public bool myIsStageCleared { get; set; }
    }

}

