using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    [SerializeField]
    private PlayerFSM playerFSM;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    public void OnAttackButtonDown()
    {
        if (!playerFSM.IsControllable()) return;
        
        playerFSM.ChangeState(PlayerFSM.PlayerState.Attack);
    }
}
