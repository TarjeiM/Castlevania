using UnityEngine;

public class PlayerStateManager : MonoBehaviour
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
    public BoxCollider2D playerBox;
    public BoxCollider2D playerCrouchBox;
    public PolygonCollider2D playerAttackBox;
    public PolygonCollider2D playerCrouchAttackBox;
    public CircleCollider2D playerSpecialAttackBox;
    public CircleCollider2D playerAirAttackBox;
    public Animator playerAnimator;

    [SerializeField] private LayerMask groundMask;

    // PLAYER STATS
    public float playerRunSpeed = 7f;
    public float playerJumpForce = 13f;
    public bool isAttacking;

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

    public bool IsGrounded() // groundcheck with boxcast, returns bool
    {
        return Physics2D.BoxCast(new Vector2(playerBox.bounds.center.x, 
        (playerBox.bounds.center.y - (playerBox.size.y / 2))), 
        new Vector2(playerBox.size.x, .01f), 0f, Vector2.down, .1f, groundMask);
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
        crouchState.isCrouchAttacking = false;
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
    private void DisableAirAttackBox() {
        playerAirAttackBox.enabled = false;
    }
    private void ResetAirAttack() {
        isAttacking = false;
    }
}
