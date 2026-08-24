using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;


    private PlayerInputs playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;



    private void Awake()
    {
        playerControls = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Update is called once per frame
    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
