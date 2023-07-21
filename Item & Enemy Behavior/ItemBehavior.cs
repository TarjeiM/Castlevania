using UnityEngine;

public class ItemBehavior : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id = "healthup1"; // unique to this item
    private PlayerStats playerStats;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStats.itemsCollected.Add(id); 
            Destroy(this.gameObject);
        }
    }

    public void CheckCollectStatus(PlayerStats playerStats) // called by data persistence manager on scene load
    {
        if (playerStats.itemsCollected.Contains(id))
        {
            Destroy(this.gameObject);
        }

    }

    public void LoadData(GameData data)
    {
        // inteface method
    }
    public void SaveData(ref GameData data)
    { 
        // interface method
    }

}
