using UnityEngine;

public class MovingBoxBehavior : MonoBehaviour
{
    private bool isFalling = false;
    private bool wasFalling = false;
    [SerializeField] private GameObject tileBox;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tileBox.SetActive(false);       
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < -.1f) { isFalling = true; wasFalling = true; }

        if (rb.velocity.y == 0f) { isFalling = false; }

        if (isFalling == false && wasFalling == true)
        {
            this.gameObject.SetActive(false);
            tileBox.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
