using System.Collections.Generic; // 리스트(List) 사용을 위해 필요
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    // 🎲 [Inspector에서 설정 가능한 변수들]
    public GameObject roadLinePrefab;  // 도로 줄무늬 프리팹
    public int lineCount = 30;         // 화면에 표시할 줄무늬 개수
    public float roadWidth = 6f;       // 도로의 폭
    public float moveSpeed = 5f;       // 도로가 위로 흐르는 속도

    // 📦 도로 줄무늬 오브젝트들을 저장할 리스트
    private List<GameObject> roadLines = new List<GameObject>();

    // 도로의 흐름을 위한 시간 누적 변수
    private float timeOffset = 0f;

    // ▶ 게임이 시작될 때 실행되는 함수
    void Start()
    {
        // 줄무늬를 lineCount 개수만큼 생성해서 리스트에 저장
        for (int i = 0; i < lineCount; i++)
        {
            GameObject line = Instantiate(roadLinePrefab, transform); // 생성하고 부모 설정
            roadLines.Add(line);
        }
    }

    // ▶ 매 프레임마다 실행되는 함수 (도로 줄무늬 위치 갱신)
    void Update()
    {
        // 시간에 따라 도로가 흐르도록 누적 값 증가
        timeOffset += Time.deltaTime * moveSpeed;

        // 줄무늬 각각에 대해 위치, 크기 계산
        for (int i = 0; i < roadLines.Count; i++)
        {
            // 📌 t는 0~1 사이의 값 (멀리 있을수록 작고, 가까울수록 큼)
            float t = ((float)i / roadLines.Count + timeOffset) % 1f;

            // 📍 Y 위치: 도로가 아래에서 위로 흐르도록 계산. Lerp값은 Camera Size에 비례
            float y = Mathf.Lerp(-5f, 4f, t);

            // 🔍 크기 (원근감): 가까울수록 큼, 멀수록 작음
            float scale = Mathf.Lerp(1.5f, 0.1f, t);

            // 🎯 줄무늬의 위치와 크기 적용
            GameObject line = roadLines[i];
            line.transform.localPosition = new Vector3(0, y, 0); // X=0으로 고정 
            line.transform.localScale = new Vector3(scale * roadWidth, scale, 1);
        }
    }
}