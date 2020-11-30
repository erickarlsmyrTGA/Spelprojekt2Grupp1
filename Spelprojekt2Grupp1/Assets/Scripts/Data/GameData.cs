using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on:
// https://www.red-gate.com/simple-talk/dotnet/c-programming/saving-game-data-with-unity/

[System.Serializable]
public class GameData
{    
    public Dictionary<string, StageData> myStageDataStr; // scenepath as key
    public Dictionary<int, StageData> myStageDataInt; // sceneIndex as key    
    
    public int myTotalCollected { get {
            int tot = 0;
            foreach (var d in myStageDataInt)
            {
                tot += d.Value.myCurrentBest;
            }
            return tot;
        } }
    public int myTotalNumAvailableCol { get {
            int tot = 0;
            foreach (var d in myStageDataInt)
            {
                tot += d.Value.myCurrentBest;
            }
            return tot;
        } } // TODO: replace with known constant eventually

    public struct StageData
    {
        public static StageData ourInvalid => new StageData { myNumAvailable = 0, myCurrentBest= 0, myIsStageCleared = false };

        public HashSet<SnowflakeTile> myCollectables { get; set; }
        public int myNumAvailable{ get; set; } // number of collectibles in stage
        public int myCurrentBest { get; set; }
        public bool myIsStageCleared { get; set; }
    }

}

