using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Refs")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerAnimator playerAnimator;
    
    [Header("Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 12f; // higher = snappier
    [SerializeField] private float gravity = -20f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;

    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    private void Update()
    {
        HandleMovement();
        playerAnimator.UpdateAnimator(controller.velocity);
    }
    
    private void HandleMovement()
    { 
        // Read + normalize input so diagonal isn't faster
        Vector2 move = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        if (move.sqrMagnitude > 1f) move.Normalize();

        // World-space move direction (no camera)
        Vector3 moveDir = new Vector3(move.x, 0f, move.y);
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        // Yaw-only rotation (rotate only around Y axis)
        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
            Vector3 euler = targetRot.eulerAngles;
            Quaternion yawOnly = Quaternion.Euler(0f, euler.y, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, yawOnly, turnSpeed * Time.deltaTime);
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f; // stick to ground a bit
        velocity.y += gravity * Time.deltaTime;

        // Apply Move Dir with Move Speed for X/Z, and Gravity for Y
        Vector3 finalMove = moveDir * moveSpeed + new Vector3(0f, velocity.y, 0f);
        controller.Move(finalMove * Time.deltaTime);
    }

    public void LookAtPoint(Vector3 transformPosition)
    {
        Vector3 lookPos = new Vector3(transformPosition.x, transform.position.y, transformPosition.z);
        transform.LookAt(lookPos);
    }
}
