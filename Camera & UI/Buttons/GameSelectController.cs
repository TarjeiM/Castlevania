using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectController : MonoBehaviour
{
    public void LoadIntroScene() // New Game
    {
        SceneManager.LoadSceneAsync("Intro Cinematic");
    }

    public void LoadLastSave()
    {
        // 
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
