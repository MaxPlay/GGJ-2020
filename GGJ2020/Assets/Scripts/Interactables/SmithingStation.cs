using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingStation : Interactable
{
    [SerializeField]
    float smithingSpeed;

    [SerializeField]
    Transform smithingPosition;

    [SerializeField]
    bool debug = false;

    public Vector3 SmithingPosition
    {
        get
        {
            return smithingPosition.position;
        }
    }

    public float SmithingSpeed { get => smithingSpeed; }

    public override Interactable Interact(Character character)
    {

        return this;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void OnDrawGizmos()
    {
        if (smithingPosition != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(smithingPosition.position, 0.1f);
        }
    }
}
