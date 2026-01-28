using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 필수

public class MapPin : MonoBehaviour
{
    [Header("이 집에 대한 정보")]
    public string houseName;      // 집 이름 (예: 윤하우스)
    public string address;        // 주소
    public Sprite houseImage;     // 집 사진

    [Header("연결할 UI 패널 (하나만 연결하면 됨)")]
    public GameObject infoPanel;        // HouseInfoPanel 오브젝트
    public TextMeshProUGUI titleText;   // 패널 안의 제목 텍스트
    public TextMeshProUGUI addressText; // 패널 안의 주소 텍스트
    public Image photoImage;            // 패널 안의 사진 이미지

    // 핀을 클릭했을 때 실행할 함수
    public void OnPinClick()
    {
        // 1. 패널에 내 정보를 입력한다.
        titleText.text = houseName;
        addressText.text = address;

        if (houseImage != null)
        {
            photoImage.sprite = houseImage;
            photoImage.gameObject.SetActive(true);
        }
        else
        {
            // 사진이 없으면 이미지 끄기
            photoImage.gameObject.SetActive(false);
        }

        // 2. 패널을 켠다.
        infoPanel.SetActive(true);
    }
}