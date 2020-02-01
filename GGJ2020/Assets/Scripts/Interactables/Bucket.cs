using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    Sword inventory;

    [SerializeField]
    Transform swordPosition;

    [SerializeField]
    float timeToCooldown;

    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override Interactable Interact(Character character)
    {
        if(character.Inventory != null && character.Inventory is Sword && inventory == null)
        {
            inventory = (character as Player).PlaceSwordInWorkstation();
            inventory.transform.position = swordPosition.position;
        }
        else if (character.Inventory == null && inventory != null)
        {
            character.PickUpItem(inventory);
            inventory = null;
        }
        return this;
    }

    private void Update()
    {
        if(inventory != null && timeToCooldown > 0)
        {
            inventory.HeatSword(Time.deltaTime / -timeToCooldown);
        }
    }
}
