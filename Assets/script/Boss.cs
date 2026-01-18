using UnityEngine;

public class Boss : MonoBehaviour
{
    public float fallMultiplier = 15f;
    public float moveSpeed = 0.0000005f;
    public float moveInput = 0f;

    private Rigidbody2D rb;

    [Header("Animation")]
    public Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
         if (Input.GetKey(KeyCode.A)) moveInput -= 1f;
         if (Input.GetKey(KeyCode.D)) moveInput += 1f;

         rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

         animator.SetFloat("Boss_Speed", Mathf.Abs(rb.linearVelocity.x));

         if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
    }
}
