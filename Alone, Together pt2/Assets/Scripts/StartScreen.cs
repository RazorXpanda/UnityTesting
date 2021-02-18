using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void OnStartButtonClick(int gameSceneNumber)
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
        SceneManager.LoadScene(gameSceneNumber);        
    }
}
