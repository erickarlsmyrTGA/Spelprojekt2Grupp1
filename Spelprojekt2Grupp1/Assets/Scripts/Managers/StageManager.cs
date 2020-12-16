using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Tooltip("Show scene selector in-game")]
    [SerializeField] public bool myIsDebugging = false;

    [SerializeField]
    public SceneReference[] levels;
    private int myCurrentSceneIndex;

    public StageManager()
    {
    }

    public void Start()
    {
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        for (int i = 0; i < levels.Length; i++)
        {
            Assert.IsNotNull(levels[i]);
            if (levels[i].ScenePath == currentScene.path)
            {
                myCurrentSceneIndex = i;
                break;
            }
            
        }
    }
        
    public void GoToNextStage()
    {
        var currentScene = SceneManager.GetActiveScene();
        var buildIndex = SceneUtility.GetBuildIndexByScenePath(currentScene.path);
        buildIndex++;
        if (buildIndex >= 2 && buildIndex <= 14)
        {
            if (buildIndex == 14)
            {
                buildIndex = 17; // Roll credits!
            }
            var scenepath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            GameManager.ourInstance.TransitionToStage(scenepath); // yeah, I know
        }
        else
        {
            var scene = SceneManager.GetSceneAt(0); // Goto main menu
            GameManager.ourInstance.TransitionToStage(scene.path);
        }
        
    }

    public void ReloadCurrentStage()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.path);
    }

    public bool HasNextStage()
    {
        return (myCurrentSceneIndex < levels.Length && levels[myCurrentSceneIndex + 1] != null);
    }

    private void OnGUI()
    {
        if (myIsDebugging)
        {
            foreach (var scene in levels)
            {
                DisplayStageButton(scene);
            }
        }
    }

    public void DisplayStageButton(SceneReference scene)
    {
        GUILayout.Label(new GUIContent("Scene name Path: " + scene));
        if (GUILayout.Button("Load " + scene))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }

}
