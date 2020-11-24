using UnityEngine;

public class SceneReferenceGM : MonoBehaviour
{
    [Tooltip("Show scene selector in-game")]
    [SerializeField] public bool myIsDebugging = false;
    private void OnGUI()
    {
        if (myIsDebugging)
        {
            DisplayLevel(world1Level1);
            DisplayLevel(world1Level2);
            DisplayLevel(world1Level3);
            DisplayLevel(world1Level4);
            DisplayLevel(world2Level1);
            DisplayLevel(world2Level2);
            DisplayLevel(world2Level3);
            DisplayLevel(world2Level4);
            DisplayLevel(world3Level1);
            DisplayLevel(world3Level2);
            DisplayLevel(world3Level3);
            DisplayLevel(world3Level4);
        }
    }

    public void DisplayLevel(SceneReference scene)
    {
        GUILayout.Label(new GUIContent("Scene name Path: " + scene));
        if (GUILayout.Button("Load " + scene))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }

    public SceneReference world1Level1;
    public SceneReference world1Level2;
    public SceneReference world1Level3;
    public SceneReference world1Level4;
    public SceneReference world2Level1;
    public SceneReference world2Level2;
    public SceneReference world2Level3;
    public SceneReference world2Level4;
    public SceneReference world3Level1;
    public SceneReference world3Level2;
    public SceneReference world3Level3;
    public SceneReference world3Level4;
    
}
