using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject optionPanel;
    public GameObject pausePanel;

    [Header("Audio")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false);

        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    // Gọi khi nhấn nút Option trong Pause
    public void OpenOption()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (optionPanel != null)
            optionPanel.SetActive(true);
    }

    // Gọi khi nhấn Back trong Option
    public void Back()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void SetMusicVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}