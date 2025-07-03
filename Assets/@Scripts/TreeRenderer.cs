using System.Collections.Generic;
using UnityEngine;

public class TreeRenderer : MonoBehaviour
{
    public GameObject treePrefab;
    public int treeCount = 30;
    public float horizontalOffset = 5f;
    public float roadWidth = 6f;
    public float edgeOffset = 1f;
    public float moveSpeed = 5f;
    public bool isRightSide = true;

    private List<GameObject> trees = new List<GameObject>();
    private float timeOffset = 0f;

    void Start()
    {
        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(treePrefab, transform);
            trees.Add(tree);
        }
    }

    void Update()
    {
        timeOffset += Time.deltaTime * moveSpeed;

        for (int i = 0; i < trees.Count; i++)
        {
            float linearT = ((float)i / treeCount + timeOffset) % 1f;

            // ✅ Apply nonlinear spacing for perspective
            float t = Mathf.Pow(linearT, 2f);  // Makes distant trees closer together

            float y = Mathf.Lerp(5f, -5f, t);
            float scale = Mathf.Lerp(0.1f, 1.5f, t);

            float roadEdge = (roadWidth * scale) / 2f;
            float x = (isRightSide ? 1 : -1) * (roadEdge + edgeOffset);

            GameObject tree = trees[i];
            tree.transform.localPosition = new Vector3(x, y, 0);
            tree.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}