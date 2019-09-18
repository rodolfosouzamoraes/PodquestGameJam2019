﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animatorCamera;
    [SerializeField] GameObject canvasUI;
    [SerializeField] GameObject[] stages; // todos os Estagios/Fases
    [SerializeField] int[] totalFoodPerPlate; // Posicao 0 = PratoA, 1 = PratoB ...
    [SerializeField] int[] totalHitPerStage;
    [SerializeField] ParticleSystem particleSystem;
    void Start()
    {
        foreach(GameObject go in stages)
        {
            go.SetActive(false);
        }
        stages[0].SetActive(true);
    }

    public void AddTotalHitPerStage()
    {
        int posi = 0;
        foreach (GameObject go in stages)
        {
            if (go.activeSelf) // verifica qual estagio está ativo.
            {
                totalHitPerStage[posi]++;
                particleSystem.Play();
                if (totalHitPerStage[posi] == totalFoodPerPlate[posi])
                {
                    particleSystem.Play();
                    Debug.Log("Estagio Completado!");
                    Invoke("NextStage",2f);
                }
                return;
            }
            posi++;
        }
    }

    void NextStage()
    {
        int posi = 0;
        int nextPosi = 0;
        int totalStages = stages.Length;
        foreach (GameObject go in stages)
        {
            if (go.activeSelf) // verifica qual estagio está ativo.
            {
                nextPosi = posi + 1;
                go.SetActive(false);
                Debug.Log("NextPosi: " + nextPosi + ", TotalStage: " + totalStages);
                if (nextPosi+1 > totalStages)
                {
                    //Fim de Jogo
                    return;
                }
                else
                {
                    stages[nextPosi].SetActive(true); // ativa o proximo estagio.
                    return;
                }
                
            }
            posi++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animatorCamera.SetBool("PositionCamera", false);
            canvasUI.SetActive(true);
        }
    }
}
