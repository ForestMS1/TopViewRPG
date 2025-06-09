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

    public static IngameUIManager instance;

    void Awake( )
    {
        if( instance == null )
            instance = this;
        else
            Destroy( this );
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
