using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    private float dirY = 0f;
    private float dirX = 0f;
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from crouch state");
        player.playerAnimator.Play("Hero_Crouch");
        player.playerCrouchBox.enabled = true;
        player.playerBox.enabled = false;
    }
    public override void UpdateState(PlayerStateManager player) {
        // take directional input
        dirY = Input.GetAxisRaw("Vertical");
        dirX = Input.GetAxisRaw("Horizontal");

        if (player.isAttacking == false) { 
            if (Input.GetButtonDown("Jump")) {
                StandingHitBox(player);
                player.SwitchState(player.jumpState);
            }
            if (dirY >= 0f) {
                StandingHitBox(player);
                player.SwitchState(player.idleState);
            }
            if (dirX != 0 && dirY == 0f) {
                StandingHitBox(player);
                player.SwitchState(player.runState);
            }
            if (Input.GetButtonDown("Attack")) { // main attack, joystick button 2 (B)
                CrouchingAttack(player);
            }
        }
    }
    public override void FixedUpdateState(PlayerStateManager player)
    {
        // do stuff
    }
    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision) {

    }
    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D other)
    {
       
    }
    private void CrouchingAttack(PlayerStateManager player)
    {
        player.playerAnimator.Play("Hero_Crouch_Attack", -1, 0f);
        player.isAttacking = true;
    }
    private void StandingHitBox(PlayerStateManager player) // enable the full height hitbox, call this on crouch exit
    {
        player.playerBox.enabled = true;
        player.playerCrouchBox.enabled = false;
    }
}