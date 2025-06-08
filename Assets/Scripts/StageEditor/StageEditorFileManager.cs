using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StageEditorFileManager : MonoBehaviour
{
    public static StageEditorFileManager instance;

    void Awake( )
    {
        if( instance == null )
            instance = this;
        else
            Destroy( this );
    }
    
    public void CreateAndSaveStageData(int chapterNum, int stageNum, string stageName, List<StageObjectData> objects)
    {
        StageData newData = ScriptableObject.CreateInstance<StageData>();
        newData.stageNum = stageNum;
        newData.stageName = stageName;
        newData.ObjectDatas = objects.ToArray();

        string chapterFolder = $"Chapter{chapterNum}";
        string folderPath = $"Assets/Resources/StageDatas/{chapterFolder}";
        string filePath = $"{folderPath}/Stage_{stageNum}.asset";

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/StageDatas"))
            AssetDatabase.CreateFolder("Assets/Resources", "StageDatas");

        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets/Resources/StageDatas", chapterFolder);
        
        if (File.Exists(filePath))
            AssetDatabase.DeleteAsset(filePath);

        AssetDatabase.CreateAsset(newData, filePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"StageData 저장 완료: {filePath}");
    }
    
    public Dictionary<int, StageData[]> LoadAllStageData()
    {
        Dictionary<int, StageData[]> result = new Dictionary<int, StageData[]>();
        StageData[] allStageData = Resources.LoadAll<StageData>("StageDatas");

        Dictionary<int, List<StageData>> temp = new Dictionary<int, List<StageData>>();

        foreach (var data in allStageData)
        {
            string path = AssetDatabase.GetAssetPath(data);
            string[] parts = path.Split('/');

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("Chapter") && int.TryParse(parts[i].Substring(7), out int chapterNum))
                {
                    if (!temp.ContainsKey(chapterNum))
                        temp[chapterNum] = new List<StageData>();
                    temp[chapterNum].Add(data);
                    break;
                }
            }
        }

        foreach (var pair in temp)
            result[pair.Key] = pair.Value.ToArray();

        return result;
    }
}