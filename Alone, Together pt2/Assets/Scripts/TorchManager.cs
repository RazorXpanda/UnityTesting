using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    List<GameObject> torches;
    bool sendWinSignal;

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
    }

    private void Update()
    {
        if (IsAllTorchesLit() && sendWinSignal == false)
        {
            GameEvents.current.ObjectiveAchieved();
            sendWinSignal = true;
        }
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
