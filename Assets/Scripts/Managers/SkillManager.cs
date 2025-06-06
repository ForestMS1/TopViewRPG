using System.Collections;
using System.Collections.Generic;
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
    private Button firstSkillButton;
    [SerializeField]
    private Button secondSkillButton;
    
    [Header("스킬 ScriptableObject")]
    [SerializeField]
    private SkillData dashSkillData;
    [SerializeField]
    private SkillData firstSkillData;
    [SerializeField]
    private SkillData secondSkillData;

    
    private List<Skill> skills; //컴포넌트로 붙은 객체가 가진 스킬 리스트

    void Awake()
    {
        actor = GetComponent<Creature>();
    }
    void Start()
    {
        //일단 느낌 잡음 현재는 하드코딩
        //TODO 좀 더 깔끔하게 짜기
        DashSkill dash = new DashSkill(actor);
        dash.Init(dashSkillData);
        skills = new List<Skill>();
        skills.Add(dash);
        //TODO 버튼리스트 만들어서 효율적으로 관리하기 
        dashButton.onClick.AddListener(delegate { skills[0].Activate(actor); });
        dashButton.image.sprite = dash.Icon;
    }


    void Update()
    {
        //사용한 스킬들 쿨타임 시간 감소
        foreach (Skill skill in skills)
        {
            if (skill.CurrentCool > 0) //현재 쿨타임 감소중이라면
            {
                skill.UpdateCooldown(Time.deltaTime);
            }
        }
    }
}
