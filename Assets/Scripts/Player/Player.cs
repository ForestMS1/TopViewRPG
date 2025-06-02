using UnityEngine;

public class Player : Creature
{
    [SerializeField] 
    private PlayerFSM _playerFsm;
    private PlayerAnimator _playerAnimator;
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
        _playerFsm = GetComponent<PlayerFSM>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        _playerAnimator.SetAnimatorTrigger("GetHit");
        //_playerFsm.ChangeState(PlayerFSM.PlayerState.Damaged);
    }
    public override void Die()
    {
        base.Die();
        _playerAnimator.SetAnimatorTrigger("Die");
        //_playerFsm.ChangeState(PlayerFSM.PlayerState.Dead);
    }
}
