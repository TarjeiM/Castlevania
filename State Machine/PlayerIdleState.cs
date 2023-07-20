using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    private float dirY = 0f; // store vertical input
    public override void EnterState(PlayerStateManager player) {
        // starting conditions, like animation.Play()
        Debug.Log("Hello from idle state");
        player.ResetAttack();
        player.playerAnimator.Play("Hero_Idle");
        player.playerRigidbody.velocity = Vector2.zero;
    }
    public override void UpdateState(PlayerStateManager player) {
        // take directional input
        dirY = Input.GetAxisRaw("Vertical");
        dirX = Input.GetAxisRaw("Horizontal");

        if (player.isAttacking == false) {
            // space button triggers switch to jump state
            if (Input.GetButtonDown("Jump")) {
                player.SwitchState(player.jumpState);
            }
            // vertical input can trigger switch to crouch state
            if (dirY < 0f) {
                player.SwitchState(player.crouchState);
            }
            // horizontal input triggers switch to run state
            if (dirX != 0) {
                player.SwitchState(player.runState);
            }
            if (Input.GetButtonDown("Attack")) { // main attack, joystick button 2 (B)
                Attack(player);
            }
            if (Input.GetButtonDown("Special")) { // special attack, joystick button 5 (RB)
                SpecialAttack(player);
            }
        }
    }
    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }
    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision) {

    }
    private void Attack(PlayerStateManager player)
    {
        player.playerAnimator.Play("Hero_Attack", -1, 0f);
        player.isAttacking = true;
    }
    private void SpecialAttack(PlayerStateManager player)
    {
        player.playerAnimator.Play("Hero_Special", -1, 0f);
        player.isAttacking = true; 
    }
}
