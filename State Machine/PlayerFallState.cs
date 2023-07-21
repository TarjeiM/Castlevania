using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from fall state");
        if (player.isAttacking == false) {
            player.playerAnimator.Play("Hero_Jump"); 
        }
    }
    public override void UpdateState(PlayerStateManager player) {
        // get current horizontal input
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Attack")) { // main attack, joystick button 2 (B)
            Attack(player);
        }
        // checking for ground and no vertical velocity to switch to idle or running
        if (player.IsGrounded()) {
            if (dirX == 0f && player.playerRigidbody.velocity.x == 0f) {
                player.SwitchState(player.idleState);
            }
            else {
                player.SwitchState(player.runState);
            }
        }
    }
    public override void FixedUpdateState(PlayerStateManager player)
    {
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
    private void Attack(PlayerStateManager player) 
    {
        player.playerAnimator.Play("Hero_Air_Attack", -1, 0f);
        player.isAttacking = true;
    }
}
