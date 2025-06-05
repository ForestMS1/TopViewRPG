using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Skill/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("기본 정보")]
    public string _name;
    [TextArea]
    public string _description;
    public float _cooldown;
    public float _currentCooldown;
    public float _mpCost;
    public Sprite _icon;

    [Header("작동 관련")] 
    public float _castTime; //시전 시간
    //private bool isUnlocked; //습득 여부
    public SkillType _type;
    public SkillTargetingType _targeting;

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
}
