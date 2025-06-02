using System.Collections;
using UnityEngine;

public class Player : Creature
{
    [SerializeField] 
    private PlayerFSM _playerFsm;
    private PlayerAnimator _playerAnimator;
    
    [SerializeField]
    private float invincibleTime = 1f;

    private float lastDamagedTime;
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
        lastDamagedTime = Time.time;
    }

    public override void OnDamage(float damage)
    {
        if (lastDamagedTime + invincibleTime > Time.time) return; //피격 후 무적시간동안 무적
        base.OnDamage(damage);
        lastDamagedTime = Time.time;
        //_playerAnimator.SetAnimatorTrigger("GetHit");
        if (!IsDead)
        {
            _playerFsm.ChangeState(PlayerFSM.PlayerState.Damaged);
            StartCoroutine(nameof(OnDamageDelay));
        }
        else
        {
            _playerFsm.ChangeState(PlayerFSM.PlayerState.Dead); //데미지 받고 죽었을 때 플레이어의 마지막 상태가 Damaged로 되는 거 방지
        }
    }
    public override void Die()
    {
        base.Die();
        Debug.Log("PlayerDie호출됨!");
        _playerFsm.ChangeState(PlayerFSM.PlayerState.Dead);
    }

    IEnumerator OnDamageDelay()
    {
        yield return new WaitForSeconds(invincibleTime);
        _playerFsm.ChangeState(PlayerFSM.PlayerState.Idle);
    }
}
