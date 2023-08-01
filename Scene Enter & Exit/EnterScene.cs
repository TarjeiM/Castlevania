using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private string lastExitName;
    [SerializeField] private GameObject player;
    private void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            player.transform.position = transform.position;
            // clear playerprefs after each successful scene transition
            PlayerPrefs.DeleteAll();
        }        
    }
}
