using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _anim;
    
    public Animator Animator
    {
        get { return _anim; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
        Animator.applyRootMotion = true;
    }

    public void SetAnimatorTrigger(string trigger)
    {
        Animator.SetTrigger(trigger);
    }

    public void ResetAnimatorTrigger(string trigger)
    {
        Animator.ResetTrigger(trigger);
    }

    public void SetFloat(string parameter, float value)
    {
        Animator.SetFloat(parameter, value);
    }
}
