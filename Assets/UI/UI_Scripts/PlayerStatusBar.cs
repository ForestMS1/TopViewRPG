using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    
    private enum PlayerBar
    {
        Hp,
        Mp
        //EXP
    }
    
    [SerializeField]
    private PlayerBar pb;
    private Slider slider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        
        switch (pb)
        {
            case PlayerBar.Hp:
                slider.maxValue = _player.MaxHP;
                slider.value = _player.HP;
                break;
            case PlayerBar.Mp:
                slider.maxValue = _player.MaxMP;
                slider.value = _player.MP;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (pb)
        {
            case PlayerBar.Hp:
                slider.value = Mathf.Lerp(slider.value, _player.HP, Time.deltaTime * 10f);
                break;
            case PlayerBar.Mp:
                slider.value = Mathf.Lerp(slider.value, _player.MP, Time.deltaTime * 10f);
                break;
        }
    }
}
