using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManual : MonoBehaviour
{
    public void Exit()
    {
        FindObjectOfType<GameManager>().ActiveCanvasUI();
        gameObject.SetActive(false);
    }
}
