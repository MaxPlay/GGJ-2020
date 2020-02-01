using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingStation : Interactable
{
    [SerializeField]
    FurnaceStation furnace;

    [SerializeField]
    Transform grindingPosition;

    [SerializeField]
    float timeToFullyHeat;

    public Vector3 GrindingPosition
    {
        get
        {
            return grindingPosition.position;
        }
    }

    public float TimeToFullyHeat { get => timeToFullyHeat; }

    public override Interactable Interact(Character character)
    {
        return this;
    }

    public void HeatFurnace()
    {
        if(timeToFullyHeat > 0)
        {
            furnace.HeatFurnace(Time.deltaTime / timeToFullyHeat);
        }
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
