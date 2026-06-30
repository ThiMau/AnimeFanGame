using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject bossUI;
    public BossAttack bossAttack;

    public float triggerX = 80f; // chỉnh theo map bạn

    private bool triggered = false;

    void Start()
    {
        if (bossUI != null)
            bossUI.SetActive(false); // ẩn lúc đầu
    }

    void Update()
    {
        if (triggered) return;

        if (Camera.main.transform.position.x >= triggerX)
        {
            triggered = true;

            bossUI.SetActive(true);

            bossAttack.isActive = true; // 🔥 bật boss

            Debug.Log("Boss UI ON 🔥");
        }
    }
}