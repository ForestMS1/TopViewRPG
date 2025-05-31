using UnityEngine;

public class Player : Creature
{
    void Start()
    {
        HP = 100f;
        MP = 100f;
        ATK = 10f;
        EXP = 0f;
        DEF = 5f;
        IsDead = false;
    }
}
