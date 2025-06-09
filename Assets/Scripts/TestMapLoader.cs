using UnityEngine;

public class TestMapLoader : MonoBehaviour
{
    public StageData data;
    public int chapterNum;
    
    void Start()
    {
        LoadStageObjects( data, chapterNum );
    }
    
    public void LoadStageObjects(StageData stageData, int chapterNum)
    {
        foreach (StageObjectData objData in stageData.ObjectDatas)
        {
            string path = $"MapData/MapObjects/Chapter{chapterNum}/{objData.objectID}";
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
                continue;

            GameObject instance = Instantiate(prefab);
            instance.transform.localPosition = objData.position;
            instance.transform.localRotation = objData.rotation;
        }
    }
}
