using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    protected List<IStageObserver> _observers = new List<IStageObserver>();

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

/*        if (GameManager.Instance.countFinish == 4) {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime);
            enabled = false;
        }*/

        Vector3 centerpoint = getCenterPoint();
        Vector3 newPosition = centerpoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.1f);

        Zoom();

    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetDistance()/10f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetDistance() {
        int firstChild = 0;
        while (firstChild != 4 && target[firstChild] == null) {
            firstChild += 1;
        }
        if (firstChild == 4)
        {
            return 0f;
        }
        var bound = new Bounds(target[firstChild].position, Vector3.zero);
        for (int i = 0; i<target.Count;i++) {
            if (target[i] !=null)
            {
                bound.Encapsulate(target[i].position);
            }
           
        }

        return bound.size.x;
    }

    Vector3 getCenterPoint() {
        int firstChild = 0;
        if (target.Count == 0)
            return Vector3.zero;

        while ( firstChild != 4 && target[firstChild] == null)
        {
            firstChild += 1;
        }

        if (firstChild == 4)
        {
            return Vector3.zero;
        }

        var bound = new Bounds(target[firstChild].position, Vector3.zero);

        if (target.Count == 1)
            return target[firstChild].position;
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i] != null){ 
                bound.Encapsulate(target[i].position);
            }
        }

        return bound.center;
    }

}
