using System;
using UnityEngine;

public class CustomerLine : MonoBehaviour
{
    [SerializeField]
    LinearPath line;
    
    private Customer[] customerPool;

    public Customer this[int index] => customerPool[index];

    private void Start()
    {
        line.Owner = transform.position;
        customerPool = new Customer[GameManager.Instance.Settings.MaxObjectives];
        for (int i = 0; i < customerPool.Length; i++)
        {
            customerPool[i] = Instantiate(GameManager.Instance.Prefabs.Customer, GetLocationWithOffset(i) - GameManager.Instance.Prefabs.Customer.Path.End, GameManager.Instance.transform.rotation, transform);
            customerPool[i].Index = i;
        }
    }

    private Vector3 GetLocationWithOffset(int offset)
    {
        if (GameManager.Instance.Settings.MaxObjectives == 0)
            return line.Start + line.Owner;
        return line.Lerp(offset / (GameManager.Instance.Settings.MaxObjectives - 1.0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(line.Start + transform.position, line.End + transform.position);
        Gizmos.DrawCube(line.Start + transform.position, Vector3.one * 0.2f);
        Gizmos.DrawCube(line.End + transform.position, Vector3.one * 0.2f);
    }
}

