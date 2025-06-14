using UnityEngine;

public class CreamSoda : Player
{

    private int currentCombo = 0;
    
    [SerializeField]
    private float nextComboTime; // 공격 이후 nextComboTime 내에 공격 키를 눌러야 다음 콤보공격 작동

    void Awake()
    {
        animator = GetComponent<Animator>();
        InitAwake();
    }

    void Start()
    {
        InitStart();
    }
    public override void DoAttackHit<Enemy>()
    {
        if (!CanAttack()) return;

        if (lastAttackTime + nextComboTime < Time.time)
            currentCombo = 0;

        if (currentCombo >= 1 && Time.time < lastAttackTime + nextComboTime)
        {
            //다음 콤보 공격
            Attack(currentCombo);
        }
        else
        {
            //첫 타
            Attack();   
        }
    }

    void Attack(int curCom = 0)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, attackRange); //음 .. 구체 말고 다른 범위로 하는게 좋을듯?
        Debug.Log($"Attack{curCom}");
        animator.SetTrigger($"Attack{curCom}");
        
        //TODO 콤보에 따른 데미지 계산 방식 변경
        float damage = ATK * (curCom+1); 
        
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log($"{hit.name} hit!");
                    hit.GetComponent<Enemy>().OnDamage(damage);
                }
            }
        }
        lastAttackTime = Time.time;
        currentCombo = ++curCom;
        if (curCom > 2)
            currentCombo = 0;
    }
    
}
