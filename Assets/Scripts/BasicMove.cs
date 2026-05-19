using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMove : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float velocity = 5f;
    [SerializeField] float jumpForce = 8f;
    Vector2 input;

    private CheckGround _checkGround;

    void Start()
    {
        TryGetComponent<Rigidbody2D>(out body);
        _checkGround = GetComponentInChildren<CheckGround>();
    }

    private void FixedUpdate()
    {
        // Usa body.velocity si tu Unity es anterior a 2023
        body.linearVelocity = new Vector2(input.x * velocity, body.linearVelocity.y);
    }

    // Llamado desde el evento Move del Player Input
    public void Move(InputAction.CallbackContext context)
    {
        if (body == null) { return; }
        input = context.ReadValue<Vector2>();
    }

    // Llamado desde el evento Jump del Player Input
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;   // solo en el momento de pulsar
        if (_checkGround == null) return;
        if (_checkGround.isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }
    }
}
