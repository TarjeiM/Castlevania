using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectController : MonoBehaviour
{
    public void LoadIntroScene() // New Game
    {
        PlayerPrefs.SetString("NewGame", "true");
        SceneManager.LoadSceneAsync("Intro Cinematic");
    }

    public void LoadLastSave()
    {
        PlayerPrefs.SetString("NewGame", "false");
        SceneManager.LoadSceneAsync("Test Scene 1");
    }

    public void Options()
    {
        // 
    }
    
    public void EndGame() // Quit Game
    {
        Application.Quit();
    }

    
}
