using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Camera Stuff")]
    [SerializeField] private GameObject _cameraFollowObject;
    private CameraFollowOBJECT cameraFollowObject;  
    [Header("Movement")]
    public float moveSpeed = 15f;

    [Header("Jump")]
    public float jumpForce = 45f;          
    public float fallMultiplier = 10f;
    public float lowJumpMultiplier = 10f;
    public float ascentMultiplier = 3.5f;  

    [Header("Camera")]
    public Camera mainCamera;

    private Rigidbody2D rb;
    private bool IsFacingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null) mainCamera = Camera.main;

        if (_cameraFollowObject != null)
        {
            cameraFollowObject = _cameraFollowObject.GetComponent<CameraFollowOBJECT>();   
        }
      
    }

    void Update()
    {
        Time.timeScale = 2f;
        
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A)){
            moveInput -= 1f;
           Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        if (Input.GetKey(KeyCode.D)){
            moveInput += 1f;
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (ascentMultiplier - 1f) * Time.deltaTime;
        }

        
        if (rb.linearVelocity.y < 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
        
        
       
       
    }
    private void Turn()
    {
        if(IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = false;

            if (cameraFollowObject != null) cameraFollowObject.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = false;

            if (cameraFollowObject != null) cameraFollowObject.CallTurn();
        }
    }
}
