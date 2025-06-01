using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float _vertical;
    private float _horizontal;
    [SerializeField]
    private JumpButton _jumpButton;
    
    public float Vertical{ get => _vertical; }
    public float Horizontal{ get => _horizontal; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            _jumpButton.OnJumpButtonDown();
        }
    }
}
