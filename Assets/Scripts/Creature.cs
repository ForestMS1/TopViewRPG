using UnityEngine;

public class Creature : MonoBehaviour, IDamageable
{
    private float hp;
    private float mp;
    private float atk;
    private float defence;
    private float exp;
    public float HP { get => hp; set => hp = value; }
    public float MP { get => mp; set => mp = value; }
    public float ATK { get => atk; set => atk = value; }
    public float DEF { get => defence; set => defence = value; }
    public float EXP { get => exp; set => exp = value; }

    public void OnDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
        }
    }
}
