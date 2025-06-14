using Unity.VisualScripting;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Attack,
        Damaged,
        Dead
    }

    [SerializeField] 
    private Player player;
    
    private PlayerController playerController;
    private PlayerState currentState;
    private PlayerAnimator animator;
    
    void Start()
    {
        currentState = PlayerState.Idle;
        animator = player.GetComponent<PlayerAnimator>();
        playerController = player.GetComponent<PlayerController>();
    }

    public PlayerState GetState()
    {
        return currentState;
    }
    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case PlayerState.Attack:
                if (!player.CanAttack()) break;
                //animator.SetAnimatorTrigger("Attack");
                player.DoAttackHit<Enemy>();
                break;
            case PlayerState.Jump:
                animator.SetAnimatorTrigger("Jump");
                playerController.Jump();
                break;
            case PlayerState.Damaged:
                animator.SetAnimatorTrigger("GetHit");
                break;
            case PlayerState.Dead:
                animator.SetAnimatorTrigger("Die");
                break;
        }
        currentState = newState;
    }

    public bool IsControllable()
    {
        if (currentState == PlayerState.Damaged || currentState == PlayerState.Dead)
        {
            return false;
        }
        return true;
    }
}
