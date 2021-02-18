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

    public Action onSpiritCollected;
    public void SpiritCollected()
    {
        if(onSpiritCollected != null)
        {
            onSpiritCollected();
        }
    }

    public void ObjectiveLost()
    {
        Debug.Log("Start end screen here");
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
