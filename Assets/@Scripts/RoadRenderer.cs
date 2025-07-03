using System.Collections.Generic;
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    public GameObject roadLinePrefab;
    public int lineCount = 30;
    public float roadWidth = 6f;
    public float moveSpeed = 5f;

    private List<GameObject> roadLines = new List<GameObject>();
    private float spacing = 0.3f;
    private float timeOffset = 0f; //누적시켜서 각 줄의 t값에 적용해 움직이는 효과 만듦

    void Start()
    {
        for (int i = 0; i < lineCount; i++)
        {
            GameObject line = Instantiate(roadLinePrefab, transform);
            roadLines.Add(line);
        }
    }

    void Update()
    {
        timeOffset += Time.deltaTime * moveSpeed;

        for (int i = 0; i < roadLines.Count; i++)
        {
            float t = ((float)i / roadLines.Count + timeOffset) % 1f;

            // Y 위치: 아래서 위로 흐르게
            float y = Mathf.Lerp(-5f, 5f, t);

            // 크기(스케일): 원근감 있게
            float scale = Mathf.Lerp(1.5f, 0.1f, 1 - t);

            GameObject line = roadLines[i];
            line.transform.localPosition = new Vector3(0, y, 0); // X = 0으로 고정 (곡선 제거)
            line.transform.localScale = new Vector3(scale * roadWidth, scale, 1);
        }
    }
}