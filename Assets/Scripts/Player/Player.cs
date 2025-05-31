using UnityEngine;

public class Player : Creature
{
    void Awake()
    {
        MaxHP = 100f;
        HP = MaxHP;
        MaxMP = 100f;
        MP = MaxMP;
        ATK = 10f;
        EXP = 0f;
        DEF = 5f;
        IsDead = false;
    }
}
