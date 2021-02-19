using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;      // public singletone of the event system

    private void Awake()
    {
        current = this;
    }

    public Action<int, int> onDamageReceived;
    public void DamageReceived(int _id, int _damage)
    {
        if (onDamageReceived != null)
            onDamageReceived(_id, _damage);
    }

    public Action<int> onSpiritCollected;
    public void SpiritCollected(int _multiplier)
    {
        if(onSpiritCollected != null)
        {
            onSpiritCollected(_multiplier);
        }
    }

    public void ObjectiveLost()
    {
        Debug.Log("Start lost end screen here");
        StartCoroutine(EndScreen());
    }

    public void ObjectiveAchieved()
    {
        Debug.Log("Start win end screen here");
        StartCoroutine(EndScreen());
    }

    private IEnumerator EndScreen()
    {
        Debug.Log("Starting end screen");
        // Shows the end screen here
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
