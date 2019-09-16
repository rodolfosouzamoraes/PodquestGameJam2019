using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animatorCamera;
    [SerializeField] GameObject canvasUI;
    void Start()
    {
        
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
