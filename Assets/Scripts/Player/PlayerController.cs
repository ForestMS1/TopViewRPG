using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick joyStick;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private PlayerFSM playerFSM;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField] 
    private float attackCoolTime = 0.5f;

    private float lastAttackTime = 0.0f;
    private Rigidbody rigid;
    private float verticalMove;
    private float horizontalMove;
    private Vector3 moveVec;
    private PlayerAnimator animator;
    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimator>();
    }

    void FixedUpdate()
    {
        if (!playerFSM.IsControllable()) return;
        
        Move();
    }

    void Move()
    {
        //Input Value
        verticalMove = joyStick.Vertical;
        horizontalMove = joyStick.Horizontal;
        moveVec = new Vector3(horizontalMove, 0, verticalMove);
        
        
        //Move
        rigid.MovePosition(rigid.position + moveVec * moveSpeed * Time.deltaTime);
        animator.SetFloat("Speed", moveVec.normalized.magnitude);
        playerFSM.ChangeState(PlayerFSM.PlayerState.Walk);

        if (moveVec.magnitude < 0.1f)
        {
            playerFSM.ChangeState(PlayerFSM.PlayerState.Idle);
            return;
        }
        //Rotate
        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }

    public void DoAttackHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, attackRange);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log($"{hit.name} is hit!");
                }
            }   
        }
    }

    public bool CanAttack()
    {
        if (lastAttackTime + attackCoolTime <= Time.time)
        {
            lastAttackTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
