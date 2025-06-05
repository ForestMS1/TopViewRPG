using UnityEngine;

public class Skill
{
    [Header("기본 정보")]
    private string name;
    private string description;
    private float cooldown;
    private float currentCooldown;
    private float mpCost;
    private Sprite icon;

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

    public virtual void Activate()
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
        currentCooldown = cooldown;
        
    }
}
