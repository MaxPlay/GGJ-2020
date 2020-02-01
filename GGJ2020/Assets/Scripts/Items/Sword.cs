using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{
    private bool hasHandle;
    private float sharpness;
    private float quality;
    private float heat;

    private bool debug = true;

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
        if (debug)
            Debug.Log("<b>[Sword]</b> New Heat: " + heat);
        return heat;
    }

    public float SharpenSword(float strength)
    {
        sharpness = Mathf.Clamp01(sharpness + strength);
        if (debug)
            Debug.Log("<b>[Sword]</b> New Sharpness: " + sharpness);
        return sharpness;
    }

    public float HammerSword(float strength)
    {
        quality = Mathf.Clamp01(quality + strength);
        if (debug)
            Debug.Log("<b>[Sword]</b> New Quality: " + quality);
        return quality;
    }

    public bool AddHandle()
    {
        if (hasHandle)
        {
            if (debug)
                Debug.Log("<b>[Sword]</b> Already Had a Handle");
            return false;
        }

        hasHandle = true;
        if (debug)
            Debug.Log("<b>[Sword]</b> Added Handle");
        return true;
    }

    public void Initialize(Objective objective)
    {
        hasHandle = !objective.Grip;
        sharpness = objective.Grind ? 0 : 1;
        quality = objective.Smith ? 0 : 1;
        heat = 0;
    }
}
