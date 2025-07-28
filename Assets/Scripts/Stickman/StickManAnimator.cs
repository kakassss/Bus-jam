using UnityEngine;

public class StickManAnimator
{
    private Animator _animator;
    private float _runTransitionTime;
    
    private static readonly int StickMan_Idle = Animator.StringToHash("An_Happy_Idle");
    private static readonly int StickMan_Move = Animator.StringToHash("An_Standard_Run");
    
    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void SetMovementAnimation()
    {
        _animator.CrossFade(StickMan_Move,0f);
    }

    public void SetIdleAnimation()
    {
        _animator.CrossFade(StickMan_Idle,0f);  
    } 
}