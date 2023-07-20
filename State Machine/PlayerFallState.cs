using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    public bool isAttacking = false;
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from fall state");
        player.playerAnimator.Play("Hero_Jump");
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
        // flip gameobject
        if (dirX > 0.0f) {
            Vector3 rotator = new Vector3(player.transform.rotation.x, 0f, player.transform.rotation.z);
            player.transform.rotation = Quaternion.Euler(rotator);
        }
        else if (dirX < 0.0f) {
            Vector3 rotator = new Vector3(player.transform.rotation.x, 180f, player.transform.rotation.z);
            player.transform.rotation = Quaternion.Euler(rotator); 
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision) {

    }

    private void Attack(PlayerStateManager player) 
    {
        player.playerAnimator.Play("Hero_Air_Attack", -1, 0f);
        isAttacking = true;
    }
}
