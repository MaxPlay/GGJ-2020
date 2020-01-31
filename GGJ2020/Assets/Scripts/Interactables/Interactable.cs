using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool isBeingUsed;

    abstract public Interactable Interact(Player player);
}
