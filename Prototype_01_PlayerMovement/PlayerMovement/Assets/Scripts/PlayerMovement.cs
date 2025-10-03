using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] float speed = 5.0f;
    [SerializeField] float acceleration = 10f;
    [Space]
    [Header("Jumping")]
    [SerializeField] float jumpingVelocity = 12f;
    [SerializeField] int jumpCounts = 2;
    [SerializeField] Transform feetPosition;
    [SerializeField] float raycastRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    float targetSpeed;
    float finalSpeed;
    bool isGrounded = false;
    bool wasGrounded = false;
    bool isFacingRight = true;
    int jumpRemaining;
    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, raycastRadius, groundLayer);

        if (!wasGrounded && isGrounded)
        {
            jumpRemaining = jumpCounts;
        }

        wasGrounded = isGrounded;
        
        // we get input axis aka move left or right or dont
        float moveInput = Input.GetAxis("Horizontal");
        targetSpeed = speed * moveInput;
        Flip(moveInput);

        if (Input.GetButtonDown("Jump") && jumpRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpingVelocity);
            jumpRemaining--;
        }
    }

    void FixedUpdate()
    {
        finalSpeed = Mathf.MoveTowards(rb.linearVelocityX, targetSpeed, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(finalSpeed, rb.linearVelocityY);
    }


    void Flip(float moveInput)
    {
        if (moveInput > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
            isFacingRight = true;
        }


        else if (moveInput < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
            isFacingRight = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(feetPosition.position, raycastRadius);
    }
}
