using System.Collections.Generic;
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    public GameObject roadLinePrefab;
    public int lineCount = 30;
    public float roadWidth = 6f;
    public float curveStrength = 2f;

    private List<GameObject> roadLines = new List<GameObject>();

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
        for (int i = 0; i < roadLines.Count; i++)
        {
            float t = (float)i / roadLines.Count;
            float y = Mathf.Lerp(-4f, 4f, t);
            float scale = Mathf.Lerp(1.5f, 0.1f, t);
            float curve = Mathf.Sin(Time.time * 0.5f) * curveStrength * t;

            GameObject line = roadLines[i];
            line.transform.localPosition = new Vector3(curve, y, 0);
            line.transform.localScale = new Vector3(scale * roadWidth, scale, 1);
        }
    }
}
