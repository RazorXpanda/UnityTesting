using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchManager : MonoBehaviour
{
    List<GameObject> torches;
    bool sendWinSignal;
    int totalTorches;
    public int currentTorches;
    [SerializeField] GameObject torchUI;
    Text torchText;

    private void Start()
    {
        
        sendWinSignal = false;
        if (torches == null)
        {
            torches = new List<GameObject>();
            foreach(var torch in GameObject.FindGameObjectsWithTag("Torch"))
            {
                torches.Add(torch);
            }
        }
        currentTorches = 0;
        totalTorches = torches.Count;
        torchText = torchUI.GetComponent<Text>();
        torchText.text = currentTorches + "/ 22";
    }

    private void Update()
    {
        if (IsAllTorchesLit() && sendWinSignal == false)
        {
            GameEvents.current.ObjectiveAchieved();
            sendWinSignal = true;
        }

        torchText.text = currentTorches + "/ 22";
    }

    bool IsAllTorchesLit()
    {
        foreach(var torch in torches)
        {
            if (torch.GetComponent<TorchInteraction>().isLit == false)
                return false;
        }

        return true;
    }
}
