using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    // this class exists for testing purposes
    [SerializeField] private string sceneToLoad;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
