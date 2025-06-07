using UnityEngine;

[CreateAssetMenu(fileName = "ChapterObjectData", menuName = "Scriptable Objects/ChapterObjectData")]
public class ChapterObjectData : ScriptableObject
{
    public int chapterNum;
    public GameObject[] objects;
}
