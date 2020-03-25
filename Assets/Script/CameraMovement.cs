﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> target;
    public Camera cam;
    public Vector3 offset;
    public float minZoom;
    public float maxZoom;



    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target.Count == 0)
            return;
        
        Vector3 centerpoint = getCenterPoint();
        Vector3 newPosition = centerpoint + offset;

        newPosition.z = newPosition.z * ((GetDistance()/200F)+1);

        transform.position = newPosition;

        Zoom();

    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetDistance()/200f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom, Time.deltaTime);
    }

    float GetDistance() {
        var bound = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i<target.Count;i++) {
            bound.Encapsulate(target[i].position);
        }

        return bound.size.x;
    }

    Vector3 getCenterPoint() {
        if (target.Count == 1)
            return target[0].position;
        
        var bound = new Bounds(target[0].position, Vector3.zero);
        for (int i = 0; i < target.Count; i++)
        {
           bound.Encapsulate(target[i].position);
        }

        return bound.center;
    }



}
