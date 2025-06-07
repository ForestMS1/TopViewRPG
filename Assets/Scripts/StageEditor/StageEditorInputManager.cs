using UnityEngine;
using UnityEngine.InputSystem;

public class StageEditorInputManager : MonoBehaviour
{
    public float camMoveSpeed = 5f;
    public float camRotateSpeed = 3f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    public Transform cam;
    private float rotationX;
    private float rotationY;

    void Start()
    {
        Vector3 angles = cam.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        RotateCam();
        MoveCam();
    }

    private void MoveCam()
    {
        if (cam == null || moveInput == Vector2.zero) return;

        Vector3 move = (cam.right * moveInput.x + cam.forward * moveInput.y);
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
}