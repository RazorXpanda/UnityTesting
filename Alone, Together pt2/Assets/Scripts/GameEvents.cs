using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;      // public singletone of the event system
    public GameObject losePanel;    //Activates lose Panel
    public GameObject winPanel; //Actives Win Panel
    public TipsList tipsList;

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
        StartCoroutine(EndScreen(true, false));
    }

    public void ObjectiveAchieved()
    {
        Debug.Log("Start win end screen here");
        StartCoroutine(EndScreen(true, true));
    }

    bool isFaded = false;
    private IEnumerator EndScreen(bool fadeIn, bool isWinPanel)
    {
        
        Debug.Log("Starting end screen");
        // Shows the end screen here

        if(isWinPanel)
        {
        winPanel.SetActive(true);       
        Text[] fadeText = winPanel.GetComponentsInChildren<Text>();
        
        if (fadeIn)
        {   
            foreach(Text item in fadeText)
            {
                for (float i = 0; i <= 5; i += Time.deltaTime) //1 second fade
                {
                    item.color = new Color(1,1,1,i);
                    yield return null;
                }
            }
        }  
        }

        else if (!isWinPanel)
        {
            losePanel.SetActive(true);       
            Text[] fadeText = losePanel.GetComponentsInChildren<Text>();
            tipsList.Tips();
            
            if (fadeIn)
            {   
                foreach(Text item in fadeText)
                {
                    for (float i = 0; i <= 5; i += Time.deltaTime) //1 second fade
                    {
                        item.color = new Color(1,1,1,i);
                        yield return null;
                    }
                }
            }  
        }
                      
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
