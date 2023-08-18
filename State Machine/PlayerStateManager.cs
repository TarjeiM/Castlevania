using UnityEngine;

public class PlayerStateManager : Subject
{
    // state instance references
    public PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerRunState runState = new PlayerRunState();
    public PlayerFallState fallState = new PlayerFallState();
    public PlayerCrouchState crouchState = new PlayerCrouchState();

    // PLAYER COMPONENTS 
    public Rigidbody2D playerRigidbody; 
    public CapsuleCollider2D playerBox;
    public CapsuleCollider2D playerCrouchBox;
    public PolygonCollider2D playerAttackBox;
    public PolygonCollider2D playerCrouchAttackBox;
    public CircleCollider2D playerSpecialAttackBox;
    public CircleCollider2D playerAirAttackBox;
    public Animator playerAnimator;
    public AudioSource attackSound;
    public AudioSource specialSound;

    [SerializeField] private LayerMask groundMask;

    // PLAYER STATS
    public float playerRunSpeed = 10f;
    public float playerJumpForce = 18f;
    public bool isAttacking;

    // Singleton Pattern Static Instance 
    public static PlayerStateManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        { 
            Debug.Log("Deleted excess player state manager from scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Start()
    {
        // state machine initial state
        playerCrouchBox.enabled = false;
        playerAttackBox.enabled = false;
        playerCrouchAttackBox.enabled = false;
        playerSpecialAttackBox.enabled = false;
        playerAirAttackBox.enabled = false;

        currentState = idleState;
        currentState.EnterState(this); 
    }

    private void Update()
    {
        currentState.UpdateState(this);
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); } 
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter2D(this, other);
    }

    public void SwitchState(PlayerBaseState state) 
    {
        currentState = state;
        state.EnterState(this);
    }

    public bool IsGrounded() 
    {
        Vector2 capsuleCenter = (Vector2)transform.position + Vector2.down * .1f * 0.5f;

        RaycastHit2D hit = Physics2D.CapsuleCast(
            capsuleCenter,
            new Vector2(.2f * 2f, .1f),
            CapsuleDirection2D.Vertical,
            0f,
            Vector2.down,
            .1f * 0.5f,
            groundMask
        );

        // If the capsule cast hits something, the character is grounded
        return hit.collider != null;
    }

    // REGULAR ATTACK - methods called in animation events
    private void EnableAttackBox() {
        playerAttackBox.enabled = true;
    }
    private void DisableAttackBox() {
        playerAttackBox.enabled = false;
    } 
    public void ResetAttack() { // used for grounded attacks
        isAttacking = false;
        playerAnimator.Play("Hero_Idle");
    }

    // CROUCH ATTACK - methods called in animation events
    private void EnableCrouchAttackBox() {
        playerCrouchAttackBox.enabled = true;
    }
    private void DisableCrouchAttackBox() {
        playerCrouchAttackBox.enabled = false;
    }
    private void ResetCrouchAttack() {
        isAttacking = false;
        playerAnimator.Play("Hero_Crouch");
    }

    // SPECIAL ATTACK - methods called in animation events
    private void EnableSpecialAttackBox() {
        playerSpecialAttackBox.enabled = true;
    }
    private void DisableSpecialAttackBox() {
        playerSpecialAttackBox.enabled = false;
    }

    // AIR ATTACK - methods called in animation events
    private void EnableAirAttackBox() {
        playerAirAttackBox.enabled = true;
    }
    public void DisableAirAttackBox() {
        playerAirAttackBox.enabled = false;
    }
    private void ResetAirAttack() {
        isAttacking = false;
    }

    private void IdleAnimation() {
        playerAnimator.Play("Hero_Idle");
    }

    // OBSERVER PATTERN
    private new void NotifyObservers()
    {
        base.NotifyObservers();
    }

    private void AddObserver()
    {

    }
    private void RemoveObserver()
    {
        
    }
}
