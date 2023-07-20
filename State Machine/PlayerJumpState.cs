using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private float dirX = 0f; // store horizontal input
    public bool isAttacking = false;
    public override void EnterState(PlayerStateManager player) {
        Debug.Log("Hello from jump state");
        Vector2 jumpPw = new Vector2(0, player.playerJumpForce);
        player.playerRigidbody.AddForce(jumpPw, ForceMode2D.Impulse);
        player.playerAnimator.Play("Hero_Jump");
    }
    public override void UpdateState(PlayerStateManager player) {
        // get current horizontal input
        dirX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Attack")) { // main attack, joystick button 2 (B)
            Attack(player);
        }
        // downward velocity triggers switch to fall state
        if (player.playerRigidbody.velocity.y < -.1f) {
            player.SwitchState(player.fallState);
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
