using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick joyStick;
    [SerializeField]
    private float moveSpeed = 5.0f;
    
    private Rigidbody rigid;
    private float verticalMove;
    private float horizontalMove;
    private Vector3 moveVec;
    private Animator animator;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        //Input Value
        verticalMove = joyStick.Vertical;
        horizontalMove = joyStick.Horizontal;
        moveVec = new Vector3(horizontalMove, 0, verticalMove);
        
        //Move
        rigid.MovePosition(rigid.position + moveVec * moveSpeed * Time.deltaTime);
        animator.SetFloat("Speed", moveVec.normalized.magnitude);

        if (moveVec.magnitude < 0.1f)
        {
            return;
        }
        //Rotate
         Quaternion dirQuat = Quaternion.LookRotation(moveVec);
         Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
         rigid.MoveRotation(moveQuat);
    }

    
}
