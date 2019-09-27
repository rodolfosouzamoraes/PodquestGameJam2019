using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CanvasUI : MonoBehaviour
{
    [SerializeField] GameObject canvasManual;
    [SerializeField] GameObject canvasCredits;
    [SerializeField] GameObject canvasSettings;
    [SerializeField] GameObject posProcessingGo;
    GameManager gm;
    private DepthOfField dofModel;

    private void Start()
    {
        dofModel = FindObjectOfType<DepthOfField>();
        gm = FindObjectOfType<GameManager>();
        canvasSettings.SetActive(false);
        canvasCredits.SetActive(false);
        canvasManual.SetActive(false);
    }
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
        dofModel.focusDistance = new FloatParameter { value = 10f };
        gm.SetDofModel(dofModel);
        camAnimator.SetBool("PositionCamera", true);
        this.gameObject.SetActive(false);
    }

    public void Manual()
    {
        gameObject.SetActive(false);
        canvasManual.SetActive(true);
    }

    public void Credits()
    {
        gameObject.SetActive(false);
        canvasCredits.SetActive(true);
    }

    public void Settings()
    {
        gameObject.SetActive(false);
        canvasSettings.SetActive(true);
        canvasSettings.GetComponent<CanvasSettings>().SetSlider();
    }
    

    public void Exit()
    {
        Application.Quit();
    }
}
