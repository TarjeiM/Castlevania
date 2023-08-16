using UnityEngine;

public class ItemBehavior : MonoBehaviour, ICollectible
{
    [SerializeField] private string id = ""; // unique to this item
    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStats.itemsCollected.Add(id);
            playerStats.ScaleStatsToLevel(); 
            Destroy(this.gameObject);
        }
    }

    public void CheckCollectStatus() // called by data persistence manager on scene load
    {
        if (playerStats.itemsCollected.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }  
}
