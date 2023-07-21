using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from run state");
        player.ResetAttack();
        player.playerAnimator.Play("Hero_Run");
    }
    public override void UpdateState(PlayerStateManager player) {
        // get current horizontal input
        dirX = Input.GetAxisRaw("Horizontal");
        // space button triggers switch to jump state
        if (Input.GetButtonDown("Jump")) {
            player.SwitchState(player.jumpState);
        }
        // check for state exit condition
        if (player.playerRigidbody.velocity.y < -.1f) {
            player.SwitchState(player.fallState);
        }
        if (dirX == 0) {
            player.SwitchState(player.idleState);
        }
    }
    public override void FixedUpdateState(PlayerStateManager player) {
        // apply velocity according to horizontal input
        player.playerRigidbody.velocity = new Vector2(dirX * player.playerRunSpeed, player.playerRigidbody.velocity.y);
        if (player.isAttacking == false) {
            // flip gameobject
            if (dirX > 0.0f) {
                Vector3 rotator = new Vector3(0f, 0f, 0f);
                player.transform.rotation = Quaternion.Euler(rotator);
            }
            else if (dirX < 0.0f) {
                Vector3 rotator = new Vector3(0f, 180f, 0f);
                player.transform.rotation = Quaternion.Euler(rotator); 
            }
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision) {

    }
    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D other)
    {
        
    }
}
