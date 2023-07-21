using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnEnable()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
