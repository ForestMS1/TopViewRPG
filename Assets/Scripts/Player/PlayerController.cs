using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick joyStick;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private PlayerFSM playerFSM;
    
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
    
    
}
