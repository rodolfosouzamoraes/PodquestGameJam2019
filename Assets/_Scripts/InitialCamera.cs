using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCamera : MonoBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gm.StartTimer();
    }
}
