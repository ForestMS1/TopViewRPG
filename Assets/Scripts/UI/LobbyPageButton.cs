using UnityEngine;
using UnityEngine.UI;

public class LobbyPageButton : MonoBehaviour
{
    public enum PageButtonType
    {
        Open,
        Back,
        Home,
    }

    private Button button;
    public UIPageType targetPage;
    public PageButtonType type;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( () => {
            switch(type)
            {
                case PageButtonType.Open:
                    LobbyUIManager.instance.OpenPage(targetPage, true);
                    break;
                case PageButtonType.Back:
                    LobbyUIManager.instance.GoBack();
                    break;
                case PageButtonType.Home:
                    LobbyUIManager.instance.GoHome();
                    break;
                default:
                    break;
            }
        });
    }
}