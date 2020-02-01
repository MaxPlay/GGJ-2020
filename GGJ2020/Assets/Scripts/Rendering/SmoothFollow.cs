using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private Transform lookAtTarget;
    private Vector3 velocity;

    public Transform LookAtTarget
    {
        get
        {
            return lookAtTarget;
        }

        set
        {
            lookAtTarget = value;
            transform.position = lookAtTarget.position;
        }
    }


    private void FixedUpdate()
    {
        if (lookAtTarget == null)
            return;

        transform.position = Vector3.SmoothDamp(transform.position, lookAtTarget.position, ref velocity, 0.3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1);
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}