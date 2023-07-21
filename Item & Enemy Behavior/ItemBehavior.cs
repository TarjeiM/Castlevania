using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private string id = "healthup1"; // unique to this item
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
            playerStats.itemsCollected.Add(id, true); 
            Destroy(this.gameObject);
        }
    }
}
