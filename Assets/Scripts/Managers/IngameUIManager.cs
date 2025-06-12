using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class IngameUIManager : MonoBehaviour
{
    [Header( "UI Objects" )]
    public Image dashCooldownImage;
    public Image specialCooldownImage;
    public Image ultimateCooldownImage;

    public Slider playerHPBarSlider;
    public TMP_Text playerHPBarText;

    public TMP_Text coinText;

    public static IngameUIManager instance;

    void Awake( )
    {
        if( instance == null )
            instance = this;
        else
            Destroy( this );
    }

    public void SetCoinText(int value)
    {
        coinText.text = value.ToString();
        coinText.rectTransform.DOKill();
        coinText.rectTransform.localScale = Vector3.one;
        coinText.rectTransform
                .DOScale(Vector3.one * 1.2f, 0.1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    coinText.rectTransform
                            .DOScale(Vector3.one, 0.3f)
                            .SetEase(Ease.InQuad);
                });
    }

    public void SetPlayerHPBar(float value, float maxValue)
    {
        float normalized = Mathf.Clamp01(value / maxValue);
        playerHPBarText.text = Mathf.FloorToInt(value).ToString();
        playerHPBarSlider.DOKill(); 
        playerHPBarSlider.DOValue(normalized, 0.3f).SetEase(Ease.OutQuad);
    }
    
    public void TriggerCooldownUI(SkillData.SkillType type, float cooldownTime)
    {
        Debug.Log( type );
        
        Image targetImage = GetCooldownImageByType(type);
        if (targetImage == null)
        {
            Debug.LogWarning($"[UI] 쿨타임 이미지 없음: {type}");
            return;
        }

        TMP_Text timeText = targetImage.GetComponentInChildren<TMP_Text>();
        if (timeText == null)
        {
            Debug.LogWarning($"[UI] 쿨타임 텍스트 없음: {type}");
            return;
        }

        targetImage.gameObject.SetActive(true);
        targetImage.fillAmount = 1f;
        timeText.text = Mathf.CeilToInt(cooldownTime).ToString();

        DOTween.To(() => targetImage.fillAmount,
                x => targetImage.fillAmount = x,
                0f,
                cooldownTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                targetImage.fillAmount = 1f;
                targetImage.gameObject.SetActive(false);
            });

        float remain = cooldownTime;
        DOTween.To(() => remain, x => {
                remain = x;
                timeText.text = Mathf.CeilToInt(remain).ToString();
            }, 0f, cooldownTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                timeText.text = "";
            });
    }


    private Image GetCooldownImageByType(SkillData.SkillType type)
    {
        return type switch
        {
            SkillData.SkillType.Dash => dashCooldownImage,
            SkillData.SkillType.Special => specialCooldownImage,
            SkillData.SkillType.Ultimate => ultimateCooldownImage,
            _ => null
        };
    }
}
