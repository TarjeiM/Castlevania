using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private string lastExitName;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            player.transform.position = transform.position;
            // clear playerprefs after each successful scene transition
            PlayerPrefs.DeleteAll();
        }        
    }
}
