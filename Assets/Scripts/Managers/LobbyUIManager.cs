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

        OpenPage(UIPageType.Home);
    }

    public void OpenPage(UIPageType type)
    {
        if (currentPage != null)
            pages[currentPage].SetActive(false);

        pages[type].SetActive(true);
        currentPage = type;
    }
}