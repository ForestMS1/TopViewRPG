using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;

public enum UIPageType
{
    Home,
    Profile,
    Event,
    MyCookies,
    Inventory,
}

public class LobbyUIManager : MonoBehaviour
{
    public Transform pageTop;
    public GameObject[] pageObjects;
    public Dictionary<UIPageType, GameObject> pages = new();
    public UIPageType currentPage;

    private Stack<UIPageType> pageHistory = new();

    public static LobbyUIManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        pageObjects = new GameObject[pageTop.childCount];
        for(int i = 0; i < pageTop.childCount; i++)
        {
            pageObjects[i] = pageTop.GetChild(i).gameObject;
            pageObjects[i].SetActive(false);
        }

        for(int i = 0; i < pageObjects.Length; i++)
            pages[(UIPageType)i] = pageObjects[i];

        OpenPage(UIPageType.Home, false);
    }

    public void OpenPage(UIPageType type, bool addToHistory = true)
    {
        if (currentPage != type)
        {
            if (addToHistory)
                pageHistory.Push(currentPage);

            if (pages.ContainsKey(currentPage))
                pages[currentPage].SetActive(false);

            pages[type].SetActive(true);
            currentPage = type;
        }
    }

    public void GoBack()
    {
        if (pageHistory.Count > 0)
        {
            UIPageType previousPage = pageHistory.Pop();
            OpenPage(previousPage, false);
        }
    }
}