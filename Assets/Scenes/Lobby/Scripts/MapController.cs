using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IScrollHandler
{
    [Header("연결 필수")]
    public RectTransform content; // 지도가 들어있는 Content 오브젝트

    [Header("줌 설정")]
    public float zoomSpeed = 0.1f;
    public float minZoom = 1.0f; // 1배율 이하로 축소 안 되게 설정
    public float maxZoom = 5.0f;

    public void OnScroll(PointerEventData eventData)
    {
        // 1. 현재 스케일 가져오기
        Vector3 newScale = content.localScale;

        // 2. 휠을 굴린 만큼 스케일 조절 (zoomSpeed 곱하기)
        float scrollDelta = eventData.scrollDelta.y * zoomSpeed;

        // 3. 확대/축소 적용
        newScale.x += scrollDelta;
        newScale.y += scrollDelta;

        // 4. 최소/최대값 제한 (Clamp)
        newScale.x = Mathf.Clamp(newScale.x, minZoom, maxZoom);
        newScale.y = Mathf.Clamp(newScale.y, minZoom, maxZoom);

        // 5. 최종 적용 (이때 피벗(중심)에 따라 확대되는 위치가 달라짐)
        content.localScale = newScale;
    }
}