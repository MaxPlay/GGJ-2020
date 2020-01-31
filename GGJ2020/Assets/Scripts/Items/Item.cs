using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public override Interactable Interact(Player player)
    {
        return this;
    }
}
