using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isRotating = true;
    private bool isMoving = false;
    private SphereCollider sCollider;
    private BoxCollider bCollider;
    private Transform player;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float moveSpeed;

    void Start()
    {
        sCollider = GetComponent<SphereCollider>();
        bCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(isRotating)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) < 0.1f)
            {
                GameManager.instance.AddCoin();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;

        bCollider.isTrigger = true;
        isRotating = false;
        isMoving = true;
        player = other.transform;
    }
}