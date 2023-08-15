using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float dirX = 0f; 
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from run state");
        player.ResetAttack();
        player.playerAnimator.Play("Hero_Run");
    }
    public override void UpdateState(PlayerStateManager player) {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            player.SwitchState(player.jumpState);
        }
        else if (player.playerRigidbody.velocity.y < -.1f && !player.IsGrounded()) {
            player.SwitchState(player.fallState);
        }
        else if (dirX == 0) {
            player.SwitchState(player.idleState);
        }
    }
    public override void FixedUpdateState(PlayerStateManager player) {
        if (player.isAttacking == false) {
            player.playerRigidbody.velocity = new Vector2(dirX * player.playerRunSpeed, player.playerRigidbody.velocity.y);
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
    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision) 
    {
      
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D other)
    {
        
    }
}
