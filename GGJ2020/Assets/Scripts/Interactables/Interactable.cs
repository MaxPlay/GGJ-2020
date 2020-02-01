using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField]
private StationProgressbar stationProgressbar;


    protected bool isBeingUsed;

    private static List<Interactable> instances;

    [SerializeField]
    protected float interactionRange;

    public float InteractionRange
    {
        get
        {
            return interactionRange;
        }
    }

    public static List<Interactable> Instances
    {
        get
        {
            return instances;
        }
    }

    public virtual void Start()
    {
        if(instances == null)
        {
            instances = new List<Interactable>();
        }
        if(!instances.Contains(this))
            instances.Add(this);
    }

    public virtual void OnDestroy()
    {
        if (instances == null)
        {
            instances = new List<Interactable>();
        }
        instances.Remove(this);
    }

    public virtual void OnEnable()
    {
        if (instances == null)
        {
            instances = new List<Interactable>();
        }
        if (!instances.Contains(this))
            instances.Add(this);
    }

    public virtual void OnDisable()
    {
        if (instances == null)
        {
            instances = new List<Interactable>();
        }
        instances.Remove(this);
    }

    abstract public Interactable Interact(Character character);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
