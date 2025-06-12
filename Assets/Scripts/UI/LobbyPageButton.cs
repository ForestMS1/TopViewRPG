using UnityEngine;
using UnityEngine.UI;

public class LobbyPageButton : MonoBehaviour
{
    private Button button;
    public UIPageType targetPage;
    public bool isOpen;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( () => {
            LobbyUIManager.instance.OpenPage(targetPage, isOpen);
        });
    }
}