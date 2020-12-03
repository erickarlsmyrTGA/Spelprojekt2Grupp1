using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager ourInstance;
    private StageManager myStageManager;
    private GameData myGameData;

    [SerializeField] public bool myIsDebugging = false;
    
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
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Deleting data");
                DeleteSavedGameData();
            }
            if (Input.GetKeyDown(KeyCode.S))
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
        if (myGameData.myStageDataStr.TryGetValue(aScenePath, out GameData.StageData data))
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
        if (currentData.myIsStageCleared == false && someNewData.myIsStageCleared)
        {            
            myGameData.myStageDataStr[aScenePath] = someNewData;
            SaveGameData();
        }
        else
        {
            bool replaceOldData = false;
            // Merge hashsets
            foreach (var snowflake in someNewData.myCollectables)
            {
                if (!currentData.myCollectables.Contains(snowflake)) // A brand new flake
                {
                    currentData.myCollectables.Add(snowflake);
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

        LoadGameData();
    }
}
