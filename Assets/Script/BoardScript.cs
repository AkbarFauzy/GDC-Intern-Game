using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public Camera cam;
    public Vector3 offset;
    
    void LateUpdate()
    {
        transform.position = cam.transform.position + offset;
    }
}
