using UnityEngine;

public abstract class PlayerBaseState
{
    // abstract methods must have their functionality
    // defined within derived class
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void FixedUpdateState(PlayerStateManager player);
    public abstract void OnCollisionEnter2D(PlayerStateManager player, Collision2D collision);
}
