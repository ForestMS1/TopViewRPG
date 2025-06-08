using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //SkillManager는 스킬을 가진 플레이어나 몬스터에게 컴포넌트로 붙인다.
    [SerializeField]
    private Creature actor;
    
    [Header("스킬 버튼")]
    [SerializeField]
    private Button dashButton;
    [SerializeField]
    private Button specialSkillButton;
    [SerializeField]
    private Button ultimateSkillButton;
    
    [Header("스킬 ScriptableObject")]
    [SerializeField] 
    private SkillData[] skillDataArray;

    private Dictionary<SkillData.SkillType, Button> skillButtons;
    private Dictionary<SkillData.SkillType, SkillData> skillDataMap;
    private Dictionary<SkillData.SkillType, Skill> skills;

    void Awake()
    {
        actor = GetComponent<Creature>();
        
        // SkillType별 버튼 등록
        skillButtons = new Dictionary<SkillData.SkillType, Button>
        {
            { SkillData.SkillType.Dash, dashButton },
            { SkillData.SkillType.Special, specialSkillButton },
            { SkillData.SkillType.Ultimate, ultimateSkillButton }
        };
        
        // SkillData 매핑
        skillDataMap = skillDataArray.ToDictionary(data => data._type, data => data);
        
        // Skill 컴포넌트 매핑
        skills = new Dictionary<SkillData.SkillType, Skill>();
        foreach (var skill in GetComponents<Skill>())
        {
            if (skillDataMap.ContainsKey(skill.Type))
            {
                skills[skill.Type] = skill;
                skill.Init(skillDataMap[skill.Type]);
            }
        }
    }
    void Start()
    {
        foreach (var pair in skillButtons)
        {
            SkillData.SkillType type = pair.Key;
            Button btn = pair.Value;

            if (skills.ContainsKey(type))
            {
                btn.onClick.AddListener(() => skills[type].Activate(actor));
            }
        }
    }


    void Update()
    {
        
    }
    

    
    
}
