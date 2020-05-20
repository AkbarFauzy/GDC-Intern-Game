using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardScript : MonoBehaviour
{
    public Camera cam;
    public Vector3 offset;
    
    void LateUpdate()
    {
        transform.position = cam.transform.position + offset;
    }

}
