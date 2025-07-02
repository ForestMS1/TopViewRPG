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
        SpecialSkill,
        UltimateSkill,
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
                currentState = newState;
                break;
            case PlayerState.SpecialSkill:
                animator.SetAnimatorTrigger("Special");
                currentState = newState;
                break;
            case PlayerState.UltimateSkill:
                animator.SetAnimatorTrigger("Ultimate");
                currentState = newState;
                break;
            case PlayerState.Jump:
                animator.SetAnimatorTrigger("Jump");
                playerController.Jump();
                currentState = newState;
                break;
            case PlayerState.Damaged:
                animator.SetAnimatorTrigger("GetHit");
                currentState = newState;
                break;
            case PlayerState.Dead:
                animator.SetAnimatorTrigger("Die");
                currentState = newState;
                break;
        }
        currentState = PlayerState.Idle;
    }

    public bool IsControllable()
    {
        if (currentState == PlayerState.Attack || currentState == PlayerState.SpecialSkill || currentState == PlayerState.UltimateSkill
            || currentState == PlayerState.Damaged || currentState == PlayerState.Dead)
        {
            return false;
        }
        return true;
    }
}
