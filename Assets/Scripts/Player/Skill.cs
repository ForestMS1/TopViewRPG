using System.Collections;
using UnityEngine;

public class Skill
{
    //스킬은 플레이어, 몬스터, 보스 등 모두가 사용할 수 있도록 설계
    [Header("기본 정보")]
    private string name;
    private string description;
    private float coolDown;
    private float currentCooldown;
    private float mpCost;
    private Sprite icon;
    
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public float CoolDown { get => coolDown; set => coolDown = value; }
    public float CurrentCool { get => currentCooldown; set => currentCooldown = value; }
    public float MpCost { get => mpCost; set => mpCost = value; }
    public Sprite Icon { get => icon; set => icon = value; }

    [Header("작동 관련")] 
    private float castTime; //시전 시간
    //private bool isUnlocked; //습득 여부
    private SkillData.SkillType type;
    private SkillData.SkillTargetingType targeting;
    
    public float CastTime { get => castTime; set => castTime = value; }
    public SkillData.SkillType Type { get => type; set => type = value; }
    public SkillData.SkillTargetingType Targeting { get => targeting; set => targeting = value; }

    

    public void Init(SkillData skillData)
    {
        Name = skillData._name;
        Description = skillData._description;
        CoolDown = skillData._cooldown;
        CurrentCool = 0f;
        MpCost = skillData._mpCost;
        Icon = skillData._icon;
        
        CastTime = skillData._castTime;
        Type = skillData._type;
        Targeting = skillData._targeting;
    }

    public virtual void Activate(Creature actor)
    {
        CurrentCool = CoolDown;
    }

    public bool CanUse()
    {
        return currentCooldown <= 0; //TODO MP양도 확인
    }
    
    public virtual void UpdateCooldown(float deltaTime)
    {
        if (CurrentCool > 0f)
            CurrentCool -= deltaTime;

        if (CurrentCool < 0f)
            CurrentCool = 0f;
    }
}
