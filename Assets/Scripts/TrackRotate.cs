using UnityEngine;

public class TrackRotate : MonoBehaviour
{
    public float rotateSpeed = 50f;
    private float currentYRotation = 0f;

    void Start()
    {
        currentYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        currentYRotation += rotateSpeed * Time.deltaTime;

        Vector3 euler = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(euler.x, currentYRotation, euler.z);
    }
}
