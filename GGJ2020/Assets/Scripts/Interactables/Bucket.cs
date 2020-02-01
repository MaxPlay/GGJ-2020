using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Interactable
{
    Sword inventory;

    [SerializeField]
    Transform swordPosition;

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
            SetProgressbarEnabled(true);
            inventory = (character as Player).PlaceSwordInWorkstation();
            inventory.transform.position = swordPosition.position;
        }
        else if (character.Inventory == null && inventory != null)
        {
            SetProgressbarEnabled(false);
            character.PickUpItem(inventory);
            inventory = null;
        }
        return this;
    }

    private void Update()
    {
        if(inventory != null && settings.BucketTimeToCooldDown > 0)
        {
            inventory.HeatSword(Time.deltaTime / -settings.BucketTimeToCooldDown);
            SetProgressbarValue(inventory.Heat);
        }
    }
}
