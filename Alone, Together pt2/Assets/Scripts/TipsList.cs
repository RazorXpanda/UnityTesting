using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsList : MonoBehaviour
{
    public Text tipsText;
    private List<string> listOfTips = new List<string>();
    private void Start()
    {
        listOfTips.Add("Don't forget to defend your castle!");
        listOfTips.Add("Light up all the torches to save the world!");
        listOfTips.Add("Some ghosts may drop spirits.");
        listOfTips.Add("There are items hidden around the vases across the map!");
        listOfTips.Add("Ghosts will aim for the castle. Defend it!");
        listOfTips.Add("Ghosts can see much better in the dark.");
    }

    public void Tips()
    {
        string randomListString = listOfTips[Random.Range(0, listOfTips.Count - 1)];
        tipsText.text = "Tip: " + randomListString;
    }
}
