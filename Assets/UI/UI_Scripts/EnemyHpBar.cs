using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    private Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Enemy의 체력 값은 Awake에서 불러와야 값이 제대로 불러와짐
        enemy = GetComponent<Enemy>();
        hpSlider.maxValue = enemy.MaxHP;
        hpSlider.minValue = 0;
        hpSlider.value = enemy.HP;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = enemy.HP;

        hpSlider.value = Mathf.Lerp(hpSlider.value, enemy.HP, Time.deltaTime * 10f);
        // 항상 카메라를 향하게
        //hpSlider.transform.LookAt(Camera.main.transform);
        //hpSlider.transform.Rotate(0, 180, 0); // 뒤집기
    }
}
