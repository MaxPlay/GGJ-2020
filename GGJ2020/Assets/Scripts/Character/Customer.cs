using UnityEngine;

public class Customer : MonoBehaviour
{
    public Objective Objective { get; private set; }

    public void Initialize(Objective objective)
    {
        Objective = objective;
    }

    private void OnDrawGizmos()
    {
        if (Objective == null)
            return;

        Gizmos.color = Objective.Grind ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 0.3f, Vector3.one * 0.3f);
        Gizmos.color = Objective.Grip ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one * 0.3f);
        Gizmos.color = Objective.Smith ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * -0.3f, Vector3.one * 0.3f);
    }
}