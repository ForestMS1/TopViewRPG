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

    [Header("작동 관련")] 
    private float castTime; //시전 시간
    //private bool isUnlocked; //습득 여부
    private SkillType type;
    private SkillTargetingType targeting;

    public enum SkillType
    {
        Active,
        Passive,
        Buff,
        Debuff
    }

    public enum SkillTargetingType
    {
        Self,
        SingleEnemy,
        Area,
        Directional
    }

    public virtual void Activate(Creature actor)
    {
        if (!CanUse())
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다!");
            return;
        }
        StartCooldown();
    }

    private bool CanUse()
    {
        return currentCooldown <= 0; //TODO MP양도 확인
    }

    public void StartCooldown()
    {
        currentCooldown = coolDown;
        
    }
}
