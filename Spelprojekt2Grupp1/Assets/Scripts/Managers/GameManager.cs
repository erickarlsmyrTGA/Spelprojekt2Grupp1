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

    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TransitionNextStage();
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
            Debug.LogError("There is no save data!");
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
    }

    public void OnStageCompleted()
    {
        // TODO: handle end of stage
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
