using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingStation : Interactable
{
    [SerializeField]
    Transform smithingPosition;

    [SerializeField]
    bool debug = false;

    public Animator anim;

    public Vector3 SmithingPosition
    {
        get
        {
            return smithingPosition.position;
        }
    }

    public override Interactable Interact(Character character)
    {

        return this;
    }

    public override void Start()
    {
        anim = GetComponent<Animator>();
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
