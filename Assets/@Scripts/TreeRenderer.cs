using System.Collections.Generic; // 여러 개의 나무를 담을 리스트를 쓰기 위해 필요
using UnityEngine;

public class TreeRenderer : MonoBehaviour
{
    // 🔧 인스펙터에서 직접 설정할 수 있는 변수들
    public GameObject treePrefab;       // 복제해서 사용할 나무 오브젝트
    public int treeCount = 30;          // 화면에 동시에 표시할 나무 수
    public float horizontalOffset = 1f; // 도로 가장자리에서 나무까지 거리
    public float roadWidth = 6f;        // 도로 폭 (나무 위치 계산에 필요)
    public float moveSpeed = 5f;        // 나무가 내려오는 속도
    public float densityFactor = 1f;    // 나무 간격 조절 (숫자가 클수록 더 텅텅 비게 됨)
    public bool isRightSide = true;     // true면 오른쪽 나무, false면 왼쪽 나무

    // 실제로 화면에 그려질 나무 오브젝트들을 담는 리스트
    private List<GameObject> trees = new List<GameObject>();
    private float timeOffset = 0f; // 시간이 흐를수록 나무가 아래로 내려오게 하기 위한 값

    void Start()
    {
        // 게임이 시작되면 나무 오브젝트를 미리 treeCount만큼 만들어서 리스트에 저장
        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(treePrefab, transform); // 프리팹을 복제해서 자식으로 생성
            trees.Add(tree); // 리스트에 넣기
        }
    }

    void Update()
    {
        // 매 프레임마다 나무가 아래로 조금씩 이동하도록 시간 값을 누적
        timeOffset += Time.deltaTime * moveSpeed;

        for (int i = 0; i < trees.Count; i++)
        {
            // 각 나무에 대해 화면 상의 위치 비율 구하기 (0 ~ 1)
            float linearT = ((float)i / treeCount + timeOffset) % 1f;

            // 원근 느낌을 주기 위해 pow 함수로 곡선 비율로 변경
            float t = Mathf.Pow(linearT, 1.5f * densityFactor);

            // y 위치는 위(멀리)에서 아래(가까이)로 내려오도록 보간
            float y = Mathf.Lerp(4f, -5f, t);

            // 나무의 크기도 t 값에 따라 작게~크게 보간 (멀리 작게, 가까이 크게)
            float scale = Mathf.Lerp(0.1f, 1.5f, t);

            // 도로의 현재 폭 계산 (scale 값 기반으로 도로 폭도 원근 적용)
            float roadEdge = (roadWidth * scale) / 2f;

            // 나무의 가로 위치 계산 (왼쪽이면 -, 오른쪽이면 + 방향)
            float x = (isRightSide ? 1 : -1) * (roadEdge + horizontalOffset);

            // 실제 나무 오브젝트에 위치와 크기 적용
            GameObject tree = trees[i];
            tree.transform.localPosition = new Vector3(x, y, 0);
            tree.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
