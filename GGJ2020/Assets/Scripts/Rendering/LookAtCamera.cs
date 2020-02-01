using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    Transform lookAtTarget;

    SmoothFollow smoothFollow;

    private void Start()
    {
        GameObject smoothFollowObject = new GameObject();
        smoothFollowObject.transform.position = lookAtTarget == null ? Vector3.zero : lookAtTarget.position;
        smoothFollow = smoothFollowObject.AddComponent<SmoothFollow>();
    }

    private void Update()
    {
        if (lookAtTarget == null)
        {
            smoothFollow.LookAtTarget = null;
            return;
        }
        else
        {
            if (smoothFollow.LookAtTarget == null)
                smoothFollow.LookAtTarget = lookAtTarget;
        }

        transform.LookAt(smoothFollow.transform);
    }
}