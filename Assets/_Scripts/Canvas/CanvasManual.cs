using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManual : MonoBehaviour
{
    [SerializeField] GameObject[] slides;

    private void Start()
    {
        foreach(GameObject go in slides)
        {
            go.SetActive(false);
        }
        slides[0].SetActive(true);
    }
    public void Exit()
    {
        FindObjectOfType<GameManager>().ActiveCanvasUI();
        gameObject.SetActive(false);
    }

    public void NextSlide(int value)
    {
        int i = 0;
        foreach(GameObject go in slides)
        {
            if (go.activeSelf)
            {
                
                go.SetActive(false);
                int next = i + 1 * value;
                if (next > 0)
                {
                    if (i < slides.Length)
                    {
                        if (next == 8)
                        {
                            slides[0].SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Next: " + next + ", tamanho slides: " + slides.Length);
                            slides[next].SetActive(true);
                        }
                        
                    }
                    else
                    {
                        slides[0].SetActive(true);
                    }
                }
                else if (next < 0)
                {
                    slides[7].SetActive(true);
                }
                else
                {
                    slides[0].SetActive(true);
                }
                return;

            }
            i++;
        }
    }
}
