using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageEditor : MonoBehaviour
{
    [Header("Data")]
    public List<ChapterObjectData> chapterObjectDatas;

    [Header("UI Objects")]
    public Transform objectScrollViewContent;
    public GameObject objectScrollViewPrefab;

    [Header( "Variables" )]
    public static StageEditor instance;
    public GameObject selectedObject;

    private void Awake( )
    {
        if( instance == null )
            instance = this;
        else
            Destroy( this );
    }

    void Start()
    {
        LoadChapterObjectDatas();
        DisplayChapterObjects();
    }

    public void LoadChapterObjectDatas()
    {
        chapterObjectDatas = new List<ChapterObjectData>();
        ChapterObjectData[] chapters = Resources.LoadAll<ChapterObjectData>("MapData/ChapterObjectDatas");

        chapterObjectDatas.Clear();
        chapterObjectDatas.AddRange(chapters);
    }

    public void DisplayChapterObjects()
    {
        if (chapterObjectDatas == null || chapterObjectDatas.Count == 0)
        {
            Debug.LogWarning("ChapterObjectData가 없습니다.");
            return;
        }

        ChapterObjectData chapter = chapterObjectDatas[0];

        foreach (GameObject obj in chapter.objects)
            StartCoroutine(LoadAndDisplayPreview(obj));
    }

    IEnumerator LoadAndDisplayPreview(GameObject obj)
    {
        int id = obj.GetInstanceID();

        while (AssetPreview.IsLoadingAssetPreview(id))
            yield return null;
        
        Texture2D preview = AssetPreview.GetAssetPreview(obj);
        
        int retry = 0;
        while (preview == null && retry < 20)
        {
            yield return new WaitForSeconds(0.1f);
            preview = AssetPreview.GetAssetPreview(obj);
            retry++;
        }

        if (preview != null)
        {
            Sprite sprite = Sprite.Create(preview,
                new Rect(0, 0, preview.width, preview.height),
                new Vector2(0.5f, 0.5f));

            GameObject item = Instantiate(objectScrollViewPrefab, objectScrollViewContent);
            item.GetComponent<StageEditorObjectButton>( ).targetObject = obj;
            Image image = item.GetComponent<Image>();
            if (image != null)
                image.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"썸네일 생성 실패: {obj.name}");
        }
    }

    public void SelectObject( GameObject target )
    {
        if(selectedObject != null)
            selectedObject.GetComponent<StageEditorObjectButton>(  ).selected.SetActive( false );
        
        selectedObject = target;
        selectedObject.GetComponent<StageEditorObjectButton>(  ).selected.SetActive( true );
    }
}