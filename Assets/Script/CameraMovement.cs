using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> target;
    public Camera cam;
    public Vector3 offset;
    public float minZoom;
    public float maxZoom;

    private Vector3 velocity;

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

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.1f);

        Zoom();

    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetDistance()/50f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
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

    public void removePlayer(int pNumber)
    {

    }

}
