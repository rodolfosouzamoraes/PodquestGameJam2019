using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animatorCamera;
    [SerializeField] GameObject canvasUI;
    [SerializeField] GameObject[] stages; // todos os Estagios/Fases
    [SerializeField] GameObject inGameStage;
    [SerializeField] GameObject originalStage;
    [SerializeField] int[] totalFoodPerPlate; // Posicao 0 = PratoA, 1 = PratoB ...
    [SerializeField] int[] totalHitPerStage;
    [SerializeField] ParticleSystem particleSystem;
    AudioSource[] audioSource;
    Timer timer;
    //[HideInInspector]
    //public int foodDropped = 0; // não preciso mais contar se o alimento caiu no prato, apenas se ele acertou ou não.
    //GameObject arrow;
    bool restart = false;
    float hitMargin = 0.75f; // margem de acerto
    bool isActive;
    string source;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        //arrow = GameObject.FindGameObjectWithTag("Seta");
        audioSource = GetComponents<AudioSource>();
        isActive = true;
        source = "";
        //DisableArrow();
        StartStages();
        StartGameVolume();
    }

    private void StartStages()
    {
        foreach (GameObject go in stages)
        {
            go.SetActive(false);
        }
        stages[0].SetActive(true);

        for (int x = 0; x< totalHitPerStage.Length; x++)
        {
            totalHitPerStage[x] = 0;
        }
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
                CheckStage(posi);
                return;
            }
            posi++;
        }
    }
    public void RestartStages(string origin) // reinicia estagios.
    {
        source = origin;
        PlaySoundEffect(3);
        StartCoroutine(DelayRestart());
    }

    IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(2f);
        RestartGame();
    }

    private void RestartGame()
    {
        foreach (GameObject stage in stages) // Destroi todos os stagios do jogo
        {
            Destroy(stage);
        }

        GameObject go = Instantiate(originalStage); // instancia um novo objeto Estagios com todos os estagios
        go.transform.position = inGameStage.transform.position; // coloca na posição do Estagios atual;
        Destroy(inGameStage); //Limpa a variavel para armazenar o novo Estagios

        inGameStage = go;
        int totalStage = inGameStage.transform.childCount; // conta quantos estagios tem dentro do Estagios
        for (int x = 0; x < totalStage; x++)
        {
            stages[x] = inGameStage.transform.GetChild(x).gameObject;
        }

        if (source.Equals("Timer")) // se o game over vier do tempo ele reseta o tempo
        {
            source = "";
            timer.ResetTimer();
        }

        StartStages();
    }

    private void CheckStage(int posi)
    {
        if (totalHitPerStage[posi] == totalFoodPerPlate[posi]) // verifica se todos os alimentos foram jogados
        {
            Debug.Log("Estagio Completado!");
            Invoke("NextStage", 2f);
        }
    }

    void NextStage()
    {
        //foodDropped = 0;
        int posi = 0;
        int nextPosi = 0;
        int totalStages = stages.Length;
        if(restart)
        {
            stages[0].SetActive(true);
            restart = false;
            return;
        }
        else
        {
            foreach (GameObject go in stages)
            {
                if (go.activeSelf) // verifica qual estagio está ativo.
                {
                    nextPosi = posi + 1;
                    go.SetActive(false);
                    Debug.Log("NextPosi: " + nextPosi + ", TotalStage: " + totalStages);
                    if (nextPosi + 1 > totalStages)
                    {
                        //Fim de Jogo
                        //Audio de bater palmas
                        isActive = false;
                        PlaySoundEffect(4);
                        StartCoroutine(Restart());
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
        
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(6.3f);
        ReturnCavnasUI();
        RestartGame();
        isActive = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MainMenu();
        }
        
    }

    private void MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnCavnasUI();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ReturnCavnasUI()
    {
        timer.PauseTimer();
        animatorCamera.SetBool("PositionCamera", false);
        ActiveCanvasUI();
    }

    //public void ActiveArrow(GameObject target)
    //{
    //    arrow.GetComponent<Arrow>().SetTarget(target);
    //    arrow.SetActive(true);
    //}
    //public void DisableArrow()
    //{
    //    arrow.SetActive(false);
    //}

    public void SetGameVolume()
    {
        audioSource[0].volume = PlayerPrefs.GetFloat("VolumeMusic");
        audioSource[1].volume = PlayerPrefs.GetFloat("VolumeEffects");
        audioSource[2].volume = PlayerPrefs.GetFloat("VolumeEffects");
        audioSource[3].volume = PlayerPrefs.GetFloat("VolumeEffects");
    }

    public void ActiveCanvasUI()
    {
        canvasUI.SetActive(true);
    }
    
    public void StartGameVolume()
    {
        float firstAccess = PlayerPrefs.GetFloat("FirstAccess");
        if (firstAccess == 0)
        {
            PlayerPrefs.SetFloat("FirstAccess", 1);
            PlayerPrefs.SetFloat("VolumeMusic", 0.5f);
            PlayerPrefs.SetFloat("VolumeEffects", 0.5f);
        }
        audioSource[0].volume = PlayerPrefs.GetFloat("VolumeMusic");
        audioSource[1].volume = PlayerPrefs.GetFloat("VolumeEffects");
        audioSource[2].volume = PlayerPrefs.GetFloat("VolumeEffects");
        audioSource[3].volume = PlayerPrefs.GetFloat("VolumeEffects");
        audioSource[0].Play();
    }

    public void PlaySoundEffect(int i)
    {
        audioSource[i].Play();
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(4.3f);
        timer.StartTimer();
    }
}
