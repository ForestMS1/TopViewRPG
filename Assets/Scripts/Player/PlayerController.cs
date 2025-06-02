using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick joyStick;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField] 
    private float jumpForce = 4.0f;
    [SerializeField]
    private InputManager inputManager;
    private Player player;
    private Rigidbody rigid;
    private float verticalMove;
    private float horizontalMove;
    private Vector3 moveVec;
    private PlayerAnimator animator;
    private PlayerFSM playerFSM;
    private bool isGrounded;
    [SerializeField] 
    private float groundCheckDistance;
    private LayerMask groundLayer;
    
    

    void Start()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimator>();
        playerFSM = GetComponent<PlayerFSM>();
        isGrounded = true;
        groundLayer = LayerMask.GetMask("Ground");
        groundCheckDistance = 0.1f;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
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
        if (verticalMove == 0 || horizontalMove == 0)
        {
            verticalMove = inputManager.Vertical;
            horizontalMove = inputManager.Horizontal;
        }
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

    public void Jump()
    {
        if (!isGrounded) return; //바닥을 밟고있는게 아니라면 리턴
        rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
}
