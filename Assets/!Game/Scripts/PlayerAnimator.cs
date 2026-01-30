using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    public void UpdateAnimator(Vector3 velocity)
    {
        Vector3 horizontalVelocity = new Vector3(GetComponent<CharacterController>().velocity.x, 0f, GetComponent<CharacterController>().velocity.z);
        float speed = horizontalVelocity.magnitude;
        animator.SetFloat(Speed, speed);
    }
}
