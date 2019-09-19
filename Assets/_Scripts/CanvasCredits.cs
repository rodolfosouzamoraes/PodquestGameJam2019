using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCredits : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
        FindObjectOfType<GameManager>().ActiveCanvasUI();
        gameObject.SetActive(false);
    }
}
