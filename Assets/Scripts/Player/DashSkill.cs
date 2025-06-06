using System;
using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    //플레이어의 대쉬 스킬
    private float dashPower;
    private Rigidbody rb;

    public DashSkill(Creature actor, float dashPower = 15f)
    {
        this.dashPower = dashPower;
        rb = actor.GetComponent<Rigidbody>();
    }
    
    public override void Activate(Creature actor)
    {
        //일단 느낌 잡음
        //TODO 좀 더 깔끔하게 짜기
        if (!CanUse())
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다!");
            return;
        }
        base.Activate(actor);
        rb.AddForce(actor.transform.forward * dashPower, ForceMode.Impulse);

        Debug.Log($"{actor.name} : Dash activated!");
    }
    


}
