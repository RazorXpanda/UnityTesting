using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public static UIManagement current;
    public GameObject infoPanel;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        StartCoroutine(StartingGame());
    }

    public void SetOverlayHealthUI(Slider _slider, int _value)
    {
        if(_slider !=  null)
            _slider.value = _value;
    }

    private IEnumerator StartingGame()
    {
        yield return new WaitForSeconds(5f);
        if (infoPanel.activeSelf)
            infoPanel.SetActive(false);
    }
}
