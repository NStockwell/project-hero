using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

/// <summary>
/// Remember to add Manager scene to Scenes in Build Settings
/// </summary>
public class SceneInstantiator : MonoBehaviour
{
    public Object persistentScene;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(persistentScene.name, LoadSceneMode.Additive);
    }

    [ContextMenu("LoadScene")]
    public void LoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
