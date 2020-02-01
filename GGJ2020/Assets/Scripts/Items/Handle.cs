using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : Item
{
    private bool debug = true;

    public override Interactable Interact(Character character)
    {
        return base.Interact(character);
    }
}
