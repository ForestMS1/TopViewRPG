using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private static InputManager instance;

    private float verticalMove;
    private float horizontalMove;

    public float VerticalMove
    {
        get
        {
            return verticalMove;
        }
    }

    public float HorizontalMove
    {
        get
        {
            return horizontalMove;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        // {
        //     verticalMove = Input.GetAxis("Vertical");
        //     horizontalMove = Input.GetAxis("Horizontal");   
        // }
    }
}
