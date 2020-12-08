﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager ourInstance;
    public AudioManager myAudioManager { get; private set; }

    private StageManager myStageManager;
    private GameData myGameData;

    [SerializeField] public bool myIsDebugging = false;

    private AudioSource myCurrentMusicSource;

    public void Update()
    {
        if (myIsDebugging)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                TransitionNextStage();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Loading data");
                LoadGameData();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Deleting data");
                DeleteSavedGameData();
                LoadGameData(); // Create new instance
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Saving data");
                SaveGameData();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Inspecting");
                Debug.Log(myGameData.ToString());
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                GameData.StageData testData = GameData.StageData.ourInvalid;
                testData.myNumCollected += 2;
                testData.myIsStageCleared = true;
                testData.myNumAvailable = 5;
                var currentScenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
                myGameData.myStageDataStr[currentScenePath] = testData;
                // TODO: test with snowflaketiles!
                Debug.Log(myGameData.ToString());
            }
        }
    }


    public void TransitionNextStage()
    {
        myStageManager.GoToNextStage();
        // TODO: use coroutine LoadStage - but if above works ok, scratch this for reducing complexity
    }

    public void RestartCurrentStage()
    {
        myStageManager.ReloadCurrentStage();
    }

    /// <summary>
    /// Load data when game is started. Always in memory.
    /// </summary>
    void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/AChristmasCarrotSaveData.dat";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            myGameData = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!");
        }
        else
        {
            myGameData = new GameData();
            //Debug.LogError("There is no save data!");
        }
    }

    void DeleteSavedGameData()
    {
        string filePath = Application.persistentDataPath + "/AChristmasCarrotSaveData.dat";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }        
    }

    /// <summary>
    /// Save data every time a stage is completed.
    /// </summary>
    void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/AChristmasCarrotSaveData.dat";
        FileStream file = File.Create(filePath);
        bf.Serialize(file, myGameData);
        file.Close();
        Debug.Log("Game data saved!");
    }


    public GameData.StageData GetSavedCurrentStageData()
    {
        var currentScenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
        return GetSavedStageData(currentScenePath);
    }

    public GameData.StageData GetSavedStageData(string aScenePath)
    {
        // Attempt to fetch saved data if exists, otherwise get new instance of stage data.
        if (!myGameData.myStageDataStr.TryGetValue(aScenePath, out GameData.StageData data))
        {
            data = GameData.StageData.ourInvalid;
        }
        return data;
    }

    public void UpdateSavedStageData()
    {
        var currentScenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
        UpdateSavedStageData(currentScenePath, CollectableManager.ourInstance.myStageData);
    }
    public void UpdateSavedStageData(string aScenePath, GameData.StageData someNewData)
    {
        GameData.StageData currentData = GetSavedStageData(aScenePath); // Gets current save if exists, otherwise get default instance of StageData
        
        // if entirely new data, save it.
        if (currentData.myIsStageCleared == false)
        {
            someNewData.myIsStageCleared = true;
            myGameData.myStageDataStr[aScenePath] = someNewData;
            SaveGameData();
        }
        else
        {
            bool replaceOldData = false;
            // Merge hashsets
            foreach (var id in someNewData.myCollectables)
            {
                if (!currentData.myCollectables.Contains(id)) // A brand new flake
                {
                    currentData.myCollectables.Add(id);
                    currentData.myNumCollected++;
                    replaceOldData = true;
                }
            }
            if (replaceOldData)
            {
                myGameData.myStageDataStr[aScenePath] = currentData;
                SaveGameData();
            }

        }

    }

    public void TransitionToStage(int aStageIndex)
    {
        // TODO: Start coroutine for LoadStage async
    }

    public void TransitionToMainMenu()
    {
    }

    private IEnumerator LoadStage(int aStageIndex)
    {

        //SceneManager.LoadScene(...);

        // Wait for next frame when the scene is fully loaded and active
        yield return null;

        OnStageBegin(aStageIndex);
    }

    private void OnStageBegin(int aStageIndex)
    {
        Debug.Assert(CollectableManager.ourInstance != null, "No instance of CollectableManager found!");
    }

    public void OnStageCleared()
    {
        UpdateSavedStageData();

        // TODO: show current stage's score  
        GameManager.ourInstance.TransitionNextStage();
    }

    /// <summary>
    /// Returns a list of cleared stages.
    /// </summary>
    /// <returns></returns>
    public List<string> GetClearedStages()
    {
        var stageList = new List<string>();
        foreach (KeyValuePair<string, GameData.StageData> entry  in myGameData.myStageDataStr)
        {
            if (entry.Value.myIsStageCleared)
            {
                stageList.Add(entry.Key);
            }
            
        }
        return stageList;
    }

    /// <summary>
    /// Check if stage is cleared
    /// </summary>
    /// <param name="aScenePath">A relative scene path</param>
    /// <returns></returns>
    public bool IsStageCleared(string aScenePath)
    {
        // Ensures key is set before attempting to access bool in StageData.
        return (myGameData.myStageDataStr.TryGetValue(aScenePath, out GameData.StageData data) && data.myIsStageCleared);
    }

    private void Start()
    {
    }

    private void Awake()
    {
        if (ourInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        ourInstance = this;

        myStageManager = GetComponent<StageManager>();
        myAudioManager = GetComponent<AudioManager>();

        LoadGameData();
    }

    private void PlayMusic(string anAudioName, float someVolume = 0.165f, bool aShouldRestart = false)
    {
        if (myCurrentMusicSource != null)
        {
            if (myCurrentMusicSource.clip == myAudioManager.GetAudioClip(anAudioName) && !aShouldRestart)
            {
                myCurrentMusicSource.volume = someVolume;
                return;
            }

            myAudioManager.Stop(myCurrentMusicSource);
            myCurrentMusicSource = null;
        }

        myCurrentMusicSource = myAudioManager.PlayMusicClip(anAudioName, someVolume: someVolume, aShouldLoop: true);
    }

    private void StopMusic()
    {
        if (myCurrentMusicSource != null)
        {
            myAudioManager.Stop(myCurrentMusicSource);
            myCurrentMusicSource = null;
        }
    }
}
