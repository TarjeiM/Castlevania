using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D rbody;
    private float axisH = 0.0f;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jump = 9.0f;
    [SerializeField] private LayerMask groundLayer;
    private bool goJump = false;
    private bool onGround = false;
    public static string gameState = "playing";

    // Singleton pattern
    public static PlayerController instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        { 
            Debug.Log("Deleted excess player controller from scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private int jumpCount;

    // Interface method, reads saved data
    public void LoadData(GameData data) {
    }
    // Interface method, writes save data
    public void SaveData(ref GameData data) { 
    }

    private void Start() 
    {
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";
    }

    private void Update() 
    {
        if (gameState != "playing") {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0.0f) {
            transform.localScale = new Vector2(1, 1); // flip gameobject
        }
        else if (axisH < 0.0f) {
            transform.localScale = new Vector2(-1, 1); // flip gameobject
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.P)) {
            Debug.Log(jumpCount);
        }
    }

    private void FixedUpdate() 
    {
        if (gameState != "playing") {
            return;
        }

        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0) {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump) {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }     
    }

    private void Jump() {
        goJump = true;
        jumpCount++;
    }
}
