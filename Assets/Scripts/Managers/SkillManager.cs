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
    
    [Header("스킬 컴포넌트")]
    [SerializeField]
    private DashSkill dashSkill;
    [SerializeField]
    private SpecialSkill specialSkill;
    [SerializeField]
    private UltimateSkill ultimateSkill;
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
    

    void Awake()
    {
        actor = GetComponent<Creature>();
        
        dashSkill = GetComponent<DashSkill>();
        specialSkill = GetComponent<SpecialSkill>();
        ultimateSkill = GetComponent<UltimateSkill>();
        Skill[] skillList = { dashSkill, specialSkill, ultimateSkill };

        for (int i = 0; i < skillList.Length; i++)
        {
            skillList[i].Init(skillDataArray[i]);
        }
        
    }
    void Start()
    {
        dashButton.onClick.AddListener(() => dashSkill.Activate(actor));
        specialSkillButton.onClick.AddListener(() => specialSkill.Activate(actor));
        ultimateSkillButton.onClick.AddListener(() => ultimateSkill.Activate(actor));
    }


    void Update()
    {
        
    }
    

    
    
}
