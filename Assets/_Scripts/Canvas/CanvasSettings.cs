using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettings : MonoBehaviour
{
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderEffects;
    [SerializeField] GameManager gm;


    public void SetSlider()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("VolumeMusic");
        sliderEffects.value = PlayerPrefs.GetFloat("VolumeEffects");
    }
    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("VolumeMusic", sliderMusic.value);
        PlayerPrefs.SetFloat("VolumeEffects", sliderEffects.value);
        gm.SetGameVolume();
        Exit();
    }

    public void Exit()
    {
        gm.ActiveCanvasUI();
        gameObject.SetActive(false);
    }

}
