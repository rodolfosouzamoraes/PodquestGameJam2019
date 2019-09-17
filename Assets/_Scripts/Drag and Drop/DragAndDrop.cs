using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 dist;
    Vector3 startPos;
    Rigidbody rb;
    AudioSource[] soundHit; // Array para diversos sons que possa acontecer com o objeto
    GameManager gm;
    float posX;
    float posZ;
    float posY;
    bool isDragging = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        soundHit = GetComponents<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseUp()
    {
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
        string objectTag = other.gameObject.tag;
        switch (gameObject.tag)
        {
            case "Hamburger":
                CheckTarget(objectTag, "PosiHamburger");
                break;
            case "Egg":
                CheckTarget(objectTag, "PosiEgg");
                break;
        }
    }

    private void CheckTarget(string objectTag, string posiFood)
    {
        Destroy(GetComponent<DragAndDrop>()); // vai destruir o Script para não correr o risco do jogador pega-lo novamente
        gm.AddTotalHitPerStage();
        if (objectTag.Equals(posiFood))
        {
            soundHit[1].Play();
            Debug.Log("Acertou!");
        }
        else
        {
            soundHit[2].Play();
            Debug.Log("Errou!");
        }
    }
}
