using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimatorTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void ResetAnimatorTrigger(string trigger)
    {
        anim.ResetTrigger(trigger);
    }

    public void SetFloat(string parameter, float value)
    {
        anim.SetFloat(parameter, value);
    }
}
