using UnityEngine;

public class JumpButton : MonoBehaviour
{
    [SerializeField]
    private PlayerFSM playerFSM;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    public void OnJumpButtonDown()
    {
        if (!playerFSM.IsControllable()) return;
        if (playerFSM.GetState() == PlayerFSM.PlayerState.Jump) return; //중복 점프 방지
        
        playerFSM.ChangeState(PlayerFSM.PlayerState.Jump);
    }
}
