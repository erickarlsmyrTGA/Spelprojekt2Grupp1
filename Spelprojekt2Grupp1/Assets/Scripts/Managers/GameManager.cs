using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region DUCT TAPE HACKS

    public static List<int> ourPickupsPerStage = new List<int>()
            {
                0, 0,       // offset for buildindex
                2, 3, 4, 3, // World 1
                3, 1, 3, 1, // World 2
                3, 1, 2, 3, // World 3
            };

    public static int ourTotalPickups
    {
        get
        {
            int sum = 0;
            foreach (var num in ourPickupsPerStage)
            {
                sum += num;
            }
            return sum;
        }
    }

    #endregion



    public static GameManager ourInstance;
    public AudioManager myAudioManager { get; private set; }

    private StageManager myStageManager;
    private GameData myGameData;

    [SerializeField] public bool myIsDebugging = false;
    [SerializeField] Image myFadeImage;

    [SerializeField] private float myFadeTime;

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
                Log("Loading data");
                LoadGameData();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Log("Deleting data");
                DeleteSavedGameData();
                LoadGameData(); // Create new instance
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Log("Saving data");
                SaveGameData();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Log("Inspecting");
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
    }

    public void RestartCurrentStage()
    {
        myStageManager.ReloadCurrentStage();
    }

    /// <summary>
    /// Load data when game is started. Always in memory.
    /// </summary>
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/AChristmasCarrotSaveData.dat";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            myGameData = (GameData)bf.Deserialize(file);
            file.Close();
            Log("Game data loaded!");
        }
        else
        {
            myGameData = new GameData();
            Log("Fresh file created!");
        }
    }

    public void DeleteSavedGameData()
    {
        string filePath = Application.persistentDataPath + "/AChristmasCarrotSaveData.dat";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Log("File deleted");
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
        Log("Game data saved!");
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

            // TEMP HACK for setting correct number of available snowflakes (hard-coded)
            var idx = SceneUtility.GetBuildIndexByScenePath(aScenePath);
            someNewData.myNumAvailable = ourPickupsPerStage[idx];
            someNewData.myNumCollected = someNewData.myCollectables.Count;
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
                // TEMP HACK for setting correct number of available snowflakes (hard-coded)
                var idx = SceneUtility.GetBuildIndexByScenePath(aScenePath);
                currentData.myNumAvailable = ourPickupsPerStage[idx];
                myGameData.myStageDataStr[aScenePath] = currentData;
                SaveGameData();
            }

        }

    }

    public void TransitionToStage(string aScenePath)
    {
        // TODO: Start coroutine for LoadStage async
        StartCoroutine(LoadStage(aScenePath));

    }

    public void TransitionToMainMenu()
    {
    }

    private IEnumerator LoadStage(string aScenePath)
    {
        //SceneManager.LoadScene(...);
        float t = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(aScenePath);
        asyncLoad.allowSceneActivation = false;        
        while (t < 1)
        {

            t = Mathf.Clamp(t + Time.deltaTime / myFadeTime, 0, 1);

            Color current = myFadeImage.color;
            current.a = t;

            myFadeImage.color = current;
            yield return null;
        }
        yield return (asyncLoad.progress > 0.90f);
        StartCoroutine(FadeIn());
        asyncLoad.allowSceneActivation = true;
        OnStageBegin();
        //SceneManager.LoadSceneAsync(aScenePath);


        // Wait for next frame when the scene is fully loaded and active
    }

    private IEnumerator FadeIn()

    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.Clamp(t - Time.deltaTime / myFadeTime, 0, 1);

            Color current = myFadeImage.color;
            current.a = t;

            myFadeImage.color = current;
            yield return null;
        }
    }

    private void OnStageBegin()
    {        
        StartOrChangeMusic();        
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
        foreach (KeyValuePair<string, GameData.StageData> entry in myGameData.myStageDataStr)
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

    public int myCurrentlyPlaying = -1;

    private void Start()
    {
        myCurrentlyPlaying = -1;
        StartOrChangeMusic();
    }


    public void StartOrChangeMusic(int aBuildIndex = -1)
    {
        int currentLevel = aBuildIndex > -1 ? aBuildIndex : SceneManager.GetActiveScene().buildIndex;

        if (currentLevel <= 1 && myCurrentlyPlaying != 1)
        {
            myCurrentlyPlaying = 1;
            GameManager.ourInstance.PlayMusic("Happy_Frog", 0.8f, true);
        }
        else if (currentLevel == 2 && myCurrentlyPlaying != 2)
        {
            myCurrentlyPlaying = 2;
            GameManager.ourInstance.PlayMusic("First_Level_Music", 0.8f, true);
        }
        else if (currentLevel >= 3 && currentLevel <= 5 && myCurrentlyPlaying != 3)
        {
            myCurrentlyPlaying = 3;
            GameManager.ourInstance.PlayMusic("World1_Music", 0.8f, true);
        }
        else if (currentLevel >= 6 && currentLevel <= 9 && myCurrentlyPlaying != 4)
        {
            myCurrentlyPlaying = 4;
            GameManager.ourInstance.PlayMusic("World2_Music", 0.8f, true);
        }
        else if (currentLevel >= 10 && currentLevel <= 13 && myCurrentlyPlaying != 5)
        {
            myCurrentlyPlaying = 5;
            GameManager.ourInstance.PlayMusic("World3_Music", 0.8f, true);
        }
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

    public void PlayMusic(string anAudioName, float someVolume = 0.165f, bool aShouldRestart = false)
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

    private void Log(string aString)
    {
#if UNITY_EDITOR
        Debug.Log(aString);
#endif
    }
}
