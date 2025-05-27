using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody rigidbody;
    private float moveSpeed;
    private float verticalMove;
    private float horizontalMove;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        verticalMove = inputManager.VerticalMove;
        horizontalMove = inputManager.HorizontalMove;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + rigidbody.linearVelocity * verticalMove * Time.fixedDeltaTime);
        rigidbody.MovePosition(rigidbody.position + rigidbody.linearVelocity * horizontalMove * Time.fixedDeltaTime);
    }

    
}
