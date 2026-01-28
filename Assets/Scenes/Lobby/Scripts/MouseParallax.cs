using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    [Header("설정")]
    [Tooltip("배경이 움직이는 최대 거리 (클수록 많이 움직임)")]
    public float moveAmount = 30f;

    [Tooltip("움직임의 부드러움 정도 (높을수록 빠릿함, 낮을수록 부드러움)")]
    public float smoothSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        // 게임 시작 시, 배경의 원래 위치를 기억해둡니다.
        startPos = transform.position;
    }

    void Update()
    {
        // 1. 마우스의 화면상 위치를 구합니다. (0 ~ 1 사이 값으로 정규화)
        // (0.5, 0.5)가 화면 정중앙이 되도록 보정합니다.
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        // 2. 목표 위치 계산
        // 마우스가 오른쪽으로 가면(+) 배경은 왼쪽(-)으로 가야 깊이감이 느껴집니다.
        // 그래서 -1을 곱해 반대로 움직이게 합니다.
        Vector3 targetPos = new Vector3(
            startPos.x + (mouseX * moveAmount * -1),
            startPos.y + (mouseY * moveAmount * -1),
            startPos.z
        );

        // 3. 현재 위치에서 목표 위치까지 부드럽게(Lerp) 이동
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
    }
}