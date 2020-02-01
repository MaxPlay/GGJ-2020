using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private LinearPath path;

    private BoxCollider trigger;

    public Objective Objective { get; private set; }

    public int Index { get; set; }

    public LinearPath Path { get => path; }

    private void Start()
    {
        path.Owner = transform.position;
        trigger = GetComponent<BoxCollider>();
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

        if (forward)
        {
            Sword sword = Instantiate(GameManager.Instance.Prefabs.Sword, transform.position + trigger.center, Quaternion.identity);
            sword.Initialize(Objective);
        }
        else
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

    private void OnTriggerEnter(Collider other)
    {
        Sword sword = other.GetComponent<Sword>();
        if (sword != null)
        {
            if(Objective.DoesMatch(sword))
            {
                Destroy(sword.gameObject);
                ObjectiveCompleted();
            }
        }
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
        Gizmos.DrawCube(transform.position + Vector3.up * 1.9f, Vector3.one * 0.3f);
        Gizmos.color = Objective.Grip ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 1.6f, Vector3.one * 0.3f);
        Gizmos.color = Objective.Smith ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 1.3f, Vector3.one * 0.3f);
    }
}
