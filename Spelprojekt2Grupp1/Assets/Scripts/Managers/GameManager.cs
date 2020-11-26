using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ourInstance;
    private StageManager myStageManager;



    public void TransitionNextStage()
    {        
        myStageManager.GoToNextStage();
        // TODO: use coroutine LoadStage - but if above works ok, scratch this for reducing complexity
    }

    public void RestartCurrentStage()
    {
        myStageManager.ReloadCurrentStage();
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
    }
}
