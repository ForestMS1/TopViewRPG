using TMPro;
using UnityEngine;

public class PlayerStateText : MonoBehaviour
{
    [SerializeField]
    private PlayerFSM playerFSM;
    private TextMeshProUGUI playerStateText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStateText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerStateText.text = playerFSM.GetState().ToString();
    }
}
