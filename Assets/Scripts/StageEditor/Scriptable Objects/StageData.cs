using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public int stageNum;
    public string stageName;
    public StageObjectData[] ObjectDatas;
}

[System.Serializable]
public class StageObjectData
{
    public string objectID;
    public Vector3 position;
    public Quaternion rotation;
}