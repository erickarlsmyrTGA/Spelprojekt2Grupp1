using UnityEngine;
using UnityEngine.Assertions;
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
        var scene = levels[++myCurrentSceneIndex];
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ReloadCurrentStage()
    {
        var scene = levels[myCurrentSceneIndex];
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
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
