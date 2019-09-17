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
    bool isDragging = false;
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
        isDragging = false;
    }
    void OnMouseDown()
    {
        rb.useGravity = false;
        isDragging = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z);
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
        soundHit[0].Play();
        colliderOfFoodOnPlate.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollided)
        {
            isCollided = true;
            string objectTag = other.gameObject.tag;
            GameObject go = other.gameObject;
            switch (gameObject.tag)
            {
                case "Hamburger":
                    CheckTarget(objectTag, "PosiHamburger", go);
                    break;
                case "Egg":
                    CheckTarget(objectTag, "PosiEgg", go);
                    break;
                case "Lettude":
                    CheckTarget(objectTag, "PosiLettude", go);
                    break;
                case "Steak":
                    CheckTarget(objectTag, "PosiSteak", go);
                    break;
                case "Chicken":
                    CheckTarget(objectTag, "PosiChicken", go);
                    break;
                case "Maki":
                    CheckTarget(objectTag, "PosiMaki", go);
                    break;
                case "Tempura":
                    CheckTarget(objectTag, "PosiTempura", go);
                    break;
                case "Sushi":
                    CheckTarget(objectTag, "PosiSushi", go);
                    break;
            }
        }
        
    }

    private void CheckTarget(string objectTag, string posiFood, GameObject go)
    {
        Destroy(GetComponent<DragAndDrop>()); // vai destruir o Script para não correr o risco do jogador pega-lo novamente
        gm.AddTotalHitPerStage();
        if (objectTag.Equals(posiFood))
        {
            soundHit[1].Play();
            colliderOfFoodOnPlate.SetActive(false);
            Destroy(go);
            Debug.Log("Acertou!");
        }
        else
        {
            soundHit[2].Play();
            Debug.Log("Errou!");
        }
    }
}
