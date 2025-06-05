using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //SkillManager는 스킬을 가진 플레이어나 몬스터에게 컴포넌트로 붙인다.
    [SerializeField]
    private Creature actor;
    [SerializeField]
    private Button dashButton;
    private List<Skill> skills; //컴포넌트로 붙은 객체가 가진 스킬 리스트

    void Start()
    {
        //일단 느낌 잡음
        //TODO 좀 더 깔끔하게 짜기
        actor = actor.GetComponent<Creature>();
        DashSkill dash = new DashSkill();
        skills = new List<Skill>();
        skills.Add(dash);
        
        dashButton.onClick.AddListener(delegate { dash.Activate(actor); });

    }


    void Update()
    {
        
    }
}
