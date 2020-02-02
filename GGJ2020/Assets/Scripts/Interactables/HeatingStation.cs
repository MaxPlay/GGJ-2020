using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingStation : Interactable
{
    [SerializeField]
    FurnaceStation furnace;

    [SerializeField]
    Transform heatingPosition;

    public Vector3 HeatingPosition
    {
        get
        {
            return heatingPosition.position;
        }
    }

    public float TimeToFullyHeat { get => settings.TimeToFullyHeat; }

    public override Interactable Interact(Character character)
    {
        return this;
    }

    public void HeatFurnace()
    {
        if(settings.TimeToFullyHeat > 0)
        {
            furnace.HeatFurnace(((Time.deltaTime / settings.TimeToFullyHeat) / 4) / (furnace.CurrentHeatLevel + 1));
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
        if(heatingPosition != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(heatingPosition.position, 0.1f);
        }
    }
}
