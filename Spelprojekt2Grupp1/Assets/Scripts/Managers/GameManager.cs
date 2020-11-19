using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ourInstance;


    public bool HasNextStage()
    {
        return false;
    }

    public void TransitionNextStage()
    {
        // TODO: goto next stage.
        Debug.Log("Goal reached! Goto next if exists.");
    }

    public void RestartCurrentStage()
    {
    }

    public void TransitionToStage(int aStageIndex)
    {
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

    }
}
