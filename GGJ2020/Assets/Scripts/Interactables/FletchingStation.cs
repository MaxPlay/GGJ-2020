using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FletchingStation : Interactable
{
    [SerializeField]
    Transform grindingPosition;

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
}
