using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStash : Interactable
{
    public override Interactable Interact(Character character)
    {
        if(character.Inventory == null)
        {
            Wood spawnedWood = Instantiate(prefabs.Wood).GetComponent<Wood>();

            character.PickUpItem(spawnedWood);
        }

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
