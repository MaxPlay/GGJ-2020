﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{
    private bool hasHandle;
    private float sharpness;
    private float quality;
    private float heat;

    public bool HasHandle { get => hasHandle; }
    public float Sharpness { get => sharpness; }
    public float Quality { get => quality; }
    public float Heat { get => heat; }

    public override Interactable Interact(Character character)
    {
        return base.Interact(character);
    }

    public float HeatSword(float strength)
    {
        heat += strength;
        return heat;
    }

    public float SharpenSword(float strength)
    {
        sharpness = Mathf.Clamp01(sharpness + strength);
        return sharpness;
    }

    public float HammerSword(float strength)
    {
        quality = Mathf.Clamp01(quality + strength);
        return quality;
    }

    public bool AddHandle()
    {
        if (hasHandle)
            return false;
        hasHandle = true;
        return true;
    }
}
