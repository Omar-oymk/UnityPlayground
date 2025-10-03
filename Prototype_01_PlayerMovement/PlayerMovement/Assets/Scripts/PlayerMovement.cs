using Unity.Hierarchy;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float acceleration = 10f;
    Rigidbody2D rb;
    float targetSpeed;
    float finalSpeed;
    bool isFacingRight = true;
    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        // we get input axis aka move left or right or dont
        float moveInput = Input.GetAxis("Horizontal");
        targetSpeed = speed * moveInput;
        Flip(moveInput);
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
}
