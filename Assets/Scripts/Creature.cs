using Unity.VisualScripting;
using UnityEngine;

public class Creature : MonoBehaviour, IDamageable
{
    private float maxHp;
    private float maxMp;
    private float hp;
    private float mp;
    private float atk;
    private float defence;
    private float exp;
    private bool isDead;
    private bool isInvincible;
    
    [SerializeField]
    protected float attackRange = 1.5f;
    [SerializeField] 
    protected float attackCoolTime = 0.5f;
    
    protected float lastAttackTime;
    public float MaxHP{get => maxHp; set => maxHp=value; }
    public float MaxMP{get => maxMp; set => maxMp=value; }
    public float HP { get => hp; set => hp = value; }
    public float MP { get => mp; set => mp = value; }
    public float ATK { get => atk; set => atk = value; }
    public float DEF { get => defence; set => defence = value; }
    public float EXP { get => exp; set => exp = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    
    protected Animator animator;
    void Awake()
    {
        lastAttackTime = Time.time;
        animator = GetComponent<Animator>();
    }

    public virtual void OnDamage(float damage)
    {
        if (IsDead) return;
        
        HP -= damage;
        
        if(HP > 0)
            Debug.Log($"{gameObject.name}: {HP}");
        else
        {
            HP = 0;
            Die();
        }
    }

    public virtual void DoAttackHit<T>() where T : Creature
    {
        if (!CanAttack()) return;
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, attackRange);
        animator.SetTrigger("Attack");
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                T enemy = hit.gameObject.GetComponent<T>();
                if (enemy != null)
                {
                    Debug.Log($"{hit.name} hit!");
                    hit.GetComponent<T>().OnDamage(ATK);
                    lastAttackTime = Time.time;
                }
            }
        }
    }


    public bool CanAttack()
    {
        if (lastAttackTime + attackCoolTime <= Time.time)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Die()
    {
        IsDead = true;
        Debug.Log($"{gameObject.name} is dead!");
    }
    
    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }
    public bool IsInvincible()
    {
        return isInvincible;
    }
}
