using UnityEngine;
public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 1.0f; // 1초 후 삭제
    void Start()
    {
        Destroy(gameObject, destroyTime); // 지정된 시간 후 이 오브젝트 파괴
    }
}