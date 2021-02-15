using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public float startDelay = 2f;
    public float endDelay = 4f;

    private WaitForSeconds startWait;
    private WaitForSeconds endWait;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        SpawnPlayer();

        StartCoroutine(GameLoop());
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        // Either enable the UI for start over or exit to main menu here
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // For playing again
        // or load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator RoundStarting()
    {
        ResetRound();

        Debug.Log("Round is starting");
        yield return startWait;

        EnablePlayerControl();
    }

    private IEnumerator RoundPlaying()
    {
        while (playerIsAlive())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        // Display score or anything necessary at when game is ended
        yield return endWait;
    }

    private void ResetRound()
    {
        // Reset player's position, remove all enemies entities that are still active
        // Disable spawners
        return;
    }

    private void EnablePlayerControl()
    {
        // Eneable playerController script, enabling their ability to control the character
        return;
    }

    private bool playerIsAlive()
    {
        return true;
    }
}
