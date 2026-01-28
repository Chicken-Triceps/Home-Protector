using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // 에디터에서 종료 기능을 테스트하기 위해 필수
#endif

public class LobbyManager : MonoBehaviour
{
    [Header("패널 연결")]
    public GameObject exitPanel;  // 종료 패널 (ExitPanel)
    public GameObject startPanel; // 시작 버튼 눌렀을 때 뜨는 패널

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 만약 종료 패널이 켜져 있다면 -> 끈다
            if (exitPanel.activeSelf)
            {
                CloseExitPanel();
            }
            // 만약 시작 패널이 켜져 있다면 -> 끈다
            else if (startPanel != null && startPanel.activeSelf)
            {
                CloseStartPanel();
            }
        }
    }

    // --- 종료 패널 관련 기능 ---
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void CloseExitPanel() // No 버튼, ESC에 연결
    {
        exitPanel.SetActive(false);
    }

    // --- 시작 패널 관련 기능 ---
    public void OpenStartPanel()
    {
        if (startPanel != null) startPanel.SetActive(true);
    }

    public void CloseStartPanel() // 시작 패널 닫기 버튼, ESC에 연결
    {
        if (startPanel != null) startPanel.SetActive(false);
    }

    // --- 게임 종료 기능 (YES 버튼) ---
    public void QuitGame()
    {
        Debug.Log("게임 종료 버튼이 눌렸습니다."); // 콘솔창 확인용

#if UNITY_EDITOR
        // 유니티 에디터에서 플레이 모드를 멈춤
        EditorApplication.isPlaying = false;
#else
            // 실제 빌드된 게임(exe)을 종료함
            Application.Quit();
#endif
    }
}