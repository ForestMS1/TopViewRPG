using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class StageEditor : MonoBehaviour
{
    [Header("Data")]
    public List<ChapterObjectData> chapterObjectDatas;

    [Header("UI Objects")]
    public Transform objectScrollViewContent;
    public GameObject objectScrollViewPrefab;
    public TMP_Text currentObjectNameText;
    public TMP_Text currentObjectPosText;
    public TMP_Text currentObjectRotText;

    [Header("Camera")]
    public Camera mainCam;

    [Header("Variables")] 
    public static StageEditor instance;
    public GameObject selectedObject;
    public List<StageObjectData> currentMapObjects = new List<StageObjectData>();
    private List<Outline> currentOutlines = new List<Outline>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        LoadChapterObjectDatas();
        DisplayChapterObjects();

        if (mainCam == null)
            mainCam = Camera.main;
    }

    void Update()
    {
        UpdateRaycastOutline();
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
            item.GetComponent<StageEditorObjectButton>().Init(obj);
            Image image = item.GetComponent<Image>();
            if (image != null)
                image.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"썸네일 생성 실패: {obj.name}");
        }
    }

    public void SelectObject(GameObject target)
    {
        if (selectedObject != null)
            selectedObject.GetComponent<StageEditorObjectButton>().selected.SetActive(false);

        selectedObject = target;
        selectedObject.GetComponent<StageEditorObjectButton>().selected.SetActive(true);
    }

    private void UpdateRaycastOutline()
    {
        if (mainCam == null) return;

        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            GameObject rootObj = hit.collider.transform.root.gameObject;
            Outline[] newOutlines = rootObj.GetComponentsInChildren<Outline>(true);

            if (!IsSameOutlines(newOutlines, currentOutlines))
            {
                SetOutlinesEnabled(currentOutlines, false);
                currentOutlines = new List<Outline>(newOutlines);
                SetOutlinesEnabled(currentOutlines, true);
            }

            currentObjectNameText.text = rootObj.name;
            currentObjectPosText.text = $"Pos: {rootObj.transform.position:F2}";
            currentObjectRotText.text = $"Rot: {rootObj.transform.eulerAngles:F1}";
        }
        else
        {
            SetOutlinesEnabled(currentOutlines, false);
            currentOutlines.Clear();

            currentObjectNameText.text = "";
            currentObjectPosText.text = "";
            currentObjectRotText.text = "";
        }
    }


    public void PlaceObjectOnRightClick()
    {
        if (selectedObject == null || mainCam == null)
            return;

        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            Vector3 placePos = hit.collider.transform.position + hit.normal;
            Vector3Int gridPos = Vector3Int.RoundToInt(placePos);

            GameObject instance = Instantiate(selectedObject.GetComponent<StageEditorObjectButton>(  ).targetObject);
            instance.transform.position = gridPos;
            instance.transform.rotation = Quaternion.identity;

            StageObjectData data = new StageObjectData
            {
                objectID = selectedObject.GetComponent<StageEditorObjectButton>(  ).targetObject.name,
                position = gridPos,
                rotation = Quaternion.identity
            };

            currentMapObjects.Add(data);
        }
    }
    
    public void DeleteObjectOnLeftClick()
    {
        if (mainCam == null)
            return;

        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            GameObject targetRoot = hit.collider.transform.root.gameObject;

            // currentMapObjects에서 위치 일치 항목 제거
            Vector3Int pos = Vector3Int.RoundToInt(targetRoot.transform.position);
            currentMapObjects.RemoveAll(data => data.position == pos);

            Destroy(targetRoot);
            currentOutlines.Clear();
            Debug.Log($"오브젝트 제거됨: {targetRoot.name}");
        }
    }


    private void SetOutlinesEnabled(List<Outline> outlines, bool enabled)
    {
        foreach (var outline in outlines)
        {
            if (outline != null)
                outline.enabled = enabled;
        }
    }

    private bool IsSameOutlines(Outline[] a, List<Outline> b)
    {
        if (a.Length != b.Count) return false;
        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i])
                return false;
        return true;
    }
}