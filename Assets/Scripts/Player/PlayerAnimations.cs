using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation:MonoBehaviour
{
    private static readonly int IsSittingHash = Animator.StringToHash("IsSitting");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private Animator animator;
    private PlayerMovement movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement.IsMoving && movement.IsRunning)
        {
            animator.SetFloat(SpeedHash, 5f);
        }
        else if (movement.IsMoving)
        {
            animator.SetFloat(SpeedHash, 1f);
        }
        else
        {
            animator.SetFloat(SpeedHash, 0f);
        }
    }

    public void SetSitting(bool sitting)
    {
        animator.SetBool(IsSittingHash, sitting);
    }

}