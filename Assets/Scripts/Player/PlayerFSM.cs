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

    private GameObject _instance;
    private PlayerState currentState;
    private PlayerAnimator animator;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        currentState = PlayerState.Idle;
        animator = GameObject.FindWithTag("Player").GetComponent<PlayerAnimator>();
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
                animator.SetAnimatorTrigger("Attack");
                break;
            //case PlayerState.Dead:
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
