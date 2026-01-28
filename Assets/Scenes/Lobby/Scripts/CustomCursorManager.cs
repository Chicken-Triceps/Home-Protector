using UnityEngine;

public class CustomCursorManager : MonoBehaviour
{
    [Header("기본 커서 설정")]
    public Texture2D defaultCursorTexture;
    public Vector2 hotSpot = new Vector2(16, 16);

    [Header("클릭 이펙트 설정")]
    public GameObject ripplePrefab; // 물결 프리팹 넣을 곳
    public Canvas parentCanvas;     // 캔버스 넣을 곳 (UI 위에 그려야 하므로)

    void Start()
    {
        Cursor.SetCursor(defaultCursorTexture, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 순간 (0: 좌클릭, 1: 우클릭, 2: 휠클릭)
        if (Input.GetMouseButtonDown(0))
        {
            SpawnRippleEffect();
        }
    }

    void SpawnRippleEffect()
    {
        // 1. 프리팹을 캔버스의 자식으로 생성
        GameObject ripple = Instantiate(ripplePrefab, parentCanvas.transform);

        // 2. 마우스 위치를 UI 좌표로 변환하여 이펙트 위치 설정 (핵심!)
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, // 기준이 될 캔버스 RectTransform
            Input.mousePosition,                     // 현재 마우스 화면 좌표
            parentCanvas.worldCamera,                // 캔버스가 사용하는 카메라 (Overlay 모드면 null)
            out localPoint);                         // 변환된 좌표를 받을 변수

        // 3. 위치 적용
        ripple.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }
}