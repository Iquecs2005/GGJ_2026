using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    //[Header("Player Variables")]
    public Vector2 facingDirection { get; private set; }
    public PlayerState playerState { get; private set; }

    [Header("Events")]
    [SerializeField] private UnityEvent onFacingDirectionChange;
    [SerializeField] private UnityEvent<PlayerState> onPlayerStateChange;

    public void SetFacingDirection(Vector2 newDirection) 
    {
        if (facingDirection != newDirection)
        {
            facingDirection = newDirection;
            onFacingDirectionChange.Invoke();
        }
    }

    public void SetPlayerState(PlayerState playerState)
    {
        if (this.playerState == playerState)
            return;

        this.playerState = playerState;
        onPlayerStateChange.Invoke(this.playerState);
    }
}

public enum PlayerState 
{
    Idle, Blocked
}
