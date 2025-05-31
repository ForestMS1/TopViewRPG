using UnityEngine;

public class Enemy : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        MaxHP = 100f;
        HP = MaxHP;
        MaxMP = 50f;
        MP = MaxMP;
        ATK = 10f;
        DEF = 3f;
        EXP = 3f;
        IsDead = false;
    }
}
