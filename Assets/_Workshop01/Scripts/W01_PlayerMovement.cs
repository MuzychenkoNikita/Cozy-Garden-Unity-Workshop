using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class W01_PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Movement")]
    public float moveSpeed = 6.0f;
    public float sprintSpeed = 8.5f;
    public float acceleration = 18f;
    public bool useSprint = true;

    [Header("Grounding")]
    public float stickToGroundForce = 2.0f;

    private CharacterController _cc;
    private UnityEngine.Vector3 _currentMove;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        if (!cameraTransform && Camera.main) cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        UnityEngine.Vector3 input = new UnityEngine.Vector3(x, 0f, z);
        float inputMag = Mathf.Clamp01(input.magnitude);

        UnityEngine.Vector3 forward = UnityEngine.Vector3.forward;
        UnityEngine.Vector3 right = UnityEngine.Vector3.right;

        if (cameraTransform)
        {
            forward = cameraTransform.forward;
            right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
        }

        UnityEngine.Vector3 desiredDir = (forward * z + right * x).normalized;

        float targetSpeed = moveSpeed;
        if (useSprint && Input.GetKey(KeyCode.LeftShift) && inputMag > 0.1f)
            targetSpeed = sprintSpeed;

        UnityEngine.Vector3 targetMove = desiredDir * (targetSpeed * inputMag);
        _currentMove = UnityEngine.Vector3.MoveTowards(_currentMove, targetMove, acceleration * Time.deltaTime);

        UnityEngine.Vector3 motion = _currentMove;
        motion.y = -stickToGroundForce;

        _cc.Move(motion * Time.deltaTime);
    }
}
