using UnityEngine;

[CreateAssetMenu(fileName = "NewSlimeData", menuName = "Monster/SlimeData")]
public class SlimeData : ScriptableObject
{
    public float attackRange;
    public float attackCoolTime;
    public float MaxHP;
    public float MaxMP;
    public float ATK;
    public float DEF;
    public float EXP;
    
    
    public string slimeName;
    public float moveSpeed;
    public enum SlimeType { Normal, Fire, Ice, Poison }
    public SlimeType slimeType;

    [TextArea]
    public string description;
}
