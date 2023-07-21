using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int lootExp = 100;
    private PlayerStats playerStats;
    void Start()
    {
        GetComponent<Animator>().Play("Flying");
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Attack")
        {
            health -= 4;
            if (health <= 0)
            {
                Die();
                return;
            }
            Debug.Log("Ow! Dummy took 4 damage and has " + health + " hp left");
        }
        else if (other.gameObject.name == "SpecialAttack")
        {
            health -= 12;
            if (health <= 0)
            {
                Die();
                return;
            }
            Debug.Log("Ow! Dummy took 4 damage and has " + health + " hp left");
        }
    }
    
    private void Die()
    {   
        Debug.Log("Dummy was slain");
        playerStats.GainExperience(lootExp);
        Destroy(this.gameObject);
    }
} 
