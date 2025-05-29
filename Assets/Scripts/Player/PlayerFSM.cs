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
    private GameObject player;

    private GameObject _instance;
    private PlayerState currentState;
    private PlayerAnimator animator;
    private PlayerController playerController;

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
        player = GameObject.FindGameObjectWithTag("Player");
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
                animator.SetAnimatorTrigger("Attack");
                playerController.DoAttackHit();
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
