using UnityEngine;
using UnityEngine.InputSystem;

public class StageEditorInputManager : MonoBehaviour
{
    public float camMoveSpeed = 5f;
    private Vector2 moveInput;
    private Transform cam;

    void Start( )
    {
        cam = StageEditorCamera.instance.transform;
    }

    public void OnMove( InputValue value )
    {
        moveInput = value.Get<Vector2>( );
    }

    void Update( )
    {
        MoveCam( );
    }

    private void MoveCam( )
    {
        if( cam == null ) return;
        if( moveInput == Vector2.zero ) return;
        if( !StageEditorCamera.instance.isOrthographic ) return;

        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize( );

        Vector3 right = cam.transform.right;
        right.y = 0;
        right.Normalize( );

        Vector3 move = ( right * moveInput.x + forward * moveInput.y ) * camMoveSpeed * Time.deltaTime;
        cam.position += move;
    }
}