using System;
using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    //플레이어의 대쉬 스킬
    [SerializeField] 
    private float dashPower = 10f;
    [SerializeField]
    private float dashDuration = 1f;
    private Rigidbody rb;
    
    public override void Activate(Creature actor)
    {
        //일단 느낌 잡음
        //TODO 좀 더 깔끔하게 짜기
        base.Activate(actor);
        CurrentCool = CoolDown;
        rb = actor.GetComponent<Rigidbody>();
        rb.AddForce(actor.transform.forward * dashPower, ForceMode.Impulse);

        Debug.Log("Dash activated");
    }
    


}
