using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick joyStick;
    [SerializeField]
    private float moveSpeed = 3.0f;
    
    private Rigidbody rigid;
    private float verticalMove;
    private float horizontalMove;
    private Vector3 moveVec;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
        moveVec = new Vector3(verticalMove, 0, horizontalMove) * moveSpeed * Time.deltaTime;
        
        if (moveVec.magnitude == 0)
        {
            return;
        }
        
        //Move
        rigid.MovePosition(rigid.position + moveVec);
        
        //Rotate
        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }

    
}
