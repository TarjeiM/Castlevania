using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from run state");
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
}
