using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class StageEditorInputManager : MonoBehaviour
{
    public float camMoveSpeed = 5f;
    public float camRotateSpeed = 3f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    public Transform cam;
    public TMP_Text cursorModeText;

    private float rotationX;
    private float rotationY;
    private bool cursorLocked = false;

    void Start()
    {
        Vector3 angles = cam.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
        SetCursorLock(false);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnToggleCursor(InputValue value)
    {
        cursorLocked = !cursorLocked;
        SetCursorLock(cursorLocked);
    }

    public void OnPlaceObject( InputValue value )
    {
        StageEditor.instance.PlaceObjectOnRightClick(  );
    }
    
    public void OnDeleteObject( InputValue value )
    {
        if( !cursorLocked ) return;
        StageEditor.instance.DeleteObjectOnLeftClick(  );
    }

    public void OnRotate( InputValue value )
    {
        StageEditor.instance.RotateObjectOnR( );
    }

    void Update()
    {
        if (!cursorLocked) return;

        RotateCam();
        MoveCam();
    }

    private void MoveCam()
    {
        if (cam == null || moveInput == Vector2.zero) return;

        Vector3 move = cam.right * moveInput.x + cam.forward * moveInput.y;
        cam.position += move * camMoveSpeed * Time.deltaTime;
    }

    private void RotateCam()
    {
        if (cam == null || lookInput == Vector2.zero) return;

        rotationX += lookInput.x * camRotateSpeed;
        rotationY -= lookInput.y * camRotateSpeed;
        rotationY = Mathf.Clamp(rotationY, -89f, 89f);

        cam.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }

    private void SetCursorLock(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
        cursorModeText.text = locked ? "커서 잠김" : "커서 풀림";
    }
}