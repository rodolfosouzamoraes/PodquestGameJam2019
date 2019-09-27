using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] int time = 180; //180 segundos = 3 minutos
    [SerializeField] TextMeshPro txtTimer;
    AudioSource[] audios;
    GameManager gm;
    int cacheTime;
    int minute;
    int seconds;
    bool isActive;

    private void Start()
    {
        cacheTime = time;
        audios = GetComponents<AudioSource>();
        gm = FindObjectOfType<GameManager>();
    }

    public void StartTimer()
    {
        isActive = true;
        audios[0].volume = PlayerPrefs.GetFloat("VolumeEffects");
        SetTime();
        StartCoroutine(DigitalWatch());
    }

    private void SetTime()
    {
        minute = cacheTime / 60;
        seconds = cacheTime - (minute * 60);

        if (minute < 10)
        {
            if (seconds < 10)
            {
                txtTimer.text = "0" + minute + ":0" + seconds;
            }
            else
            {
                txtTimer.text = "0" + minute + ":" + seconds;
            }

        }
    }

    IEnumerator DigitalWatch()
    {
        while (isActive)
        {
            yield return new WaitForSecondsRealtime(1f);
            cacheTime--;
            SetTime();
            audios[0].Play();
            if (cacheTime <= 0)
            {
                //gm.RestartStages("Timer");
                isActive = false;
                gm.SetGameOver();
            }
        }
    }

    public void ResetTimer()
    {
        cacheTime = time;
        isActive = true;
        SetTime();
        StartCoroutine(DigitalWatch());
    }

    public void PauseTimer()
    {
        isActive = false;
    }
}
