using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public GameObject panel;

    void Awake()
    {
        Instance = this;

        panel.SetActive(false);
    }

    public void OpenShop()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}