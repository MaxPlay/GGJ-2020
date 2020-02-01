using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStash : Interactable
{
    [SerializeField]
    private GameObject woodPrefab;

    public override Interactable Interact(Character character)
    {
        if(character.Inventory == null)
        {
            Wood spawnedWood = Instantiate(woodPrefab).GetComponent<Wood>();

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
