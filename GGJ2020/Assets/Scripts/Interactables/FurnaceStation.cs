using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceStation : Interactable
{
    [SerializeField]
    float heatDropSpeed;

    [SerializeField]
    Transform dropParent;

    float currentHeat = 0.5f;
    Sword inventory;

    private int currentHeatLevel
    {
        get
        {
            return (int)(currentHeat * 4);
        }
    }

    public float HeatDropSpeed { get => heatDropSpeed; }

    public override Interactable Interact(Character character)
    {
        if(character.Inventory is Sword && inventory == null)
        {
            inventory = (character as Player).PlaceSwordInFurncae();
            inventory.transform.parent = dropParent;
            inventory.transform.localPosition = Vector3.zero;
        }
        else if(character.Inventory == null && inventory != null)
        {
            character.PickUpItem(inventory);
            inventory = null;
        }
        return this;
    }

    public void HeatFurnace(float strength)
    {
        currentHeat = Mathf.Clamp01(currentHeat + strength);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void Update()
    {
        if(currentHeatLevel > 0 && inventory != null)
        {
            inventory.HeatSword(Time.deltaTime / (9 / currentHeatLevel));
        }
    }
}
