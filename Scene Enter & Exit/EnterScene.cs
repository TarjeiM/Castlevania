using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [SerializeField] private string lastExitName;
    [SerializeField] private GameObject player;
    void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            player.transform.position = transform.position;
        }        
    }
}
