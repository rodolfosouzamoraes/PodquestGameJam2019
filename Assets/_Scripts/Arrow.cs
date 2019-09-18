using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        
    }

    public void SetTarget(GameObject go)
    {
        target = go;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            gameObject.transform.position = new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);
        }
    }
}
