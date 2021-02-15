using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        settingsPanel.SetActive(false);
    }
    public void OnStartButtonClick(int gameSceneNumber)
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
        SceneManager.LoadScene(gameSceneNumber);        
    }

    public void OnSettingsButtonClick()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
        settingsPanel.SetActive(true);
    }

    public void OnSettingsButtonExit()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
        settingsPanel.SetActive(false);
    }
}
