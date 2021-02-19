using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public static UIManagement current;

    private void Awake()
    {
        current = this;
    }

    public void SetOverlayHealthUI(Slider _slider, int _value)
    {
        if(_slider !=  null)
            _slider.value = _value;
    }
}
