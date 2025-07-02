using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float _vertical;
    private float _horizontal;
    [SerializeField]
    private JumpButton _jumpButton;
    [SerializeField]
    private PlayerFSM _playerFSM;
    public float Vertical{ get => _vertical; }
    public float Horizontal{ get => _horizontal; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerFSM.IsControllable()) return;
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        // if (Input.GetKey(KeyCode.Space))
        // {
        //     _jumpButton.OnJumpButtonDown();
        // }
    }
}
