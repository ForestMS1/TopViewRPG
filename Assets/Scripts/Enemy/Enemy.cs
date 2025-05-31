using UnityEngine;

public class Enemy : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = 100f;
        MP = 50f;
        ATK = 10f;
        DEF = 3f;
        EXP = 3f;
        IsDead = false;
    }
}
