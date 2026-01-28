using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LobbyManager : MonoBehaviour
{
    [Header("패널 연결")]
    public GameObject exitPanel;      // 종료 패널
    public GameObject startPanel;     // 지도(맵) 패널
    public GameObject houseInfoPanel; // [추가] 집 정보 패널 (HouseInfoPanel)

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 1순위: 집 정보가 떠 있다면 -> 집 정보를 닫는다 (지도로 돌아감)
            if (houseInfoPanel != null && houseInfoPanel.activeSelf)
            {
                CloseHouseInfoPanel();
            }
            // 2순위: 종료 패널이 떠 있다면 -> 종료 패널을 닫는다
            else if (exitPanel.activeSelf)
            {
                CloseExitPanel();
            }
            // 3순위: 지도(시작) 패널이 떠 있다면 -> 지도를 닫는다 (메인화면으로)
            else if (startPanel != null && startPanel.activeSelf)
            {
                CloseStartPanel();
            }
            // (선택 사항) 4순위: 아무것도 없으면 -> 종료 패널을 띄운다?
            // 필요하면 여기에 OpenExitPanel(); 추가
        }
    }

    // --- 집 정보 패널 닫기 ---
    public void CloseHouseInfoPanel()
    {
        if (houseInfoPanel != null) houseInfoPanel.SetActive(false);
    }

    // --- 기존 기능들 ---
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void OpenStartPanel()
    {
        if (startPanel != null) startPanel.SetActive(true);
    }

    public void CloseStartPanel()
    {
        if (startPanel != null) startPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}