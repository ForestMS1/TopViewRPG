using System;
using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    //플레이어의 대쉬 스킬
    [SerializeField]
    private float dashPower = 20f;
    [SerializeField]
    private float dashDuration = 0.1f;
    private float startTime;
    private Rigidbody rb;
    public SkillData.SkillType Type => SkillData.SkillType.Dash;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public override void Activate(Creature actor)
    {
        //일단 느낌 잡음
        //TODO 좀 더 깔끔하게 짜기
        if (!CanUse(actor))
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다!");
            return;
        }
        base.Activate(actor);
        StartCoroutine(nameof(Dash));
        
        Debug.Log($"{actor.name} : Dash activated!");
    }

    IEnumerator Dash()
    {
        startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + rb.transform.forward * dashPower * Time.deltaTime);

            yield return null;
        }
    }


}
