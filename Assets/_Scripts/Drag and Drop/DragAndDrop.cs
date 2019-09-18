using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject colliderOfFoodOnPlate;
    Vector3 dist;
    Vector3 startPos;
    Rigidbody rb;
    AudioSource[] soundHit; // Array para diversos sons que possa acontecer com o objeto
    GameManager gm;
    float posX;
    float posZ;
    float posY;
    bool isDragging = true; //Jogo começa com o jogador conseguindo arrastar e solta
    bool isCollided = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        soundHit = GetComponents<AudioSource>();
        rb = GetComponent<Rigidbody>();
        colliderOfFoodOnPlate.SetActive(false);
    }

    private void OnMouseUp()
    {
        colliderOfFoodOnPlate.SetActive(true);
        rb.useGravity = true;
        isDragging = false; // Quando o jogador solta o mouse, ele não consegue mais arrastar
    }
    void OnMouseDown()
    {   
        if(isDragging)
        {
            rb.useGravity = false;
            //isDragging = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z);
            startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            posZ = Input.mousePosition.z - dist.z;
            soundHit[0].Play();
            colliderOfFoodOnPlate.SetActive(false);
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            float disX = Input.mousePosition.x - posX;
            float disY = Input.mousePosition.y - posY;
            float disZ = Input.mousePosition.z - posZ;
            Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));
            transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Mesa") && !isCollided)
        {
            WrongTarget();
        }
        else if (collision.gameObject.tag.Equals("Tabua"))
        {
            isDragging = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollided)
        {
            //gm.foodDropped++;
            //Debug.Log(gm.foodDropped);
            isCollided = true;
            GameObject go = other.gameObject;
            switch (gameObject.tag)
            {
                case "Hamburger":
                    CheckTarget(go.tag, "PosiHamburger", go);
                    break;
                case "Egg":
                    CheckTarget(go.tag, "PosiEgg", go);
                    break;
                case "Lettude":
                    CheckTarget(go.tag, "PosiLettude", go);
                    break;
                case "Steak":
                    CheckTarget(go.tag, "PosiSteak", go);
                    break;
                case "Chicken":
                    CheckTarget(go.tag, "PosiChicken", go);
                    break;
                case "Maki":
                    CheckTarget(go.tag, "PosiMaki", go);
                    break;
                case "Tempura":
                    CheckTarget(go.tag, "PosiTempura", go);
                    break;
                case "Sushi":
                    CheckTarget(go.tag, "PosiSushi", go);
                    break;
            }
        }
        
    }

    private void CheckTarget(string objectTag, string posiFood, GameObject go)
    {
        Destroy(GetComponent<DragAndDrop>()); // vai destruir o Script para não correr o risco do jogador pega-lo novamente
        if (objectTag.Equals(posiFood))
        {
            gm.AddTotalHitPerStage();
            soundHit[1].Play();
            colliderOfFoodOnPlate.SetActive(false);
            Destroy(go);
            Debug.Log("Acertou!");
        }
        else
        {
            WrongTarget();
        }
    }

    void WrongTarget()
    {
        DropDragAndDrop(); // não permite que o jogador pegue o aliemento para não correr o risco dele pegar um alimento diferente depois que errar o anterior.
        soundHit[2].Play();
        Debug.Log("Errou!");
        gm.RestartStages();
    }

    private static void DropDragAndDrop()
    {
        DragAndDrop[] dragAndDrop = FindObjectsOfType<DragAndDrop>();
        foreach (DragAndDrop dd in dragAndDrop)
        {
            Destroy(dd);
        }
    }
}
