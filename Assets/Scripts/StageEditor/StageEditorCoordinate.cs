using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StageEditorCoordinate : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridSize = 5;        // -5 ~ 5 까지 (총 11줄)
    public float cellSize = 1f;     // 각 칸의 간격
    public float yHeight = 0f;      // 고정 Y 위치

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawGrid();
    }

    void DrawGrid()
    {
        int linesPerAxis = gridSize * 2 + 1;     // X축과 Z축 각각
        int totalLines = linesPerAxis * 2;       // X방향 선 + Z방향 선
        int totalPoints = totalLines * 2;        // 각 선당 시작/끝 점 2개씩

        lineRenderer.positionCount = totalPoints;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = false;

        int index = 0;

        // X 방향 라인 (Z는 고정, X는 이동)
        for (int i = -gridSize; i <= gridSize; i++)
        {
            float z = i * cellSize;
            Vector3 start = new Vector3(-gridSize * cellSize, yHeight, z);
            Vector3 end = new Vector3(gridSize * cellSize, yHeight, z);

            lineRenderer.SetPosition(index++, start);
            lineRenderer.SetPosition(index++, end);
        }

        // Z 방향 라인 (X는 고정, Z는 이동)
        for (int i = -gridSize; i <= gridSize; i++)
        {
            float x = i * cellSize;
            Vector3 start = new Vector3(x, yHeight, -gridSize * cellSize);
            Vector3 end = new Vector3(x, yHeight, gridSize * cellSize);

            lineRenderer.SetPosition(index++, start);
            lineRenderer.SetPosition(index++, end);
        }
    }
}