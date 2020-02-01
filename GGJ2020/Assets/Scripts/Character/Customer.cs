using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    LinearPath path;

    public Objective Objective { get; private set; }

    public int Index { get; set; }

    public LinearPath Path { get => path; }

    private void Start()
    {
        path.Owner = transform.position;
    }

    public IEnumerator Move(bool forward)
    {
        float time = 0.0f;
        float totalTime = GameManager.Instance.Settings.CustomerMoveTime;

        if (totalTime != 0.0f)
        {
            while (time < totalTime)
            {
                transform.position = path.Lerp((forward ? 1 : -1) * time / totalTime);
                yield return null;
                time += Time.deltaTime;
            }
        }
        else
        {
            transform.position = path.Lerp(forward ? 1 : -1);
        }

        if (!forward)
        {
            Objective = null;
            GameManager.Instance.GameState.ObjectiveQueue.FreeSlot(Index);
        }
    }

    public void Initialize(Objective objective)
    {
        Objective = objective;
        StartCoroutine(Move(true));
    }

    public void ObjectiveCompleted()
    {
        Move(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(path.Start + path.Owner, path.End + path.Owner);
        Gizmos.DrawCube(path.Start + path.Owner, Vector3.one * 0.2f);
        Gizmos.DrawCube(path.End + path.Owner, Vector3.one * 0.2f);

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
