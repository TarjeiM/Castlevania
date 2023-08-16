using UnityEngine;

public class MovingBoxBehavior : MonoBehaviour
{
    private bool isFalling = false;
    private bool wasFalling = false;
    [SerializeField] private GameObject tileBox;
    [SerializeField] private GameObject boxBlocker;
    private Rigidbody2D rb;
    private PlayerStats playerStats;
    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        tileBox.SetActive(false);
        boxBlocker.SetActive(true);       
    }
    private void Update()
    {
        if (rb.velocity.y < -.1f) { isFalling = true; wasFalling = true; }

        if (rb.velocity.y == 0f) { isFalling = false; }

        if (isFalling == false && wasFalling == true && transform.position.y < -9.5f)
        {
            this.gameObject.SetActive(false);
            tileBox.SetActive(true);
            boxBlocker.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerStats.itemsCollected.Contains("strengthchain"))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
