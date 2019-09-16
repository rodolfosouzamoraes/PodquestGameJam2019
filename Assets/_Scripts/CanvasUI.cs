using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{

    public void SelectButton(Animator animator)
    {
        animator.SetBool("Selected", true);
    }

    public void NotSelectButton(Animator animator)
    {
        animator.SetBool("Selected", false);
    }

    public void Play(Animator camAnimator)
    {
        camAnimator.SetBool("PositionCamera", true);
        this.gameObject.SetActive(false);
    }

    public void Manual()
    {

    }

    public void Credits()
    {

    }

    public void Sair()
    {
        Application.Quit();
    }
}
