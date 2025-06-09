using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    //스킬은 플레이어, 몬스터, 보스 등 모두가 사용할 수 있도록 설계
    [Header("기본 정보")]
    private string name;
    private string description;
    private float _maxMaxCoolDown;
    private float currentCooldown;
    private float mpCost;
    private bool isCoolDown;
    private Sprite icon;
    protected SkillData data;
    
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public float MaxCoolDown { get => _maxMaxCoolDown; set => _maxMaxCoolDown = value; }
    public float CurrentCool { get => currentCooldown; set => currentCooldown = value; }
    public float MpCost { get => mpCost; set => mpCost = value; }
    public bool IsCoolDown { get => isCoolDown; set => isCoolDown = value; }
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
        this.data = skillData;
        Name = skillData._name;
        Description = skillData._description;
        MaxCoolDown = skillData._cooldown;
        CurrentCool = 0f;
        MpCost = skillData._mpCost;
        Icon = skillData._icon;

        IsCoolDown = false;
        
        CastTime = skillData._castTime;
        Type = skillData._type;
        Targeting = skillData._targeting;
    }

    void Update()
    {
        //CoolDownImage.enabled = IsCoolDown;
    }

    public virtual void Activate(Creature actor)
    {
       StartCooldown();
       actor.MP -= MpCost;
    }

    public bool CanUse(Creature actor)
    {
        return (!IsCoolDown && actor.MP >= MpCost); //TODO MP양도 확인
    }
    
    private void StartCooldown()
    {
        if (IsCoolDown)
            return;

        IsCoolDown = true;
        CurrentCool = MaxCoolDown;

        IngameUIManager.instance.TriggerCooldownUI(Type, MaxCoolDown);
        StartCoroutine(OnCoolDownTime());
    }

    IEnumerator OnCoolDownTime()
    {
        while (CurrentCool > 0)
        {
            CurrentCool -= Time.deltaTime;
            yield return null;
        }

        IsCoolDown = false;
    }
}
