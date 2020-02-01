using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LookInDirectionCamera : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    void Update()
    {
        if(cam)
        {
            transform.forward = cam.transform.position - transform.position;
        }
        else if(Camera.main != null)
        {
            transform.forward = new Vector3(transform.forward.x, (Camera.main.transform.position - transform.position).y, (Camera.main.transform.position - transform.position).z);
        }
    }
}
