using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions playerActions;

    private Rigidbody2D rb;

    public float moveSpeed = 1f;
    private float moveX;
    

    [Header("Jumping Fields")]
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public float lowJumpMultiplier = 0.875f;

    private void OnEnable()
    {
        playerActions.ground.Enable();
    }

    private void OnDisable()
    {
        playerActions.ground.Disable();
    }

    private void Awake()
    {
        playerActions = new PlayerActions();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveX = playerActions.ground.MoveH.ReadValue<float>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hit.collider != null;

        if (playerActions.ground.Jump.triggered && isGrounded)
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }

       
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        //Debug.Log(moveX);

        if (!playerActions.ground.Jump.IsPressed() && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * lowJumpMultiplier);

        }
    }
}