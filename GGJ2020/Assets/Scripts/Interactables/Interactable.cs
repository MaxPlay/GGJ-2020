using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected GameplaySettings settings;

    [SerializeField]
    protected PrefabContainer prefabs;

    [SerializeField]
    protected StationProgressbar stationProgressbar;

    [SerializeField]
    Slider progressSlider;
    [SerializeField]
    bool startWithSlider;

    public bool isCurrentlyInteractable = true;


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

    public virtual void Awake()
    {
        if (progressSlider)
            progressSlider.gameObject.SetActive(startWithSlider);
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

    public void SetProgressbarValue(float val)
    {
        if(progressSlider)
            progressSlider.value = val;
    }

    public void SetProgressbarEnabled(bool val)
    {
        if(progressSlider)
            progressSlider.gameObject.SetActive(val);
    }

    abstract public Interactable Interact(Character character);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
