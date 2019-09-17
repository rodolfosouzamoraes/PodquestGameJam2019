using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 dist;
    Vector3 startPos;
    Rigidbody rb;
    AudioSource soundHit;
    float posX;
    float posZ;
    float posY;
    bool isDragging = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        isDragging = true;
        soundHit = GetComponent<AudioSource>();
    }
    private void OnMouseUp()
    {
        rb.useGravity = true;
        isDragging = false;

        soundHit.enabled = false;
    }
    void OnMouseDown()
    {
        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;

        soundHit.Play();
    }

    void OnMouseDrag()
    {
        if(isDragging)
        {
            float disX = Input.mousePosition.x - posX;
            float disY = Input.mousePosition.y - posY;
            float disZ = Input.mousePosition.z - posZ;
            Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));
            transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
        }
    }
}
