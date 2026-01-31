using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 이벤트 트리거 사용을 위해 필수
using System.Collections.Generic;

public class PinHover : MonoBehaviour
{
    [Header("설정")]
    public Transform pinContainer;    // 핀들이 모여있는 부모 오브젝트 (Content)
    public float hoverScale = 1.2f;   // 커질 배율
    public float smoothTime = 10f;    // 애니메이션 속도

    // 핀 하나의 상태를 관리하기 위한 클래스 (내부용)
    private class PinData
    {
        public Transform transform;      // 핀 오브젝트
        public Image glowImage;          // 핀 안의 Glow 이미지
        public Vector3 defaultScale;     // 원래 크기
        public Vector3 targetScale;      // 목표 크기 (애니메이션용)
        public float targetAlpha;        // 목표 투명도 (애니메이션용)
    }

    private List<PinData> allPins = new List<PinData>();

    void Start()
    {
        // 1. pinContainer 아래에 있는 모든 핀을 찾아서 등록
        if (pinContainer == null)
        {
            Debug.LogError("Pin Container를 연결해주세요!");
            return;
        }

        foreach (Transform childPin in pinContainer)
        {
            // 핀 데이터 생성 및 초기화
            PinData newPin = new PinData();
            newPin.transform = childPin;
            newPin.defaultScale = childPin.localScale;
            newPin.targetScale = newPin.defaultScale;
            newPin.targetAlpha = 0f;

            // Glow 이미지 찾기 (자식들 중 이름에 "Glow"가 들어간 놈을 찾음)
            // 혹은 이름 상관없이 가장 첫 번째 자식 등 규칙에 따라 수정 가능
            foreach (Transform grandChild in childPin)
            {
                if (grandChild.name.Contains("Glow")) // 이름에 Glow가 포함된 오브젝트 찾기
                {
                    newPin.glowImage = grandChild.GetComponent<Image>();
                    if (newPin.glowImage != null)
                    {
                        // 시작할 때 투명하게 만들기
                        Color c = newPin.glowImage.color;
                        c.a = 0f;
                        newPin.glowImage.color = c;
                    }
                    break;
                }
            }

            // 2. 핀에 마우스 이벤트 감지 기능(EventTrigger)을 동적으로 추가
            AddEventTrigger(childPin.gameObject, newPin);

            // 리스트에 추가
            allPins.Add(newPin);
        }
    }

    void Update()
    {
        // 3. 등록된 모든 핀의 애니메이션을 한곳에서 일괄 처리
        foreach (PinData pin in allPins)
        {
            // 크기 변경 (Lerp)
            pin.transform.localScale = Vector3.Lerp(pin.transform.localScale, pin.targetScale, Time.deltaTime * smoothTime);

            // 빛(Glow) 투명도 변경
            if (pin.glowImage != null)
            {
                Color c = pin.glowImage.color;
                c.a = Mathf.Lerp(c.a, pin.targetAlpha, Time.deltaTime * smoothTime);
                pin.glowImage.color = c;
            }
        }
    }

    // 마우스 이벤트를 코드로 직접 심어주는 함수
    void AddEventTrigger(GameObject obj, PinData pinData)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null) trigger = obj.AddComponent<EventTrigger>();

        // Enter 이벤트 (마우스 올렸을 때)
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPinEnter(pinData); });
        trigger.triggers.Add(entryEnter);

        // Exit 이벤트 (마우스 나갔을 때)
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPinExit(pinData); });
        trigger.triggers.Add(entryExit);
    }

    // --- 실제 동작 로직 ---

    void OnPinEnter(PinData pin)
    {
        pin.targetScale = pin.defaultScale * hoverScale; // 커져라
        pin.targetAlpha = 1f;                            // 빛나라
    }

    void OnPinExit(PinData pin)
    {
        pin.targetScale = pin.defaultScale;              // 작아져라
        pin.targetAlpha = 0f;                            // 꺼져라
    }
}