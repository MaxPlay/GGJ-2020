﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FletchingStation : Interactable
{
    [SerializeField]
    Transform grindingPosition;

    [SerializeField]
    float timeToGrind;

    public Vector3 GrindingPosition
    {
        get
        {
            return grindingPosition.position;
        }
    }

    public float TimeToGrind { get => timeToGrind; }

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(grindingPosition.position, 0.1f);
    }
}
