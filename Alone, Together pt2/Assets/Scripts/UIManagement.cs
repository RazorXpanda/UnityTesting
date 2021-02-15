using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public static UIManagement current;
    public Slider slider;

    private void Start()
    {
        current = this;
    }

    public void SetOverlayHealthUI(int _value)
    {
        slider.value = _value;
    }
}
