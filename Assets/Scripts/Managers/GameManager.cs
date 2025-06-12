using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Collected Items")]
    public int collectedCoin = 0;

    public static GameManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void AddCoin()
    {
        collectedCoin++;
        IngameUIManager.instance.SetCoinText(collectedCoin);
    }
}