using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer _BGMaudioMixer;
    public AudioMixer _SFXaudioMixer;
    [SerializeField] private GameObject pausePanel;

    public void SetVolume (float volume)
    {
        _BGMaudioMixer.SetFloat("volume", volume);
    }

    public void SetSFXVal (float volume)
    {
        _SFXaudioMixer.SetFloat("sfxVal", volume);
    }


    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void OnPauseButtonClick()
    {
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
        else
        {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        }

    }
}
