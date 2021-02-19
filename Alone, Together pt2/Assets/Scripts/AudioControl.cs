using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMix;
    [SerializeField] private Slider audioSlider;

    private void Start()
    {
        if(audioSlider != null)
        {
            // audioSlider.value;
            mainMix.GetFloat("MasterVol", out float masterVol);
            audioSlider.value = masterVol;
        }
    }

    public void OnSliderValueChange()
    {
        if(audioSlider != null)
        {
            mainMix.SetFloat("MasterVol", audioSlider.value);
        }
    }
}
