using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene: MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string exitName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
